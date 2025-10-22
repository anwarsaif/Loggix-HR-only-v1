using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAllowanceDeductionVWConfig : IEntityTypeConfiguration<HrAllowanceDeductionVw>
    {
        public void Configure(EntityTypeBuilder<HrAllowanceDeductionVw> entity)
        {
            entity.ToView("HR_Allowance_Deduction_VW");
        }
    }  
}


