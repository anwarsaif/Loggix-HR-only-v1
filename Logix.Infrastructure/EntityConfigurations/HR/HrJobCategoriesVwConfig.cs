using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
	public class HrJobCategoriesVwConfig : IEntityTypeConfiguration<HrJobCategoriesVw>
    {
        public void Configure(EntityTypeBuilder<HrJobCategoriesVw> entity)
        {
			entity.ToView("HR_Job_Categories2_VW");
		}
    }
}
