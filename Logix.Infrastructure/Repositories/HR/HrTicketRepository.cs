using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrTicketRepository : GenericRepository<HrTicket>, IHrTicketRepository
    {
        private readonly ApplicationDbContext context;

        public HrTicketRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }

    }
