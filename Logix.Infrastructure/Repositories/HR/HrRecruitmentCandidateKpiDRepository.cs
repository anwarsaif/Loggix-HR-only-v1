using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrRecruitmentCandidateKpiDRepository : GenericRepository<HrRecruitmentCandidateKpiD>, IHrRecruitmentCandidateKpiDRepository
    {
        private readonly ApplicationDbContext _context;

        public HrRecruitmentCandidateKpiDRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<HrRecruitmentCandidateKpiDVw>> GetAllVW(Expression<Func<HrRecruitmentCandidateKpiDVw, bool>> expression)
        {
            return await _context.Set<HrRecruitmentCandidateKpiDVw>().Where(expression).AsNoTracking().ToListAsync();
        }


    }



}
