using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAttendanceConfig : IEntityTypeConfiguration<HrAttendance>
    {
        public void Configure(EntityTypeBuilder<HrAttendance> entity)
        {
            entity.Property(e => e.AllowTimeIn).HasDefaultValueSql("((0))");

            entity.Property(e => e.AllowTimeOut).HasDefaultValueSql("((0))");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
        }
    }



}


