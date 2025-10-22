using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrIncrementTypeConfig : IEntityTypeConfiguration<HrIncrementType>
    {
        public void Configure(EntityTypeBuilder<HrIncrementType> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

        }
    }



}


