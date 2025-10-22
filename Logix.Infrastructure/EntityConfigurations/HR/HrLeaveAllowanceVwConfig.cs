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
    public class HrLeaveAllowanceVwConfig : IEntityTypeConfiguration<HrLeaveAllowanceVw>
    {
        public void Configure(EntityTypeBuilder<HrLeaveAllowanceVw> entity)
        {
            entity.ToView("HR_Leave_Allowance_VW");
        }
    }
}
