using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAttShiftCloseDConfig : IEntityTypeConfiguration<HrAttShiftCloseD>
    {
        public void Configure(EntityTypeBuilder<HrAttShiftCloseD> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

        }
    }  
}


