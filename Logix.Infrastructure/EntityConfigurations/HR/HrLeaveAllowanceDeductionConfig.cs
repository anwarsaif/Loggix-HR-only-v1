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
    public class HrLeaveAllowanceDeductionConfig : IEntityTypeConfiguration<HrLeaveAllowanceDeduction>
    {
        public void Configure(EntityTypeBuilder<HrLeaveAllowanceDeduction> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FixedOrTemporary).HasDefaultValue(1);
        }
    }
}
