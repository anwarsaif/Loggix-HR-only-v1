using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrLoanInstallmentPaymentVwConfig : IEntityTypeConfiguration<HrLoanInstallmentPaymentVw>
    {
        public void Configure(EntityTypeBuilder<HrLoanInstallmentPaymentVw> entity)
        {
            entity.ToView("HR_Loan_Installment_Payment_VW");

        }
    }

}
