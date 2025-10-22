using Logix.Domain.Hr;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPayrollNoteConfig : IEntityTypeConfiguration<HrPayrollNote>
    {
        public void Configure(EntityTypeBuilder<HrPayrollNote> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

        }
    }

}
