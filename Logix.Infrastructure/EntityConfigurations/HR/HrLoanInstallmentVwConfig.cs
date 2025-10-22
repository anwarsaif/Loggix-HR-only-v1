using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrLoanInstallmentVwConfig : IEntityTypeConfiguration<HrLoanInstallmentVw>
    {
        public void Configure(EntityTypeBuilder<HrLoanInstallmentVw> entity)
        {
            entity.ToView("HR_Loan_Installment_VW");

        }
    }

}
