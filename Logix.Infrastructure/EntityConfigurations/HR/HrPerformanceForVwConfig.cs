using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPerformanceForVwConfig : IEntityTypeConfiguration<HrPerformanceForVw>
    {
        public void Configure(EntityTypeBuilder<HrPerformanceForVw> entity)
        {

            entity.ToView("HR_Performance_For_VW", "dbo");

        }
    }



}


