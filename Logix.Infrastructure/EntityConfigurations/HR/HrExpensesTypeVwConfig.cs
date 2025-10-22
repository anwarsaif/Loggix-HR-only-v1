using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrExpensesTypeVwConfig : IEntityTypeConfiguration<HrExpensesTypeVw>
    {
        public void Configure(EntityTypeBuilder<HrExpensesTypeVw> entity)
        {
            entity.ToView("HR_Expenses_Type_VW", "dbo");

        }
    }



}


