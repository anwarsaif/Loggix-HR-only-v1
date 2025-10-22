using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPayrollAllowanceVwConfig : IEntityTypeConfiguration<HrPayrollAllowanceVw>
    {
        public void Configure(EntityTypeBuilder<HrPayrollAllowanceVw> entity)
        {
            entity.ToView("HR_Payroll_Allowance_VW");
        }
    }
}


