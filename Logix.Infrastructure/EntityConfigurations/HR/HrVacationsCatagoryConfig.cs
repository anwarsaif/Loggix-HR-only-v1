using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrVacationsCatagoryConfig : IEntityTypeConfiguration<HrVacationsCatagory>
    {
        public void Configure(EntityTypeBuilder<HrVacationsCatagory> entity)
        {
            entity.Property(e => e.CatId).ValueGeneratedNever();

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
        }
    }



}


