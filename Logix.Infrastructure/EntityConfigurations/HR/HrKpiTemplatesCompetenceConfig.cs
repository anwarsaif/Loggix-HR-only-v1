using Logix.Application.DTOs.HR;
using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrKpiTemplatesCompetenceConfig : IEntityTypeConfiguration<HrKpiTemplatesCompetence>
    {
        public void Configure(EntityTypeBuilder<HrKpiTemplatesCompetence> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

        }
    }
}
