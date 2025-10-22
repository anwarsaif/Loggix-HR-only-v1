using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAttShiftEmployeeVwConfig : IEntityTypeConfiguration<HrAttShiftEmployeeVw>
    {
        public void Configure(EntityTypeBuilder<HrAttShiftEmployeeVw> entity)
        {
            entity.ToView("HR_Att_Shift_Employee_VW");
        }
    }



}


