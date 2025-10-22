using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrActualAttendanceConfig : IEntityTypeConfiguration<HrActualAttendance>
    {
        public void Configure(EntityTypeBuilder<HrActualAttendance> entity)
        {
            entity.Property(e => e.Date).IsFixedLength();

        }
    }



}


