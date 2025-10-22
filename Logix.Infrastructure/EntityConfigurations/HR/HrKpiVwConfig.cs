using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrKpiVwConfig : IEntityTypeConfiguration<HrKpiVw>
    {
        public void Configure(EntityTypeBuilder<HrKpiVw> entity)
        {
            entity.ToView("HR_KPI_VW");
        }
    }

}
