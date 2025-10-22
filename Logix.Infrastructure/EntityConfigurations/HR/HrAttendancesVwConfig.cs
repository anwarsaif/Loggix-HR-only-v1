using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAttendancesVwConfig : IEntityTypeConfiguration<HrAttendancesVw>
    {
        public void Configure(EntityTypeBuilder<HrAttendancesVw> entity)
        {
            entity.ToView("HR_Attendances_VW");

        }
    } 



}


