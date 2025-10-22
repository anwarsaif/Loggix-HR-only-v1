using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrExpenseConfig : IEntityTypeConfiguration<HrExpense>
    {
        public void Configure(EntityTypeBuilder<HrExpense> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

        }
    }



}


