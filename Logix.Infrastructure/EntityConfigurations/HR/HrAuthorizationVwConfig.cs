using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAuthorizationVwConfig : IEntityTypeConfiguration<HrAuthorizationVw>
    {
        public void Configure(EntityTypeBuilder<HrAuthorizationVw> entity)
        {
            entity.ToView("HR_Authorization_VW");

        }
    }  
}


