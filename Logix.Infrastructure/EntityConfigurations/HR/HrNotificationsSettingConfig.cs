using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrNotificationsSettingConfig : IEntityTypeConfiguration<HrNotificationsSetting>
    {
        public void Configure(EntityTypeBuilder<HrNotificationsSetting> entity)
        {
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

        }
    }

}
