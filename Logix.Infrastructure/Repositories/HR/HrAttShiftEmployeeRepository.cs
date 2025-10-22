using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrAttShiftEmployeeRepository : GenericRepository<HrAttShiftEmployee, HrAttShiftEmployeeVw>, IHrAttShiftEmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public HrAttShiftEmployeeRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

    }

}
