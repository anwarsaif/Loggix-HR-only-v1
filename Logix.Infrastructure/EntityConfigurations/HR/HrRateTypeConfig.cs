using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrRateTypeConfig : IEntityTypeConfiguration<HrRateType>
    {
        public void Configure(EntityTypeBuilder<HrRateType> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        }
    }



}


