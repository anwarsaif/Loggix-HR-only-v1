using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrLoanPaymentConfig : IEntityTypeConfiguration<HrLoanPayment>
    {
        public void Configure(EntityTypeBuilder<HrLoanPayment> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        }
    }

}
