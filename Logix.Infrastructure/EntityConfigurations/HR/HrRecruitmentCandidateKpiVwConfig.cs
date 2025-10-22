using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrRecruitmentCandidateKpiVwConfig : IEntityTypeConfiguration<HrRecruitmentCandidateKpiVw>
    {
        public void Configure(EntityTypeBuilder<HrRecruitmentCandidateKpiVw> entity)
        {
            entity.ToView("HR_Recruitment_Candidate_KPI_VW");


        }
    } 

}
