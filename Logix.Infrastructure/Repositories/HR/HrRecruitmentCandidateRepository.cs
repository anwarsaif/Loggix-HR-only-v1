using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrRecruitmentCandidateRepository : GenericRepository<HrRecruitmentCandidate, HrRecruitmentCandidateVw>, IHrRecruitmentCandidateRepository
    {
        private readonly ApplicationDbContext _context;

        public HrRecruitmentCandidateRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
