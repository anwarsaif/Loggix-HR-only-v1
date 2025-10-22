using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrLoanPaymentVwConfig : IEntityTypeConfiguration<HrLoanPaymentVw>
    {
        public void Configure(EntityTypeBuilder<HrLoanPaymentVw> entity)
        {
            entity.ToView("HR_Loan_Payment_VW");
        }
    }

}
