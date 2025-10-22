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
    public class HrContractsAllowanceVwConfig : IEntityTypeConfiguration<HrContractsAllowanceVw>
    {
        public void Configure(EntityTypeBuilder<HrContractsAllowanceVw> entity)
        {
            entity.ToView("HR_Contracts_Allowance_VW");
        }
    }
}
