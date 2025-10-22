using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrCostTypeVwConfig : IEntityTypeConfiguration<HrCostTypeVw>
    {
        public void Configure(EntityTypeBuilder<HrCostTypeVw> entity)
        {
            entity.ToView("HR_Cost_Type_VW");

        }
    } 
}


