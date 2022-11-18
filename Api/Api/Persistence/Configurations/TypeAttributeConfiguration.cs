﻿using Api.Common.Bases.Configurations;
using Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Persistence.Configurations
{
    public class TypeAttributeConfiguration : AppEntityTypeIntConfiguration<TypeAttribute>
    {
        public override void Configure(EntityTypeBuilder<TypeAttribute> builder)
        {
            base.Configure(builder);
            builder.ToTable("TypeAttribute");

            builder.Property(c => c.Code).HasMaxLength(200).IsRequired(true);
            builder.Property(c => c.Name).HasMaxLength(1000).IsRequired(true);
            builder.Property(c => c.Note).HasMaxLength(4000).IsRequired(false);
        }
    }
}
