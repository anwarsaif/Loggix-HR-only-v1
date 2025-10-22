using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrRecruitmentCandidateVwConfig : IEntityTypeConfiguration<HrRecruitmentCandidateVw>
    {
        public void Configure(EntityTypeBuilder<HrRecruitmentCandidateVw> entity)
        {
			entity.ToView("HR_Recruitment_Candidate_VW");
		}
    }

}
