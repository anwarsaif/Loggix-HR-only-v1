using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAttTimeTableConfig : IEntityTypeConfiguration<HrAttTimeTable>
    {
        public void Configure(EntityTypeBuilder<HrAttTimeTable> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.ExitOnNextDate).HasDefaultValueSql("((0))");
        }
    }



}


