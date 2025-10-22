using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrEmployeeLocationVwConfig : IEntityTypeConfiguration<HrEmployeeLocationVw>
    {
        public void Configure(EntityTypeBuilder<HrEmployeeLocationVw> entity)
        {
            entity.ToView("HR_Employee_Location_Vw");

        }
    }  
}


