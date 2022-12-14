using Api.Common.Bases.Configurations;
using Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Persistence.Configurations
{
    public class FunctionConfiguration : AppEntityTypeIntConfiguration<Function>
    {
        public override void Configure(EntityTypeBuilder<Function> builder)
        {
            base.Configure(builder);
            builder.ToTable("Function");

            builder.Property(c => c.Code).HasMaxLength(200).IsRequired(true);
            builder.Property(c => c.Name).HasMaxLength(1000).IsRequired(true);
            builder.Property(c => c.Url).HasMaxLength(1000).IsRequired(true);
            builder.Property(c => c.Icon).HasMaxLength(500).IsRequired(false);
            builder.Property(c => c.Note).HasMaxLength(4000).IsRequired(false);
        }
    }
}
