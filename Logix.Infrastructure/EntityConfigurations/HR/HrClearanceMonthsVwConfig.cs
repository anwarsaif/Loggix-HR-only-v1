using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrClearanceMonthsVwConfig : IEntityTypeConfiguration<HrClearanceMonthsVw>
    {
        public void Configure(EntityTypeBuilder<HrClearanceMonthsVw> entity)
        {
            entity.ToView("HR_Clearance_Months_VW");
            entity.Property(e => e.MsMonth).IsFixedLength();
        }
    }
}


