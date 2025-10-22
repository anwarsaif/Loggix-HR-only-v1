using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrClearanceTypeVwConfig : IEntityTypeConfiguration<HrClearanceTypeVw>
    {
        public void Configure(EntityTypeBuilder<HrClearanceTypeVw> entity)
        {
            entity.ToView("HR_Clearance_Type_VW");
        }
    }
}


