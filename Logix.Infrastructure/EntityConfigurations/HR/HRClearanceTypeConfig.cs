using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrClearanceTypeConfig : IEntityTypeConfiguration<HrClearanceType>
    {
        public void Configure(EntityTypeBuilder<HrClearanceType> entity)
        {
            entity.Property(e => e.TypeId).ValueGeneratedNever();

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
        }
    }  
}


