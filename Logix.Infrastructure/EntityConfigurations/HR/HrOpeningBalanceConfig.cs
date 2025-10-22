using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrOpeningBalanceConfig : IEntityTypeConfiguration<HrOpeningBalance>
    {
        public void Configure(EntityTypeBuilder<HrOpeningBalance> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

        }
    }
}


