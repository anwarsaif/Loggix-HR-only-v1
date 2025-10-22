using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrDisciplinaryActionTypeConfig : IEntityTypeConfiguration<HrDisciplinaryActionType>
    {
        public void Configure(EntityTypeBuilder<HrDisciplinaryActionType> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        }
    }
}