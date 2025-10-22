using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrDecisionsTypeVwConfig : IEntityTypeConfiguration<HrDecisionsTypeVw>
    {
        public void Configure(EntityTypeBuilder<HrDecisionsTypeVw> entity)
        {
            entity.ToView("HR_Decisions_Type_VW");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        }
    }

}
