using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrJobProgramVwConfig : IEntityTypeConfiguration<HrJobProgramVw>
    {
        public void Configure(EntityTypeBuilder<HrJobProgramVw> entity)
        {
            entity.ToView("HR_Job_Program_VW");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        }
    }  
    
}
