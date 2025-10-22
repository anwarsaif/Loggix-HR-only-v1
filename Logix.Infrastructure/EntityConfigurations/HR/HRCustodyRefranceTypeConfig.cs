using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrCustodyRefranceTypeConfig : IEntityTypeConfiguration<HrCustodyRefranceType>
    {
        public void Configure(EntityTypeBuilder<HrCustodyRefranceType> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        }
    }  
}


