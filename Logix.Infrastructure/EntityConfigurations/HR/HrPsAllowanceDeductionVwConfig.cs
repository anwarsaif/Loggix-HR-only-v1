using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPsAllowanceDeductionVwConfig : IEntityTypeConfiguration<HrPsAllowanceDeductionVw>
    {
        public void Configure(EntityTypeBuilder<HrPsAllowanceDeductionVw> entity)
        {
            entity.ToView("HR_PS_Allowance_Deduction_VW");

        }
    }
    
}
