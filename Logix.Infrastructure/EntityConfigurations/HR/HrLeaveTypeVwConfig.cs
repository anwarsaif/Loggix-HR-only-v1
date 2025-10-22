using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrLeaveTypeVwConfig : IEntityTypeConfiguration<HrLeaveTypeVw>
    {
        public void Configure(EntityTypeBuilder<HrLeaveTypeVw> entity)
        {
            entity.ToView("HR_Leave_Type_VW");

        }
    }

}
