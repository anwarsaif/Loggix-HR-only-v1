using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrNotificationsVwConfig : IEntityTypeConfiguration<HrNotificationsVw>
    {
        public void Configure(EntityTypeBuilder<HrNotificationsVw> entity)
        {
            entity.ToView("HR_Notifications_VW");
        }
    }
    
}
