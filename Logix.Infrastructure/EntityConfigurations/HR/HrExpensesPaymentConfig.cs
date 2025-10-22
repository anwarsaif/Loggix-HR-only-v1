using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrExpensesPaymentConfig : IEntityTypeConfiguration<HrExpensesPayment>
    {
        public void Configure(EntityTypeBuilder<HrExpensesPayment> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

        }
    } 
}


