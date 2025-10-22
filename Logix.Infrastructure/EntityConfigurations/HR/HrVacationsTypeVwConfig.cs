using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrVacationsTypeVwConfig : IEntityTypeConfiguration<HrVacationsTypeVw>
    {
        public void Configure(EntityTypeBuilder<HrVacationsTypeVw> entity)
        {
            entity.ToView("HR_Vacations_Type_VW");

            entity.Property(e => e.VacationTypeId).ValueGeneratedOnAdd();
        }
    }  
    
}
