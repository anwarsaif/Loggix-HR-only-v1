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
    public class HrPayrollTransactionTypeValuesVwConfig : IEntityTypeConfiguration<HrPayrollTransactionTypeValuesVw>
    {
        public void Configure(EntityTypeBuilder<HrPayrollTransactionTypeValuesVw> entity)
        {
            entity.ToView("HR_Payroll_Transaction_Type_Values_VW");

            entity.Property(e => e.Name).IsFixedLength();
            entity.Property(e => e.Name2).IsFixedLength();
        }
    }
}
