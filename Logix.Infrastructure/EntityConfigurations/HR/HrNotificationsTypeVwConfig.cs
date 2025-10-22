using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrNotificationsTypeVwConfig : IEntityTypeConfiguration<HrNotificationsTypeVw>
    {
        public void Configure(EntityTypeBuilder<HrNotificationsTypeVw> entity)
        {
            entity.ToView("HR_Notifications_Type_VW");

        }
    }

}
