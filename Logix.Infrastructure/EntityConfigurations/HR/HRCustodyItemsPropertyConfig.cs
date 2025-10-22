using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrCustodyItemsPropertyConfig : IEntityTypeConfiguration<HrCustodyItemsProperty>
    {
        public void Configure(EntityTypeBuilder<HrCustodyItemsProperty> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        }
    } 
}


