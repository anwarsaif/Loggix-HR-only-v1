using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrEmployeeCostVwConfig : IEntityTypeConfiguration<HrEmployeeCostVw>
    {
        public void Configure(EntityTypeBuilder<HrEmployeeCostVw> entity)
        {
            entity.ToView("HR_Employee_Cost_VW");

        }
    }



}


