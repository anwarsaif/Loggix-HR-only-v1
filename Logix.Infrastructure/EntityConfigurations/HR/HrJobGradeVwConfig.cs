using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrJobGradeVwConfig : IEntityTypeConfiguration<HrJobGradeVw>
    {
        public void Configure(EntityTypeBuilder<HrJobGradeVw> entity)
        {
            entity.ToView("HR_Job_Grade_VW");

        }
    } 

}
