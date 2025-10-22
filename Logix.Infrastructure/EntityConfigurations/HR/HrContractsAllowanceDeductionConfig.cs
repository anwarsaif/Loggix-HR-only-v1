using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrContractsAllowanceDeductionConfig : IEntityTypeConfiguration<HrContractsAllowanceDeduction>
    {
        public void Configure(EntityTypeBuilder<HrContractsAllowanceDeduction> entity)
        {
            entity.Property(e => e.AdId).HasDefaultValue(0);
            entity.Property(e => e.AllDedId).HasDefaultValue(0L);
            entity.Property(e => e.Amount).HasDefaultValue(0m);
            entity.Property(e => e.ContractId).HasDefaultValue(0L);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedBy).HasDefaultValue(0L);
            entity.Property(e => e.NewAmount).HasDefaultValue(0m);
            entity.Property(e => e.NewRate).HasDefaultValue(0m);
            entity.Property(e => e.Rate).HasDefaultValue(0m);
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.TypeId).HasDefaultValue(0);
        }
    }
}
