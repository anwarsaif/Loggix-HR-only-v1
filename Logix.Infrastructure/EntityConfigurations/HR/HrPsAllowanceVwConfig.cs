using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrPsAllowanceVwConfig : IEntityTypeConfiguration<HrPsAllowanceVw>
    {
        public void Configure(EntityTypeBuilder<HrPsAllowanceVw> entity)
        {
            entity.ToView("HR_PS_Allowance_VW");
        }
    }
}
