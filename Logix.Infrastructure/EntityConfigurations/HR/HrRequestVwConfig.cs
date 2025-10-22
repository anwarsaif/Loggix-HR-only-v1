using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrRequestVwConfig : IEntityTypeConfiguration<HrRequestVw>
    {
        public void Configure(EntityTypeBuilder<HrRequestVw> entity)
        {
            entity.ToView("HR_Request_VW");
        }
    }



}


