using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrTimeZoneConfig : IEntityTypeConfiguration<HrTimeZone>
    {
        public void Configure(EntityTypeBuilder<HrTimeZone> entity)
        {

            entity.HasKey(e => e.Id).HasName("PK__HR_TimeZ__3214EC2751A4242E");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

        }
    }



}


