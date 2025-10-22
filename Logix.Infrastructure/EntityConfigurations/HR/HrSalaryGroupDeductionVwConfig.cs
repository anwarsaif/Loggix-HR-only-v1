using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrSalaryGroupDeductionVwConfig : IEntityTypeConfiguration<HrSalaryGroupDeductionVw>
    {
        public void Configure(EntityTypeBuilder<HrSalaryGroupDeductionVw> entity)
        {
            entity.ToView("HR_Salary_Group_Deduction_VW");
        }
    }

}
