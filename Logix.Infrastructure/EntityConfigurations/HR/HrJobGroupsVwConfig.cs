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
    public class HrJobGroupsVwConfig : IEntityTypeConfiguration<HrJobGroupsVw>
    {
        public void Configure(EntityTypeBuilder<HrJobGroupsVw> entity)
        {
            entity.ToView("HR_Job_Groups_VW");
        }
    }
}
