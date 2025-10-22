using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrFileRepository : GenericRepository<HrFile>, IHrFileRepository
    {
        private readonly ApplicationDbContext _context;

        public HrFileRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
