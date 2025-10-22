using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrKpiDetailesVwConfig : IEntityTypeConfiguration<HrKpiDetailesVw>
    {
        public void Configure(EntityTypeBuilder<HrKpiDetailesVw> entity)
        {
            entity.ToView("HR_KPI_Detailes_VW");

            entity.Property(e => e.KpiTemComId).IsFixedLength();
        }
    }

}
