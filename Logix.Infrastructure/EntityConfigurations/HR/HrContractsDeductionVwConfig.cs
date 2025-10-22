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
    public class HrContractsDeductionVwConfig : IEntityTypeConfiguration<HrContractsDeductionVw>
    {
        public void Configure(EntityTypeBuilder<HrContractsDeductionVw> entity)
        {
            entity.ToView("HR_Contracts_Deduction_VW");
        }
    }
}
