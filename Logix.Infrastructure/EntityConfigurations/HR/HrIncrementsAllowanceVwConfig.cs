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
    public class HrIncrementsAllowanceVwConfig : IEntityTypeConfiguration<HrIncrementsAllowanceVw>
    {
        public void Configure(EntityTypeBuilder<HrIncrementsAllowanceVw> entity)
        {
                entity.ToView("HR_Increments_Allowance_VW");
        }
    }
}
