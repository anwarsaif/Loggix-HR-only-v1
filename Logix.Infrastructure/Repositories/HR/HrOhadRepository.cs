using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrOhadRepository : GenericRepository<HrOhad, HrOhadVw>, IHrOhadRepository
    {
        private readonly ApplicationDbContext _context;

        public HrOhadRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
