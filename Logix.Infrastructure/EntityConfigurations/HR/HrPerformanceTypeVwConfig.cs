using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPerformanceTypeVwConfig : IEntityTypeConfiguration<HrPerformanceTypeVw>
    {
        public void Configure(EntityTypeBuilder<HrPerformanceTypeVw> entity)
        {

                entity.ToView("HR_Performance_Type_VW", "dbo");

        }
    }



}


