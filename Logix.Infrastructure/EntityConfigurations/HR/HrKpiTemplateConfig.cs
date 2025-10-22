using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrKpiTemplateConfig : IEntityTypeConfiguration<HrKpiTemplate>
    {
        public void Configure(EntityTypeBuilder<HrKpiTemplate> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            entity.Property(e => e.MaxKpis).HasDefaultValueSql("((0))");

            entity.Property(e => e.MinKpis).HasDefaultValueSql("((0))");

            entity.Property(e => e.ReadKpisId).HasDefaultValueSql("((0))");


        }
    }
}
