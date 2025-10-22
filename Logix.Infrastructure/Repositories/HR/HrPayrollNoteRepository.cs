using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.Hr;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrPayrollNoteRepository : GenericRepository<HrPayrollNote>, IHrPayrollNoteRepository
    {
        private readonly ApplicationDbContext context;

        public HrPayrollNoteRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

    }



}
