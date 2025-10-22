using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrEmpWorkTimeConfig : IEntityTypeConfiguration<HrEmpWorkTime>
    {
        public void Configure(EntityTypeBuilder<HrEmpWorkTime> entity)
        {
            entity.HasKey(e => e.EmpWorkId)
                     .HasName("PK_HR_Emp_Word_Time");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
        }
    }

}
