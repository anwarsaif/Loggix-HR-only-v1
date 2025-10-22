using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrCustodyRefranceTypeRepository : GenericRepository<HrCustodyRefranceType>, IHrCustodyRefranceTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public HrCustodyRefranceTypeRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }  



}
