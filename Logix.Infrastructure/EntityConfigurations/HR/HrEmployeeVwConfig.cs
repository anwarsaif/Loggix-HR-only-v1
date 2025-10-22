using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrEmployeeVwConfig : IEntityTypeConfiguration<HrEmployeeVw>
    {
        public void Configure(EntityTypeBuilder<HrEmployeeVw> entity)
        {
            entity.ToView("HR_Employee_VW");
        }
    }
    
}
