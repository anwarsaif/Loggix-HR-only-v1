using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrExpensesVwConfig : IEntityTypeConfiguration<HrExpensesVw>
    {
        public void Configure(EntityTypeBuilder<HrExpensesVw> entity)
        {
            entity.ToView("HR_Expenses_VW", "dbo");

        }
    }



}


