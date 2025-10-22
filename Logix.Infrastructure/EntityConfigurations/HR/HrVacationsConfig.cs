using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrVacationsConfig : IEntityTypeConfiguration<HrVacation>
    {
        public void Configure(EntityTypeBuilder<HrVacation> entity)
        {
            entity.Property(e => e.ApplicationId).HasDefaultValue(0L);
            entity.Property(e => e.Approve).HasDefaultValue(false);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HrVdtId).HasComment("نوع القرار");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.IsSalary).HasDefaultValue(false);
            entity.Property(e => e.NeedJoinRequest).HasDefaultValue(false);
        }
    }
}
