using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrRequestDetaileRepository : GenericRepository<HrRequestDetaile>, IHrRequestDetaileRepository
    {
        private readonly ApplicationDbContext _context;

        public HrRequestDetaileRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
