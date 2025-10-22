using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPayrollAllowanceAccountsVwConfig : IEntityTypeConfiguration<HrPayrollAllowanceAccountsVw>
    {
        public void Configure(EntityTypeBuilder<HrPayrollAllowanceAccountsVw> entity)
        {
            entity.ToView("HR_Payroll_Allowance_Accounts_VW");

        }
    }

}
