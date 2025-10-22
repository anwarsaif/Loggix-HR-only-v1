using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPerformanceStatusVwConfig : IEntityTypeConfiguration<HrPerformanceStatusVw>
    {
        public void Configure(EntityTypeBuilder<HrPerformanceStatusVw> entity)
        {
            entity.ToView("HR_Performance_Status_VW", "dbo");

        }
    }



}


