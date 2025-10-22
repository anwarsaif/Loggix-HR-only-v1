using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPoliciesVwConfig : IEntityTypeConfiguration<HrPoliciesVw>
    {
        public void Configure(EntityTypeBuilder<HrPoliciesVw> entity)
        {
            entity.ToView("HR_Policies_VW");
        }
    }   
    
}
