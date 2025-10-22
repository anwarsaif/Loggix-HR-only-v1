using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrRequestGoalsEmployeeDetailsVwConfig : IEntityTypeConfiguration<HrRequestGoalsEmployeeDetailsVw>
    {
        public void Configure(EntityTypeBuilder<HrRequestGoalsEmployeeDetailsVw> entity)
        {
            entity.ToView("HR_Request_Goals_Employee_Details_VW");
        }
    }
}



