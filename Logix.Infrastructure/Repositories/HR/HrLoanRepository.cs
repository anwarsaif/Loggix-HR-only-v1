using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrLoanRepository : GenericRepository<HrLoan, HrLoanVw>, IHrLoanRepository
    {
        private readonly ApplicationDbContext _context;

        public HrLoanRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }


    }
}
