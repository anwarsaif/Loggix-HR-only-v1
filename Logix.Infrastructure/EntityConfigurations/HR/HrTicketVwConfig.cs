using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logix.Infrastructure.EntityConfigurations.HR
{
    public class HrTicketVwConfig : IEntityTypeConfiguration<HrTicketVw>
    {
        public void Configure(EntityTypeBuilder<HrTicketVw> entity)
        {
            entity.ToView("HR_Ticket_VW");

        }
    } 
    
}
