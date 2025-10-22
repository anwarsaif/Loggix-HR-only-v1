using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrExpensesScheduleConfig : IEntityTypeConfiguration<HrExpensesSchedule>
    {
        public void Configure(EntityTypeBuilder<HrExpensesSchedule> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

        }
    } 
}


