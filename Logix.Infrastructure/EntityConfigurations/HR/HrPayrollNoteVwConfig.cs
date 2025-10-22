using Logix.Domain.Hr;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPayrollNoteVwConfig : IEntityTypeConfiguration<HrPayrollNoteVw>
    {
        public void Configure(EntityTypeBuilder<HrPayrollNoteVw> entity)
        {
            entity.ToView("HR_Payroll_Note_VW");

        }
    }

}
