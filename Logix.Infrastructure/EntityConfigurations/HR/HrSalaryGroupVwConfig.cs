using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrSalaryGroupVwConfig : IEntityTypeConfiguration<HrSalaryGroupVw>
    {
        public void Configure(EntityTypeBuilder<HrSalaryGroupVw> entity)
        {
            entity.ToView("HR_Salary_Group_VW");
        }
    }
    
}
