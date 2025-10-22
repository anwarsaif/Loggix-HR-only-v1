using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrRequestTypeRepository : GenericRepository<HrRequestType>, IHrRequestTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public HrRequestTypeRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
