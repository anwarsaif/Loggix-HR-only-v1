using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrEmpWorkTimeVwConfig : IEntityTypeConfiguration<HrEmpWorkTimeVw>
    {
        public void Configure(EntityTypeBuilder<HrEmpWorkTimeVw> entity)
        {
            entity.ToView("HR_Emp_Work_Time_VW");
        }
    }

}
