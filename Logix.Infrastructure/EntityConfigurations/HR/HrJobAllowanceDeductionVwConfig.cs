using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
	public class HrJobAllowanceDeductionVwConfig : IEntityTypeConfiguration<HrJobAllowanceDeductionVw>

    {
        public void Configure(EntityTypeBuilder<HrJobAllowanceDeductionVw> entity)
        {
			entity.ToView("HR_Job_Allowance_Deduction_VW");

			entity.Property(e => e.Id).ValueGeneratedOnAdd();
		}
    }
}


