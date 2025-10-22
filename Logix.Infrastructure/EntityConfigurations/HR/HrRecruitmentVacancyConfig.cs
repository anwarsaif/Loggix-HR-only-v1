using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrRecruitmentVacancyConfig : IEntityTypeConfiguration<HrRecruitmentVacancy>
    {
        public void Configure(EntityTypeBuilder<HrRecruitmentVacancy> entity)
        {
            entity.Property(e => e.DeptId).HasDefaultValueSql("((0))");

            entity.Property(e => e.FacilityId).HasDefaultValueSql("((1))");

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            entity.Property(e => e.LocationId).HasDefaultValueSql("((0))");

        }
    }

}
