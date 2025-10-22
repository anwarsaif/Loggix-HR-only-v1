using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrDirectJobVwConfig : IEntityTypeConfiguration<HrDirectJobVw>
    {
        public void Configure(EntityTypeBuilder<HrDirectJobVw> entity)
        {

            entity.ToView("HR_Direct_Job_VW");
        }
    }

}
