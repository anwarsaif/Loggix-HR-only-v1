using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrFlexibleWorkingVwConfig : IEntityTypeConfiguration<HrFlexibleWorkingVw>
    {
        public void Configure(EntityTypeBuilder<HrFlexibleWorkingVw> entity)
        {
            entity.ToView("HR_Flexible_Working_VW", "dbo");

        }
    }



}


