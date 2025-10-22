using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrDependentRepository : GenericRepository<HrDependent>, IHrDependentRepository
    {
        private readonly ApplicationDbContext _context;

        public HrDependentRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
