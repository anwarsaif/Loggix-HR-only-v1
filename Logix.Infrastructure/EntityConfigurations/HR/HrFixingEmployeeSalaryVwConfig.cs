using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrFixingEmployeeSalaryVwConfig : IEntityTypeConfiguration<HrFixingEmployeeSalaryVw>
    {
        public void Configure(EntityTypeBuilder<HrFixingEmployeeSalaryVw> entity)
        {
            entity.ToView("HR_Fixing_Employee_Salary_VW");

        }
    }



}


