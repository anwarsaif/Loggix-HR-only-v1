using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrMandateVwConfig : IEntityTypeConfiguration<HrMandateVw>
    {
        public void Configure(EntityTypeBuilder<HrMandateVw> entity)
        {
			entity.ToView("HR_Mandate_VW");
		}
    }



}


