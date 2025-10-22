using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrLicenseRepository : GenericRepository<HrLicense>, IHrLicenseRepository
    {
        private readonly ApplicationDbContext _context;

        public HrLicenseRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
