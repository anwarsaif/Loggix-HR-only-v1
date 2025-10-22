using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrDeductionVwConfig : IEntityTypeConfiguration<HrDeductionVw>
    {
        public void Configure(EntityTypeBuilder<HrDeductionVw> entity)
        {
            entity.ToView("HR_Deduction_VW");
        }
    }



}


