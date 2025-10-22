using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrLoanConfig : IEntityTypeConfiguration<HrLoan>
    {
        public void Configure(EntityTypeBuilder<HrLoan> entity)
        {
            entity.Property(e => e.CreateInstallment).HasDefaultValue(false);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(false);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
        }
    }



}


