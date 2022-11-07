using Api.Common.Bases;
using Api.Common.Bases.Configurations;
using Api.Common.Bases.Persistence;
using Api.Common.Interfaces.Helpers;
using Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static Api.Common.Enums.AppEnums;

namespace Api.Persistence
{
    public class ApiDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly IDateTimeService _dateTime;

        public ApiDbContext()
        {
        }

        public ApiDbContext(DbContextOptions<ApiDbContext> options,
            IDateTimeService datetime) : base(options)
        {
            _dateTime = datetime;
        }

        public virtual DbSet<Function> Functions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<FunctionRole> FunctionRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var assembly = typeof(AppEntityTypeBaseConfiguration<>).Assembly;
            builder.ApplyConfigurationsFromAssembly(assembly);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

                var configuration = builder.Build();
                string con = configuration.GetConnectionString("DbConnection");
                optionsBuilder.UseSqlServer(con);
            }

            //optionsBuilder.UseLazyLoadingProxies();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddAuditUserChange();

            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            AddAuditUserChange();

            return base.SaveChanges();
        }

        private void AddAuditUserChange()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity<int>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = _dateTime.Now;
                        entry.Entity.UpdatedAt = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = _dateTime.Now;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<BaseEntity<Guid>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = _dateTime.Now;
                        entry.Entity.UpdatedAt = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = _dateTime.Now;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<BaseEntity<long>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = _dateTime.Now;
                        entry.Entity.UpdatedAt = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = _dateTime.Now;
                        break;
                }
            }
        }
    }
}
