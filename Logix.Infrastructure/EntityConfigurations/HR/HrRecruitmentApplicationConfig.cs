using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrRecruitmentApplicationConfig : IEntityTypeConfiguration<HrRecruitmentApplication>
    {
        public void Configure(EntityTypeBuilder<HrRecruitmentApplication> entity)
        {
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            entity.Property(e => e.StatusId).HasDefaultValueSql("((1))");

        }
    }

}
