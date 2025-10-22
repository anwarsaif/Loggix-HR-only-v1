using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrKpiTemplatesCompetencesVwConfig : IEntityTypeConfiguration<HrKpiTemplatesCompetencesVw>
    {
        public void Configure(EntityTypeBuilder<HrKpiTemplatesCompetencesVw> entity)
        {
            entity.ToView("HR_KPI_Templates_Competences_VW");


        }
    }
}
