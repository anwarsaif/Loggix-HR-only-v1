using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrClearanceConfig : IEntityTypeConfiguration<HrClearance>
    {
        public void Configure(EntityTypeBuilder<HrClearance> entity)
        {
            entity.Property(e => e.BankId).IsFixedLength();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        }
    }
}


