using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
	public class HrJobCategoryConfig : IEntityTypeConfiguration<HrJobCategory>
    {
        public void Configure(EntityTypeBuilder<HrJobCategory> entity)
        {
			entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
		}
    }
}
