using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrVisitScheduleLocationVwConfig : IEntityTypeConfiguration<HrVisitScheduleLocationVw>
    {
        public void Configure(EntityTypeBuilder<HrVisitScheduleLocationVw> entity)
        {
            entity.ToView("HR_Visit_Schedule_Location_VW");
        }
    }



}


