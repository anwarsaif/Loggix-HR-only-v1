using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrNotificationsSettingVwConfig : IEntityTypeConfiguration<HrNotificationsSettingVw>
    {
        public void Configure(EntityTypeBuilder<HrNotificationsSettingVw> entity)
        {
            entity.ToView("HR_Notifications_Setting_VW");
        }
    }

}
