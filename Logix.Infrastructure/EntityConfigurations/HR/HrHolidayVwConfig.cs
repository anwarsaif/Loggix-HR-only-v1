using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrHolidayVwConfig : IEntityTypeConfiguration<HrHolidayVw>
    {
        public void Configure(EntityTypeBuilder<HrHolidayVw> entity)
        {
            entity.ToView("HR_Holiday_VW");

            entity.Property(e => e.HolidayId).ValueGeneratedOnAdd();
        }
    }

}
