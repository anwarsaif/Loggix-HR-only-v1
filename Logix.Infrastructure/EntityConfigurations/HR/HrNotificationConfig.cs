using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrNotificationConfig : IEntityTypeConfiguration<HrNotification>
    {
        public void Configure(EntityTypeBuilder<HrNotification> entity)
        {
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
        }
    }
    
}
