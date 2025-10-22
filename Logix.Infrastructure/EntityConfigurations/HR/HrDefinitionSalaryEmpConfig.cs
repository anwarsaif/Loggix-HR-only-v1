using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrDefinitionSalaryEmpConfig : IEntityTypeConfiguration<HrDefinitionSalaryEmp>
    {
        public void Configure(EntityTypeBuilder<HrDefinitionSalaryEmp> entity)
        {
            entity.ToView("HR_Definition_Salary_Emp", "dbo");

        }
    }



}


