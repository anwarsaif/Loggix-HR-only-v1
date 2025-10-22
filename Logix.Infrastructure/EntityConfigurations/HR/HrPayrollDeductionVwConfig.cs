using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPayrollDeductionVwConfig : IEntityTypeConfiguration<HrPayrollDeductionVw>
    {
        public void Configure(EntityTypeBuilder<HrPayrollDeductionVw> entity)
        {
            entity.ToView("HR_Payroll_Deduction_VW", "dbo");
        }
    }
}


