using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrDelayVwConfig : IEntityTypeConfiguration<HrDelayVw>
    {
        public void Configure(EntityTypeBuilder<HrDelayVw> entity)
        {
            entity.ToView("HR_Delay_VW");

        }
    }
}


