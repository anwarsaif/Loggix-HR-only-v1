using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrTransferConfig : IEntityTypeConfiguration<HrTransfer>
    {
        public void Configure(EntityTypeBuilder<HrTransfer> entity)
        {

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
        }
    }

}
