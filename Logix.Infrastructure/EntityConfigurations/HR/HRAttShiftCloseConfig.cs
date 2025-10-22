using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAttShiftCloseConfig : IEntityTypeConfiguration<HrAttShiftClose>
    {
        public void Configure(EntityTypeBuilder<HrAttShiftClose> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        }
    }  
}


