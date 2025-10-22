using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrLeaveVwConfig : IEntityTypeConfiguration<HrLeaveVw>
    {
        public void Configure(EntityTypeBuilder<HrLeaveVw> entity)
        {

            entity.ToView("HR_Leave_VW");

            entity.Property(e => e.BankId).IsFixedLength();
        }
    }

}
