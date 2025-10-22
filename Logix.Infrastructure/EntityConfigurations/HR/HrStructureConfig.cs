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
    public class HrStructureConfig : IEntityTypeConfiguration<HrStructure>
    {
        public void Configure(EntityTypeBuilder<HrStructure> entity)
        {
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
        }
    }
}
