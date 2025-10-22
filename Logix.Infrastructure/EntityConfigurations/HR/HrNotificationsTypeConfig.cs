using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrNotificationsTypeConfig : IEntityTypeConfiguration<HrNotificationsType>
    {
        public void Configure(EntityTypeBuilder<HrNotificationsType> entity)
        {
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

        }
    }

}
