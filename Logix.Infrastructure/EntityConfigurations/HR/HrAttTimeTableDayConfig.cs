using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAttTimeTableDayConfig : IEntityTypeConfiguration<HrAttTimeTableDay>
    {
        public void Configure(EntityTypeBuilder<HrAttTimeTableDay> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

        }
    } 
    
}
