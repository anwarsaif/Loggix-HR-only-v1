using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAbsenceVwConfig : IEntityTypeConfiguration<HrAbsenceVw>
    {
        public void Configure(EntityTypeBuilder<HrAbsenceVw> entity)
        {
            entity.ToView("HR_Absence_VW");

            entity.Property(e => e.Type).IsFixedLength();
        }
    } 
}


