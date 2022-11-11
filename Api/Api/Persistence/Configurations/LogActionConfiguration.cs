using Api.Common.Bases.Configurations;
using Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Persistence.Configurations
{
    public class LogActionConfiguration : AppEntityTypeLongConfiguration<LogAction>
    {
        public override void Configure(EntityTypeBuilder<LogAction> builder)
        {
            base.Configure(builder);
            builder.ToTable("LogAction");

            builder.Property(c => c.ActionName).HasMaxLength(2000).IsRequired(false);
            builder.Property(c => c.TableName).HasMaxLength(500).IsRequired(false);
            builder.Property(c => c.IpAddress).HasMaxLength(500).IsRequired(false);
            builder.Property(c => c.Log).HasColumnType("ntext").IsRequired(false);
        }
    }
}
