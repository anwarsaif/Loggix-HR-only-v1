using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrCheckInOutVwConfig : IEntityTypeConfiguration<HrCheckInOutVw>
    {
        public void Configure(EntityTypeBuilder<HrCheckInOutVw> entity)
        {
            entity.ToView("HR_CheckInOut_VW");
        }
    } 
}


