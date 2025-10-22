using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAttCheckShiftEmployeeVwConfig : IEntityTypeConfiguration<HrAttCheckShiftEmployeeVw>
    {
        public void Configure(EntityTypeBuilder<HrAttCheckShiftEmployeeVw> entity)
        {
            entity.ToView("HR_Att_Check_Shift_Employee_VW");

        }
    }
}


