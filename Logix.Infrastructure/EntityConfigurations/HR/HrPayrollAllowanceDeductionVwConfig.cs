using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPayrollAllowanceDeductionVwConfig : IEntityTypeConfiguration<HrPayrollAllowanceDeductionVw>
    {
        public void Configure(EntityTypeBuilder<HrPayrollAllowanceDeductionVw> entity)
        {
            entity.ToView("HR_Payroll_allowance_Deduction_VW");

        }
    }

}
