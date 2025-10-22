using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrRecruitmentCandidateKpiDVwConfig : IEntityTypeConfiguration<HrRecruitmentCandidateKpiDVw>
    {
        public void Configure(EntityTypeBuilder<HrRecruitmentCandidateKpiDVw> entity)
        {
            entity.ToView("HR_Recruitment_Candidate_KPI_D_VW");

            entity.Property(e => e.KpiTemComId).IsFixedLength();
        }
    }  

}
