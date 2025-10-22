using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrLoanInstallmentConfig : IEntityTypeConfiguration<HrLoanInstallment>
    {
        public void Configure(EntityTypeBuilder<HrLoanInstallment> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            entity.Property(e => e.IsPaid).HasDefaultValueSql("((0))");
        }
    }

}
