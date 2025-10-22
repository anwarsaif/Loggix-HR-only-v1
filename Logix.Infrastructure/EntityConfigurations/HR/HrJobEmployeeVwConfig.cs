using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrJobEmployeeVwConfig : IEntityTypeConfiguration<HrJobEmployeeVw>
    {
        public void Configure(EntityTypeBuilder<HrJobEmployeeVw> entity)
        {
            entity.ToView("HR_Job_Employee_VW");

        }
    }  
    
}
