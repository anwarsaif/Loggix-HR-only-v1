using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrCustodyTypeConfig : IEntityTypeConfiguration<HrCustodyType>
    {
        public void Configure(EntityTypeBuilder<HrCustodyType> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

        }
    }
}


