using AutoMapper;

using CompanyManagement.API.Enums;
using CompanyManagement.API.Models;
using CompanyManagement.API.ViewModels;

namespace CompanyManagementService.Mappings
{
    public static class AutoMapperConfig
    {
        public static void RegisterMapperProfiles(this IServiceCollection services)
        {
            var configuration = new MapperConfiguration(mapperConfig =>
            {
                mapperConfig.ConstructServicesUsing(type => ActivatorUtilities.CreateInstance(services.BuildServiceProvider(), type));
                mapperConfig.CreateMap<AuthUserViewModel, AuthUser>();
                mapperConfig.CreateMap<AuthUser, AuthUserViewModel>();

                mapperConfig.CreateMap<CompanyViewModel, Company>().
                ForMember(destination => destination.Industry, opts => opts.MapFrom(source => Enum.Parse(typeof(IndustryType), source.Industry))).
                ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id ?? Guid.Empty));
                mapperConfig.CreateMap<Company, CompanyViewModel>().
                ForMember(destination => destination.Industry, opts => opts.MapFrom(source => source.Industry.ToString()))
                .ForMember(destination => destination.ParentCompany, opts => opts.MapFrom(source => source.ParentCompany == null ? string.Empty : source.ParentCompany.CompanyName));

                mapperConfig.CreateMap<UpdateCompanyViewModel, Company>().
                ForMember(destination => destination.Industry, opts => opts.MapFrom(source => Enum.Parse(typeof(IndustryType), source.Industry))).
                ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id ?? Guid.Empty));
                mapperConfig.CreateMap<Company, UpdateCompanyViewModel>().
                ForMember(destination => destination.Industry, opts => opts.MapFrom(source => source.Industry.ToString()));
            });

            IMapper mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
