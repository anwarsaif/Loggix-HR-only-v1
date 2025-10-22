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
    public class HrPayrollTransactionTypeValueConfig : IEntityTypeConfiguration<HrPayrollTransactionTypeValue>
    {
        public void Configure(EntityTypeBuilder<HrPayrollTransactionTypeValue> entity) 
        {
            entity.HasKey(e => e.Id).HasName("PK__HR_Payro__3214EC278379272C");
        }
    }
}
