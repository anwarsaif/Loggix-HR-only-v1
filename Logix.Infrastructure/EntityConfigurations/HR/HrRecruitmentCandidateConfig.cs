using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrRecruitmentCandidateConfig : IEntityTypeConfiguration<HrRecruitmentCandidate>
    {
        public void Configure(EntityTypeBuilder<HrRecruitmentCandidate> entity)
        {
			entity.Property(e => e.CognitiveAbilityScore).HasDefaultValueSql("((0))");
			entity.Property(e => e.EnglishTestScore).HasDefaultValueSql("((0))");
			entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
			entity.Property(e => e.Rate).HasDefaultValueSql("((0))");

		}
    }

}
