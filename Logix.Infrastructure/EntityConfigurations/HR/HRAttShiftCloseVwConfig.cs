using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAttShiftCloseVwConfig : IEntityTypeConfiguration<HrAttShiftCloseVw>
    {
        public void Configure(EntityTypeBuilder<HrAttShiftCloseVw> entity)
        {
            entity.ToView("HR_Att_Shift_Close_Vw");
        }
    }
}


