using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrCustodyItemConfig : IEntityTypeConfiguration<HrCustodyItem>
    {
        public void Configure(EntityTypeBuilder<HrCustodyItem> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        }
        }
    }



