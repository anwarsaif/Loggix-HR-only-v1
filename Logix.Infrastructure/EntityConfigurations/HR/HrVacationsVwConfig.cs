using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrVacationsVwConfig : IEntityTypeConfiguration<HrVacationsVw>
    {
        public void Configure(EntityTypeBuilder<HrVacationsVw> entity)
        {
            entity.ToView("HR_Vacations_VW");
        }
    }



}


