using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Domain.PM;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrRecruitmentVacancyRepository : GenericRepository<HrRecruitmentVacancy>, IHrRecruitmentVacancyRepository
    {
        private readonly ApplicationDbContext _context;

        public HrRecruitmentVacancyRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<HrRecruitmentVacancyVw>> GetAllVW(Expression<Func<HrRecruitmentVacancyVw, bool>> expression)
        {
            return await _context.Set<HrRecruitmentVacancyVw>().Where(expression).AsNoTracking().ToListAsync();
        }
    }
}
