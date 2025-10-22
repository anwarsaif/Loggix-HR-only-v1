using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrExpensesEmployeesVwConfig : IEntityTypeConfiguration<HrExpensesEmployeesVw>
    {
        public void Configure(EntityTypeBuilder<HrExpensesEmployeesVw> entity)
        {
            entity.ToView("HR_Expenses_Employees_VW", "dbo");

        }
    }



}


