using CompanyManagement.API.Enums;
using CompanyManagement.API.Helpers;
using CompanyManagement.API.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

using System.Reflection;

namespace CompanyManagement.API.DBContexts
{
    public class AppDbContext : DbContext
    {
        private static readonly Type _appDbContextType = typeof(AppDbContext);
        private static MethodInfo _configureEntityDefaultsMethodInfo = _appDbContextType.GetMethod(nameof(ConfigureEntityDefaults), BindingFlags.Instance | BindingFlags.NonPublic);
        private IHttpContextAccessor _contextAccessor;

        //public AppDbContext(DbContextOptions options) : base(options)
        //{

        //}

        public AppDbContext(DbContextOptions options, IHttpContextAccessor contextAccessor)
            : base(options)
        {
            _contextAccessor = contextAccessor;
        }

        public virtual DbSet<AuthUser> AuthUsers { get; set; }
        public virtual DbSet<Company> Companies { get; set; }

        public override int SaveChanges()
        {
            try
            {
                ApplyEntityActions();
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            try
            {
                ApplyEntityActions();
                return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(
            entity =>
            {
                entity.Property(e => e.Industry).HasConversion(e => e.ToString(), e => (IndustryType)Enum.Parse(typeof(IndustryType), e));
                entity.Property(e => e.CompanyNo).IsRequired().HasAnnotation("MinValue", 1);
            });

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuthUser>().HasData(
                new AuthUser { Id = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), FirstName = "Faisal", LastName="Shahzad",Email="faisal@gmail.com",Password="Faisal@123", Salt="LKDJ03230", CreatedBy= new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedBy = null, UpdatedAt = null },
                new AuthUser { Id = new Guid("CFAE8354-C7F7-4BD7-AF31-43FA991B078E"), FirstName = "Le", LastName = "Tuan", Email = "tuan@gmail.com", Password = "Faisal@123", Salt = "LKDJ03230", CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedBy = null, UpdatedAt = null },
                new AuthUser { Id = new Guid("D0413C86-36C6-486B-982D-B13FA76B90B9"), FirstName = "Marc", LastName = "Josha", Email = "marc@gmail.com", Password = "Faisal@123", Salt = "LKDJ03230", CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedBy = null, UpdatedAt = null }
            );

            modelBuilder.Entity<Company>().HasData(
                new Company { Id = new Guid("531FD700-37C7-47D6-9055-8474F62BE903"), CompanyNo = 1, CompanyName = "Company1", Industry=IndustryType.ITServices, NumberOfEmployees  = 10,City = "Kuala Lumpur", ParentCompanyId = null, CompanyLevel = 0, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("39B62D5B-1CB9-476F-9FD3-F67EE3C55436"), CompanyNo = 2, CompanyName = "Company2", Industry = IndustryType.ITServices, NumberOfEmployees = 55, City = "Kuala Lumpur", ParentCompanyId = null, CompanyLevel = 0, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("DAD85E0F-BE78-4537-B5A4-E74F747AEC46"), CompanyNo = 3, CompanyName = "Company3", Industry = IndustryType.ITServices, NumberOfEmployees = 150, City = "Kuala Lumpur", ParentCompanyId = null, CompanyLevel = 0, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("EDBC63E3-FA4B-47D0-A1D6-5E5D5F4F1913"), CompanyNo = 4, CompanyName = "Company4", Industry = IndustryType.ITServices, NumberOfEmployees = 30, City = "Kuala Lumpur", ParentCompanyId = null, CompanyLevel = 0, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("3EB8EB70-7C11-4FE9-B3C5-98C79219ACE7"), CompanyNo = 5, CompanyName = "Company5", Industry = IndustryType.ITServices, NumberOfEmployees = 100, City = "Kuala Lumpur", ParentCompanyId = null, CompanyLevel = 0, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("E10B7C92-75A1-45EA-932E-8804E48A9140"), CompanyNo = 6, CompanyName = "Company6", Industry = IndustryType.ITServices, NumberOfEmployees = 10, City = "Kuala Lumpur", ParentCompanyId = null, CompanyLevel = 0, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("4A96C358-87E4-4E3F-9CFA-9D22DFE2E557"), CompanyNo = 7, CompanyName = "Company7", Industry = IndustryType.ITServices, NumberOfEmployees = 40, City = "Kuala Lumpur", ParentCompanyId = null, CompanyLevel = 0, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("EF4CD96E-90FD-4E59-8FCD-F7421DB8606D"), CompanyNo = 8, CompanyName = "Company8", Industry = IndustryType.ITServices, NumberOfEmployees = 10, City = "Kuala Lumpur", ParentCompanyId = null, CompanyLevel = 0, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("3E768C9D-CEA6-4F4C-B729-1AAFD9AE778E"), CompanyNo = 9, CompanyName = "Company9", Industry = IndustryType.ITServices, NumberOfEmployees = 300, City = "Kuala Lumpur", ParentCompanyId = null, CompanyLevel = 0, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("DBD62898-0E2C-424D-9D48-42096EAAD2E2"), CompanyNo = 10, CompanyName = "Company10", Industry = IndustryType.ITServices, NumberOfEmployees = 500, City = "Kuala Lumpur", ParentCompanyId = null, CompanyLevel = 0, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("543ECCBB-006C-4E16-9B54-FF5C36E6FE9D"), CompanyNo = 11, CompanyName = "Company11", Industry = IndustryType.ITServices, NumberOfEmployees = 770, City = "Kuala Lumpur", ParentCompanyId = null, CompanyLevel = 0, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("C7311DEC-BFA8-4CA2-A897-E5E2E100187D"), CompanyNo = 12, CompanyName = "Company12", Industry = IndustryType.ITServices, NumberOfEmployees = 1200, City = "Kuala Lumpur", ParentCompanyId = null, CompanyLevel = 0, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("6727ACC2-D544-48D5-AB81-73A214204202"), CompanyNo = 13, CompanyName = "Company13", Industry = IndustryType.ITServices, NumberOfEmployees = 600, City = "Kuala Lumpur", ParentCompanyId = null, CompanyLevel = 0, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("5DA087F3-9E65-4009-8AB7-E404F927BBBE"), CompanyNo = 14, CompanyName = "Company14", Industry = IndustryType.ITServices, NumberOfEmployees = 3000, City = "Kuala Lumpur", ParentCompanyId = null, CompanyLevel = 0, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("591CFC26-5A7F-4927-A658-F211530D6F0D"), CompanyNo = 15, CompanyName = "Company15", Industry = IndustryType.ITServices, NumberOfEmployees = 550, City = "Kuala Lumpur", ParentCompanyId = null, CompanyLevel = 0, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("5F074A61-C8E4-4BFE-B02E-4CAC4741B51B"), CompanyNo = 16, CompanyName = "Company16", Industry = IndustryType.ITServices, NumberOfEmployees = 350, City = "Kuala Lumpur", ParentCompanyId = new Guid("531FD700-37C7-47D6-9055-8474F62BE903"), CompanyLevel = 1, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("A2503CF6-812C-49D8-95BC-3D95D49AF19E"), CompanyNo = 17, CompanyName = "Company17", Industry = IndustryType.ITServices, NumberOfEmployees = 10, City = "Kuala Lumpur", ParentCompanyId = new Guid("39B62D5B-1CB9-476F-9FD3-F67EE3C55436"), CompanyLevel = 1, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("AE32F26C-DD6D-4360-AAD4-A76C99BD7234"), CompanyNo = 18, CompanyName = "Company18", Industry = IndustryType.ITServices, NumberOfEmployees = 10, City = "Kuala Lumpur", ParentCompanyId = new Guid("531FD700-37C7-47D6-9055-8474F62BE903"), CompanyLevel = 1, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("050138B6-6C23-4346-88DB-87FB74BFCB44"), CompanyNo = 19, CompanyName = "Company19", Industry = IndustryType.ITServices, NumberOfEmployees = 100, City = "Kuala Lumpur", ParentCompanyId = new Guid("39B62D5B-1CB9-476F-9FD3-F67EE3C55436"), CompanyLevel = 1, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("3A80ED4E-5724-4799-A5DF-CF3A2AB819A4"), CompanyNo = 20, CompanyName = "Company20", Industry = IndustryType.ITServices, NumberOfEmployees = 270, City = "Kuala Lumpur", ParentCompanyId = new Guid("39B62D5B-1CB9-476F-9FD3-F67EE3C55436"), CompanyLevel = 1, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("CE0FE173-D0B7-4AAE-A26A-604B67848995"), CompanyNo = 21, CompanyName = "Company21", Industry = IndustryType.ITServices, NumberOfEmployees = 10, City = "Kuala Lumpur", ParentCompanyId = new Guid("531FD700-37C7-47D6-9055-8474F62BE903"), CompanyLevel = 1, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("3DCA1E9C-DF90-4B41-850E-1231AE301EC4"), CompanyNo = 22, CompanyName = "Company22", Industry = IndustryType.ITServices, NumberOfEmployees = 10, City = "Kuala Lumpur", ParentCompanyId = new Guid("DAD85E0F-BE78-4537-B5A4-E74F747AEC46"), CompanyLevel = 1, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("83A5BCBE-6D6E-4847-92FB-E0F966ABD05C"), CompanyNo = 23, CompanyName = "Company23", Industry = IndustryType.ITServices, NumberOfEmployees = 10, City = "Kuala Lumpur", ParentCompanyId = new Guid("E10B7C92-75A1-45EA-932E-8804E48A9140"), CompanyLevel = 1, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("8AF1DA6B-2CDB-455F-B37D-496E450E0258"), CompanyNo = 24, CompanyName = "Company24", Industry = IndustryType.ITServices, NumberOfEmployees = 50, City = "Kuala Lumpur", ParentCompanyId = new Guid("543ECCBB-006C-4E16-9B54-FF5C36E6FE9D"), CompanyLevel = 1, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("CE3D59FF-4499-4D85-AF44-E1B4F78DF3F1"), CompanyNo = 25, CompanyName = "Company25", Industry = IndustryType.ITServices, NumberOfEmployees = 10, City = "Kuala Lumpur", ParentCompanyId = new Guid("543ECCBB-006C-4E16-9B54-FF5C36E6FE9D"), CompanyLevel = 1, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("6548679A-D29C-477F-9958-1E982D221515"), CompanyNo = 26, CompanyName = "Company26", Industry = IndustryType.ITServices, NumberOfEmployees = 60, City = "Kuala Lumpur", ParentCompanyId = new Guid("543ECCBB-006C-4E16-9B54-FF5C36E6FE9D"), CompanyLevel = 1, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("D9091216-C339-4CFD-805F-75B9EEFA8603"), CompanyNo = 27, CompanyName = "Company27", Industry = IndustryType.ITServices, NumberOfEmployees = 80, City = "Kuala Lumpur", ParentCompanyId = new Guid("5F074A61-C8E4-4BFE-B02E-4CAC4741B51B"), CompanyLevel = 2, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("5474783E-3BDA-45FD-AC4F-74FC99656259"), CompanyNo = 28, CompanyName = "Company28", Industry = IndustryType.ITServices, NumberOfEmployees = 10, City = "Kuala Lumpur", ParentCompanyId = new Guid("CE0FE173-D0B7-4AAE-A26A-604B67848995"), CompanyLevel = 2, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("B61C1A5B-7AC5-453D-9FE0-6D2193DF89DF"), CompanyNo = 29, CompanyName = "Company29", Industry = IndustryType.ITServices, NumberOfEmployees = 10, City = "Kuala Lumpur", ParentCompanyId = new Guid("5F074A61-C8E4-4BFE-B02E-4CAC4741B51B"), CompanyLevel = 2, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null },
                new Company { Id = new Guid("3A716E09-C8D9-4E53-8206-2FCC2083927F"), CompanyNo = 30, CompanyName = "Company30", Industry = IndustryType.ITServices, NumberOfEmployees = 10, City = "Kuala Lumpur", ParentCompanyId = new Guid("83A5BCBE-6D6E-4847-92FB-E0F966ABD05C"), CompanyLevel = 2, CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedAt = null, UpdatedBy = null }
            );

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                _configureEntityDefaultsMethodInfo
                    .MakeGenericMethod(entityType.ClrType)
                    .Invoke(this, new object[] { modelBuilder, entityType });
            }
        }

