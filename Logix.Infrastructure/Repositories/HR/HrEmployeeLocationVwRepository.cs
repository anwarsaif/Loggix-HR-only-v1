using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Application.Services;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrEmployeeLocationVwRepository : GenericRepository<HrEmployeeLocationVw>, IHrEmployeeLocationVwRepository
    {
        private readonly ApplicationDbContext _context;

        public HrEmployeeLocationVwRepository(ApplicationDbContext context, ICurrentData session) : base(context)
        {
            this._context = context;
        }

    }



}
