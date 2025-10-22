using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrDisciplinaryRuleVwConfig : IEntityTypeConfiguration<HrDisciplinaryRuleVw>
    {
        public void Configure(EntityTypeBuilder<HrDisciplinaryRuleVw> entity)
        {
            entity.ToView("HR_Disciplinary_Rule_VW");
        }
    } 
    
}
