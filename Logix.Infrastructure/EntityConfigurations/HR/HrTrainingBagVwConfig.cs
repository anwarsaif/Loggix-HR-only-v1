using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrTrainingBagVwConfig : IEntityTypeConfiguration<HrTrainingBagVw>
    {
        public void Configure(EntityTypeBuilder<HrTrainingBagVw> entity)
        {
            entity.ToView("HR_Training_bag_VW");

        }
    }  
    
}
