using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
	public class HrJobLevelsAllowanceDeductionConfig : IEntityTypeConfiguration<HrJobLevelsAllowanceDeduction>

    {
        public void Configure(EntityTypeBuilder<HrJobLevelsAllowanceDeduction> entity)
        {
			entity.HasKey(e => e.Id).HasName("PK_HR_Job_Level_Allowance_Deduction");

			entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
		}
    }
}


