using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPayrollVwConfig : IEntityTypeConfiguration<HrPayrollVw>
    {
        public void Configure(EntityTypeBuilder<HrPayrollVw> entity)
        {
            entity.ToView("HR_Payroll_VW");

            entity.Property(e => e.MsMonth).IsFixedLength();
        }
    } 
    
}
