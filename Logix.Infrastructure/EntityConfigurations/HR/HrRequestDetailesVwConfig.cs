using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrRequestDetailesVwConfig : IEntityTypeConfiguration<HrRequestDetailesVw>
    {
        public void Configure(EntityTypeBuilder<HrRequestDetailesVw> entity)
        {
            entity.ToView("HR_Request_Detailes_VW");
        }
    }



}


