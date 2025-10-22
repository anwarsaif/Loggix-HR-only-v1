using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrLoanVwConfig : IEntityTypeConfiguration<HrLoanVw>
    {
        public void Configure(EntityTypeBuilder<HrLoanVw> entity)
        {
            entity.ToView("HR_Loan_VW");
        }
    }



}


