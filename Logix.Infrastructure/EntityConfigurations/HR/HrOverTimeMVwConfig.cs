using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrOverTimeMVwConfig : IEntityTypeConfiguration<HrOverTimeMVw>
    {
        public void Configure(EntityTypeBuilder<HrOverTimeMVw> entity)
        {

            entity.ToView("HR_OverTime_M_VW");
        }
    }

}
