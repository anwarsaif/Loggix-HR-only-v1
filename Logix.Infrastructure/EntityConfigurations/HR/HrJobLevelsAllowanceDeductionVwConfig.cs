using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
	public class HrJobLevelsAllowanceDeductionVwConfig : IEntityTypeConfiguration<HrJobLevelsAllowanceDeductionVw>

    {
        public void Configure(EntityTypeBuilder<HrJobLevelsAllowanceDeductionVw> entity)
        {
			entity.ToView("HR_Job_Levels_Allowance_Deduction_VW");

			entity.Property(e => e.Id).ValueGeneratedOnAdd();
		}
    }
}


