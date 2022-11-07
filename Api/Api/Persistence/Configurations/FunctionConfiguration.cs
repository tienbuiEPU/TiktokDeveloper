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
    public class FunctionConfiguration : AppEntityTypeIntConfiguration<Function>
    {
        public override void Configure(EntityTypeBuilder<Function> builder)
        {
            base.Configure(builder);
            builder.ToTable("Function");
        }
    }
}
