using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPayrollCostcenterVwConfig : IEntityTypeConfiguration<HrPayrollCostcenterVw>
    {
        public void Configure(EntityTypeBuilder<HrPayrollCostcenterVw> entity)
        {
            entity.ToView("HR_Payroll_Costcenter_VW");

            entity.Property(e => e.MsMonth).IsFixedLength();

        }
    }
}


