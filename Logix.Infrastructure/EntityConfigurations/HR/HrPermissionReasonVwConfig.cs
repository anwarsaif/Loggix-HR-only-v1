using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPermissionReasonVwConfig : IEntityTypeConfiguration<HrPermissionReasonVw>
    {
        public void Configure(EntityTypeBuilder<HrPermissionReasonVw> entity)
        {
            entity.ToView("HR_Permission_Reason_VW", "dbo");
        }
    }



}


