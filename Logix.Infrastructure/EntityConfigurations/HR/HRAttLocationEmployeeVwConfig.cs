using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrAttLocationEmployeeVwConfig : IEntityTypeConfiguration<HrAttLocationEmployeeVw>
    {
        public void Configure(EntityTypeBuilder<HrAttLocationEmployeeVw> entity)
        {
            entity.ToView("HR_Att_Location_Employee_VW");

        }
    }  
}


