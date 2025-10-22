using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrContractesVwConfig : IEntityTypeConfiguration<HrContractesVw>
    {
        public void Configure(EntityTypeBuilder<HrContractesVw> entity)
        {
            entity.ToView("HR_Contractes_VW");

        }
    } 
}


