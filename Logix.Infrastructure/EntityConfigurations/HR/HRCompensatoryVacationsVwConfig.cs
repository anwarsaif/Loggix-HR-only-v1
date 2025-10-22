using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrCompensatoryVacationsVwConfig : IEntityTypeConfiguration<HrCompensatoryVacationsVw>
    {
        public void Configure(EntityTypeBuilder<HrCompensatoryVacationsVw> entity)
        {
            entity.ToView("HR_Compensatory_Vacations_VW");

        }
    }  
}


