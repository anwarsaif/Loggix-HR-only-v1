using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrClearanceMonthConfig : IEntityTypeConfiguration<HrClearanceMonth>
    {
        public void Configure(EntityTypeBuilder<HrClearanceMonth> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.HasKey(e => e.Id); // Configure ID as primary key for EF

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            entity.Property(e => e.MsMonth).IsFixedLength();

            entity.Property(e => e.PayrollTypeId).HasDefaultValueSql("((1))");
        }
    }
}


