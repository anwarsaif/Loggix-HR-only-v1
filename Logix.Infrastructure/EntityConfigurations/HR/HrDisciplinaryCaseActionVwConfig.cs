using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrDisciplinaryCaseActionVwConfig : IEntityTypeConfiguration<HrDisciplinaryCaseActionVw>
    {
        public void Configure(EntityTypeBuilder<HrDisciplinaryCaseActionVw> entity)
        {
            entity.ToView("HR_Disciplinary_Case_Action_VW");
        }
    } 
    
}
