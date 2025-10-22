using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrEmpStatusHistoryConfig : IEntityTypeConfiguration<HrEmpStatusHistory>
    {
        public void Configure(EntityTypeBuilder<HrEmpStatusHistory> entity)
        {
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

        }
    } 
}


