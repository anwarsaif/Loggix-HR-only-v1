using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPreparationSalariesVwConfig : IEntityTypeConfiguration<HrPreparationSalariesVw>
    {
        public void Configure(EntityTypeBuilder<HrPreparationSalariesVw> entity)
        {
            entity.ToView("HR_Preparation_Salaries_VW");

            entity.Property(e => e.MsMonth).IsFixedLength();

        }
    } 
    
}
