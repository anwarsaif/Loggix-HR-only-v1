using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
	public class HrJobGradeRepository : GenericRepository<HrJobGrade>, IHrJobGradeRepository
    {
        private readonly ApplicationDbContext _context;

        public HrJobGradeRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
