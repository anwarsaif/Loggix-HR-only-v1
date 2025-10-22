using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrEmpWarnVwConfig : IEntityTypeConfiguration<HrEmpWarnVw>
    {
        public void Configure(EntityTypeBuilder<HrEmpWarnVw> entity)
        {

            entity.ToView("HR_Emp_Warn_VW");
        }
    }

}
