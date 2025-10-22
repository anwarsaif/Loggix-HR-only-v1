using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrProvisionsVwConfig : IEntityTypeConfiguration<HrProvisionsVw>
    {
        public void Configure(EntityTypeBuilder<HrProvisionsVw> entity)
        {
            entity.ToView("HR_Provisions_VW", "dbo");

        }
    }



}


