using Logix.Application.DTOs.HR;
using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{

	public class HrAllowanceDeductionTempOrFixConfig : IEntityTypeConfiguration<HrAllowanceDeductionTempOrFix>

    {
        public void Configure(EntityTypeBuilder<HrAllowanceDeductionTempOrFix> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        }
    }
}


