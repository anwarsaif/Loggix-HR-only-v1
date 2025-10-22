using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrRecruitmentCandidateKpiRepository : GenericRepository<HrRecruitmentCandidateKpi>, IHrRecruitmentCandidateKpiRepository
    {
        private readonly ApplicationDbContext _context;

        public HrRecruitmentCandidateKpiRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<HrRecruitmentCandidateKpiVw>> GetAllVW(Expression<Func<HrRecruitmentCandidateKpiVw, bool>> expression)
        {
            return await _context.Set<HrRecruitmentCandidateKpiVw>().Where(expression).AsNoTracking().ToListAsync();
        }

    }



}
