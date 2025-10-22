using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAttActionConfig : IEntityTypeConfiguration<HrAttAction>
    {
        public void Configure(EntityTypeBuilder<HrAttAction> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Time).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.EmpId).HasDefaultValueSql("((0))");

            entity.Property(e => e.IsManual).HasDefaultValueSql("((0))");

            entity.Property(e => e.TypeId).HasDefaultValueSql("('I')");

            entity.Property(e => e.UserExtFmt).HasDefaultValueSql("((0))");

            entity.Property(e => e.WorkCode).HasDefaultValueSql("((0))");
        }
    } 
}


