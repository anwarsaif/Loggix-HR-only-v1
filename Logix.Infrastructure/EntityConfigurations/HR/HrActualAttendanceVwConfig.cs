using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrActualAttendanceVwConfig : IEntityTypeConfiguration<HrActualAttendanceVw>
    {
        public void Configure(EntityTypeBuilder<HrActualAttendanceVw> entity)
        {
            entity.ToView("HR_ActualAttendance_VW", "dbo");

            entity.Property(e => e.Date).IsFixedLength();
        }
    }



}


