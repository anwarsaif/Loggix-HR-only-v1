using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrOhadDetailsVwConfig : IEntityTypeConfiguration<HrOhadDetailsVw>
    {
        public void Configure(EntityTypeBuilder<HrOhadDetailsVw> entity)
        {

            entity.ToView("HR_OhadDetails_VW");
        }
    }

}
