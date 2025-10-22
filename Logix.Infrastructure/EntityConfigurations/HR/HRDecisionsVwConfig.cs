using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrDecisionsVwConfig : IEntityTypeConfiguration<HrDecisionsVw>
    {
        public void Configure(EntityTypeBuilder<HrDecisionsVw> entity)
        {
            entity.ToView("HR_Decisions_VW");

        }
    } 
}


