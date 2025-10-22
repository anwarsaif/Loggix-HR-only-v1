using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrVisitScheduleLocationConfig : IEntityTypeConfiguration<HrVisitScheduleLocation>
    {
        public void Configure(EntityTypeBuilder<HrVisitScheduleLocation> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
        }
    }



}


