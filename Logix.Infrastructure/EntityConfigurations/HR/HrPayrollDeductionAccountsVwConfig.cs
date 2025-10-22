using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPayrollDeductionAccountsVwConfig : IEntityTypeConfiguration<HrPayrollDeductionAccountsVw>
    {
        public void Configure(EntityTypeBuilder<HrPayrollDeductionAccountsVw> entity)
        {
            entity.ToView("HR_Payroll_Deduction_Accounts_VW");
        }
    }  
    
}
