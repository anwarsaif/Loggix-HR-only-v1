using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrExpensesEmployeeConfig : IEntityTypeConfiguration<HrExpensesEmployee>
    {
        public void Configure(EntityTypeBuilder<HrExpensesEmployee> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

        }
    }



}


