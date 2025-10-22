using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrInsuranceEmpVwConfig : IEntityTypeConfiguration<HrInsuranceEmpVw>
    {
        public void Configure(EntityTypeBuilder<HrInsuranceEmpVw> entity)
        {
            entity.ToView("HR_Insurance_Emp_VW");
        }
    }
}


