using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrDependentsVwConfig : IEntityTypeConfiguration<HrDependentsVw>
    {
        public void Configure(EntityTypeBuilder<HrDependentsVw> entity)
        {

            entity.ToView("HR_Dependents_VW");
        }
    }

}
