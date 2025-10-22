using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrTransfersVwConfig : IEntityTypeConfiguration<HrTransfersVw>
    {
        public void Configure(EntityTypeBuilder<HrTransfersVw> entity)
        {

            entity.ToView("HR_Transfers_VW");
        }
    }

}
