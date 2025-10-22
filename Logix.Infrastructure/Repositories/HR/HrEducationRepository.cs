using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrEducationRepository : GenericRepository<HrEducation>, IHrEducationRepository
    {
        private readonly ApplicationDbContext _context;

        public HrEducationRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
