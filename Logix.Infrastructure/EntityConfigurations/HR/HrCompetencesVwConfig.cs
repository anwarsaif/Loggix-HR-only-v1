using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrCompetencesVwConfig : IEntityTypeConfiguration<HrCompetencesVw>
    {
        public void Configure(EntityTypeBuilder<HrCompetencesVw> entity)
        {
            entity.ToView("HR_Competences_VW");
        }
    }

}
