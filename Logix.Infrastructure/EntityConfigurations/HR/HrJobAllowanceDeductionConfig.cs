using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
	public class HrJobAllowanceDeductionConfig : IEntityTypeConfiguration<HrJobAllowanceDeduction>

    {
        public void Configure(EntityTypeBuilder<HrJobAllowanceDeduction> entity)
        {
			entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
		}
    } 
}


