using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrIncrementsVwConfig : IEntityTypeConfiguration<HrIncrementsVw>
    {
        public void Configure(EntityTypeBuilder<HrIncrementsVw> entity)
        {

            entity.ToView("HR_Increments_VW");
        }
    }

}
