using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrRecruitmentCandidateKpiConfig : IEntityTypeConfiguration<HrRecruitmentCandidateKpi>
    {
        public void Configure(EntityTypeBuilder<HrRecruitmentCandidateKpi> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

        }
    } 

}
