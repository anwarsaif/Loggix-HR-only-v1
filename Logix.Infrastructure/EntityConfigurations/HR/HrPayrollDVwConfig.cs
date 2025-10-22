using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPayrollDVwConfig : IEntityTypeConfiguration<HrPayrollDVw>
    {
        public void Configure(EntityTypeBuilder<HrPayrollDVw> entity)
        {
            entity.ToView("HR_Payroll_D_VW");

            entity.Property(e => e.MsMonth).IsFixedLength();
        }
    }
}


