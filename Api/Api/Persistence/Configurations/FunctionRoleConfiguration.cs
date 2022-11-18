using Api.Common.Bases.Configurations;
using Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Persistence.Configurations
{
    public class FunctionRoleConfiguration : AppEntityTypeIntConfiguration<FunctionRole>
    {
        public override void Configure(EntityTypeBuilder<FunctionRole> builder)
        {
            base.Configure(builder);
            builder.ToTable("FunctionRole");

            builder.Property(c => c.ActiveKey).HasMaxLength(10).IsRequired(true);
        }
    }
}
