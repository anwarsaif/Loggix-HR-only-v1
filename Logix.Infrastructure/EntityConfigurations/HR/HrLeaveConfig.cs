using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrLeaveConfig : IEntityTypeConfiguration<HrLeave>
    {
        public void Configure(EntityTypeBuilder<HrLeave> entity)
        {
            //entity.ToTable("HR_Leave", tb => tb.HasTrigger("HR_Leave_Log_Ins"));

            entity.Property(e => e.BankId).IsFixedLength();
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        }
    }

}
