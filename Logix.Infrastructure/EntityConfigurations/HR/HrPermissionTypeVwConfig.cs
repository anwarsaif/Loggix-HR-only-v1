using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPermissionTypeVwConfig : IEntityTypeConfiguration<HrPermissionTypeVw>
    {
        public void Configure(EntityTypeBuilder<HrPermissionTypeVw> entity)
        {
            entity.ToView("HR_Permission_Type_VW", "dbo");
        }
    }



}


