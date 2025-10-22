using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrDisciplinaryActionTypeRepository : GenericRepository<HrDisciplinaryActionType>, IHrDisciplinaryActionTypeRepository
    {
        private readonly ApplicationDbContext _context;



        public HrDisciplinaryActionTypeRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }



}
