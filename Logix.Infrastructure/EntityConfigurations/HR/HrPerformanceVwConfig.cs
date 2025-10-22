using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPerformanceVwConfig : IEntityTypeConfiguration<HrPerformanceVw>
    {
        public void Configure(EntityTypeBuilder<HrPerformanceVw> entity)
        {
            entity.ToView("HR_Performance_VW", "dbo");

        }
    }



}


