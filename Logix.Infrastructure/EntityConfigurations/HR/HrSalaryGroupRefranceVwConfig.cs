using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrSalaryGroupRefranceVwConfig : IEntityTypeConfiguration<HrSalaryGroupRefranceVw>
    {
        public void Configure(EntityTypeBuilder<HrSalaryGroupRefranceVw> entity)
        {
            entity.ToView("HR_Salary_Group_Refrance_VW");
        }
    }  
    
}
