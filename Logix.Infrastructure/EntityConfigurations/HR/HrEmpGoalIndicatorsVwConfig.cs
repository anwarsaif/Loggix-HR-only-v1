using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrEmpGoalIndicatorsVwConfig : IEntityTypeConfiguration<HrEmpGoalIndicatorsVw>
    {
        public void Configure(EntityTypeBuilder<HrEmpGoalIndicatorsVw> entity)
        {
            entity.ToView("HR_Emp_Goal_Indicators_VW", "dbo");

        }
    }



}


