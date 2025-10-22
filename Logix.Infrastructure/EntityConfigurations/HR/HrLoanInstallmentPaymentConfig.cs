using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrLoanInstallmentPaymentConfig : IEntityTypeConfiguration<HrLoanInstallmentPayment>
    {
        public void Configure(EntityTypeBuilder<HrLoanInstallmentPayment> entity)
        {
            entity.Property(e => e.AmountPaid).HasDefaultValueSql("((0))");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            entity.Property(e => e.LoanPaymentId).HasDefaultValueSql("((0))");

        }
    }

}
