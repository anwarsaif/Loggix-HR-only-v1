using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAttShiftConfig : IEntityTypeConfiguration<HrAttShift>
    {
        public void Configure(EntityTypeBuilder<HrAttShift> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        }
    } 
    
}
