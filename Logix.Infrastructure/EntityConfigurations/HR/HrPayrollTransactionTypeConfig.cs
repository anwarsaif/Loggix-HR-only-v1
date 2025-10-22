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
    public class HrPayrollTransactionTypeConfig : IEntityTypeConfiguration<HrPayrollTransactionType>
    {
        public void Configure(EntityTypeBuilder<HrPayrollTransactionType> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__HR_Payro__3214EC27DA35796D");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).IsFixedLength();
            entity.Property(e => e.Name2).IsFixedLength();
        }
    }
}
