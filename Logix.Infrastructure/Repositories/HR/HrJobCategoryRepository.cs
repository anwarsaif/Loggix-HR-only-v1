using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
	public class HrJobCategoryRepository : GenericRepository<HrJobCategory>, IHrJobCategoryRepository
	{
        private readonly ApplicationDbContext _context;

        public HrJobCategoryRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
