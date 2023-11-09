using AutoMapper;

using CompanyManagement.API.Contracts;
using CompanyManagement.API.DBContexts;
using CompanyManagement.API.Enums;
using CompanyManagement.API.Exceptions;
using CompanyManagement.API.Helpers;
using CompanyManagement.API.Models;
using CompanyManagement.API.ViewModels;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel;

namespace CompanyManagement.API.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<CompanyService> _logger;
        private readonly IMapper _mapper;


        public CompanyService(AppDbContext dbContext,
            IMapper mapper,
            ILogger<CompanyService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CompanyViewModel> Get(Guid id)
        {
            try
            {
                var company = await _dbContext
                    .Companies
                    .Include(c => c.ParentCompany)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (company == null)
                {
                    throw new OperationException("No company data found.");
                }

                return _mapper.Map<CompanyViewModel>(company);
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error - CompanyService_Get(id: {id}, error: {ex.Message}.");
                throw new OperationException("Error occurred in getting the requested company data.");
            }
        }

        public async Task<dynamic> GetAll()
        {
            try
            {
                return await _dbContext.Companies
                .OrderBy(x => x.CompanyNo)
                .Select
                (c => new
                {
                    c.Id,
                    c.CompanyNo,
                    c.CompanyName
                })
                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error - CompanyService_GetAll(error: {ex.Message}.");
                throw new OperationException("Error occurred in getting the companies lists data.");
            }
        }

        public async Task<CompaniesListResultViewModel> GetList(CompanyFilterViewModel model)
        {
            try
            {
                var result = _dbContext.Companies
                    .Include(c => c.ParentCompany)
                    .AsNoTracking();

                if (model.CompanyName.IsNotNullOrEmpty())
                {
                    result = result.Where(c => c.CompanyName.Contains(model.CompanyName));
                }

                if (model.Industry.IsNotNullOrEmpty())
                {
                    var enumValue = (IndustryType)Enum.Parse(typeof(IndustryType), model.Industry);
                    result = result.Where(c => c.Industry == enumValue);
                }

                if (model.CompanyNo.HasValue && model.CompanyNo > 0)
                {
                    result = result.Where(c => c.CompanyNo == model.CompanyNo);
                }

                if (model.City.IsNotNullOrEmpty())
                {
                    result = result.Where(c => c.City.ToLower().Contains(model.City.ToLower()));
                }

                if (model.ParentCompany.IsNotNullOrEmpty())
                {
                    result = result.Where(c => c.ParentCompany != null && c.ParentCompany.CompanyName.ToLower().Contains(model.ParentCompany.ToLower()));
                }

                if (model.SortOrder == "desc")
                {
                    result = result.OrderByDescending(e => EF.Property<object>(e, model.SortBy));
                }
                else
                {
                    result = result.OrderBy(e => EF.Property<object>(e, model.SortBy));
                }

                var totalRecords = result.Count();

                var records = (await result
                    .Skip(model.PageIndex * model.PageSize)
                    .Take(model.PageSize)
                    .ToListAsync())
                    .Select(c => _mapper.Map<CompanyViewModel>(c))
                    .ToList();

                var endIndex = totalRecords * 1.0m / model.PageSize;
                return new CompaniesListResultViewModel
                {
                    Pagination = new CompanyPaginationViewModel
                    {
                        TotalRecords = totalRecords,
                        CurrentPageRecords = records.Count(),
                        PageSize = model.PageSize,
                        PageIndex = model.PageIndex,
                        StartIndex = 0,
                        EndIndex = (int)Math.Ceiling(endIndex),
                        SortBy = model.SortBy,
                        SortOrder = model.SortOrder,
                    },
                    Companies = records
                };

            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error - CompanyService_GetList(error: {ex.Message}.");
                throw new OperationException("Error occurred in getting the companies list.");
            }
        }

        public async Task<CompanyViewModel> CreateOrUpdateCompany(UpdateCompanyViewModel model)
        {
            try
            {
                var company = _mapper.Map<Company>(model);

                if (company != null)
                {
                    var existingCompany = await _dbContext.Companies.Select(x => new { x.Id, x.CompanyName })
                        .FirstOrDefaultAsync(company => company.CompanyName.ToLower() == model.CompanyName.ToLower());

                    if (existingCompany != null && existingCompany.Id != model.Id)
                    {
                        throw new CreateUpdateCompanyException($"Company Name \"{model.CompanyName}\" already exists.");
                    }

                    if (model.Id.HasValue)
                    {
                        var companyToUpdate = await _dbContext.Companies.FirstOrDefaultAsync(company => company.Id == model.Id);

                        if (companyToUpdate == null)
                        {
                            throw new CreateUpdateCompanyException("Company not found");
                        }

                        companyToUpdate.CompanyName = model.CompanyName;
                        companyToUpdate.City = model.City;
                        companyToUpdate.NumberOfEmployees = model.NumberOfEmployees;
                        companyToUpdate.Industry = (IndustryType)Enum.Parse(typeof(IndustryType), model.Industry);
                        companyToUpdate.ParentCompanyId = model.ParentCompanyId;

                        _dbContext.Companies.Update(companyToUpdate);
                    }
                    else
                    {
                        company.CompanyNo = GetNewCompanyNo();
                        _dbContext.Add(company);
                    }

                    company.CompanyLevel = await GetCompanyLevel(company.ParentCompanyId, 0);
                    await _dbContext.SaveChangesAsync();
                    return _mapper.Map<CompanyViewModel>(company);
                }

                throw new CreateUpdateCompanyException("Invalid company input.");
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error - CompanyService_CreateOrUpdateCompany(CompanyName: {model.CompanyName}): error saving company, {ex.Message}.");
                throw new CreateUpdateCompanyException("Error occurred in saving the company data.");
            }
        }

        #region Private Methods

        private int GetNewCompanyNo()
        {
            try
            {
                return (_dbContext.Companies.AsNoTracking().Max(x => x.CompanyNo)) + 1;
            }
            catch
            {
                return 1;
            }
        }

        private async Task<int> GetCompanyLevel(Guid? parentCompanyId, int currentLevel)
        {
            if (parentCompanyId.IsNotNullOrEmpty())
            {
                var parentCompany = await _dbContext
                    .Companies
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id.Equals(parentCompanyId));

                if (parentCompany != null)
                {
                    return await GetCompanyLevel(parentCompany.ParentCompanyId, ++currentLevel);
                }
            }

            return currentLevel;
        }

        #endregion
    }
}