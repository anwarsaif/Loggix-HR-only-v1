using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrSalaryGroupAllowanceVwConfig : IEntityTypeConfiguration<HrSalaryGroupAllowanceVw>
    {
        public void Configure(EntityTypeBuilder<HrSalaryGroupAllowanceVw> entity)
        {
            entity.ToView("HR_Salary_Group_Allowance_VW");
        }
    }

}
