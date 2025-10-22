using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPoliciesTypeConfig : IEntityTypeConfiguration<HrPoliciesType>
    {
        public void Configure(EntityTypeBuilder<HrPoliciesType> entity)
        {
            entity.HasKey(e => e.TypeId)
                      .HasName("PK_HR_Policies_Type_1");

            entity.Property(e => e.TypeId).ValueGeneratedNever();
        }
    }  
    
}
