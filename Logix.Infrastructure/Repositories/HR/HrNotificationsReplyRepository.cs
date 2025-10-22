using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrNotificationsReplyRepository : GenericRepository<HrNotificationsReply>, IHrNotificationsReplyRepository
    {
        private readonly ApplicationDbContext _context;

        public HrNotificationsReplyRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }  
   


}
