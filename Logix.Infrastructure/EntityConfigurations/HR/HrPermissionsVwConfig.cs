using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPermissionsVwConfig : IEntityTypeConfiguration<HrPermissionsVw>
    {
        public void Configure(EntityTypeBuilder<HrPermissionsVw> entity)
        {
            entity.ToView("HR_Permissions_VW");

        }
    }
}


