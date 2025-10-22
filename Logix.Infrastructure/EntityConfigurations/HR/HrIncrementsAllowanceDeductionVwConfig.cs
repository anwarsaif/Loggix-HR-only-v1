using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrIncrementsAllowanceDeductionVwConfig : IEntityTypeConfiguration<HrIncrementsAllowanceDeductionVw>
    {
        public void Configure(EntityTypeBuilder<HrIncrementsAllowanceDeductionVw> entity)
        {
            entity.ToView("HR_Increments_Allowance_Deduction_VW", "dbo");

        }
    }
}


