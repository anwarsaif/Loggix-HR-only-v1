using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAttShiftTimeTableVwConfig : IEntityTypeConfiguration<HrAttShiftTimeTableVw>
    {
        public void Configure(EntityTypeBuilder<HrAttShiftTimeTableVw> entity)
        {
            entity.ToView("HR_Att_Shift_TimeTable_VW");

        }
    }
}