        #region Private Methods

        private void ConfigureEntityDefaults<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType) where TEntity : class
        {
            if (typeof(IEntity).IsAssignableFrom(typeof(TEntity)))
            {
                modelBuilder.Entity<TEntity>(
                    entity =>
                    {
                        entity.Property(e => ((IEntity)e).Id).HasDefaultValueSql("NEWID()");
                        entity.Property(e => ((IEntity)e).CreatedBy).HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'");
                        entity.Property(e => ((IEntity)e).CreatedAt).HasDefaultValueSql("getdate()");
                        entity.Property(e => ((IEntity)e).UpdatedBy).HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'");
                        entity.Property(e => ((IEntity)e).UpdatedAt).HasDefaultValueSql("getdate()");
                    });
            }
        }

        private Guid GetCurrentUserId()
        {
            Guid result = Guid.Empty;
            if (_contextAccessor != null && _contextAccessor.HttpContext != null)
            {
                result = _contextAccessor.HttpContext.GetUserId();
            }

            return result;
        }

        private void ApplyEntityActions()
        {
            var userId = GetCurrentUserId();

            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        ApplyEntityAddedActions(entry, userId);
                        break;
                    case EntityState.Modified:
                        ApplyEntityUpdatedActions(entry, userId);
                        break;
                }
            }
        }

        private void ApplyEntityAddedActions(EntityEntry entry, Guid userId)
        {
            if (entry.Entity is IEntity)
            {
                ((IEntity)entry.Entity).CreatedBy = userId;
                ((IEntity)entry.Entity).CreatedAt = DateTime.UtcNow;
                ((IEntity)entry.Entity).UpdatedBy = userId;
                ((IEntity)entry.Entity).UpdatedAt = DateTime.UtcNow;
            }
        }

        private void ApplyEntityUpdatedActions(EntityEntry entry, Guid userId)
        {
            if (entry.Entity is IEntity)
            {
                ((IEntity)entry.Entity).UpdatedBy = userId;
                ((IEntity)entry.Entity).UpdatedAt = DateTime.UtcNow;
            }
        }

        #endregion

    }
}
