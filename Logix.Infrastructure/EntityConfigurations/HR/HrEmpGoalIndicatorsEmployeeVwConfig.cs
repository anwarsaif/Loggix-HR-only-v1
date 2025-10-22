using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrEmpGoalIndicatorsEmployeeVwConfig : IEntityTypeConfiguration<HrEmpGoalIndicatorsEmployeeVw>
    {
        public void Configure(EntityTypeBuilder<HrEmpGoalIndicatorsEmployeeVw> entity)
        {
            entity.ToView("HR_Emp_Goal_Indicators_Employee_VW", "dbo");

        }
    }



}


