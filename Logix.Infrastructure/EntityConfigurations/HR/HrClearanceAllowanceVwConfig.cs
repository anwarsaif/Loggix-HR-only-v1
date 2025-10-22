using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrClearanceAllowanceVwConfig : IEntityTypeConfiguration<HrClearanceAllowanceVw>
    {
        public void Configure(EntityTypeBuilder<HrClearanceAllowanceVw> entity)
        {
            entity.ToView("HR_Clearance_Allowance_VW");
        }
    }
}
