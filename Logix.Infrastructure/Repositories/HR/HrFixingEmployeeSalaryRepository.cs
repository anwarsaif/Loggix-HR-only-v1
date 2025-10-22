using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrFixingEmployeeSalaryRepository : GenericRepository<HrFixingEmployeeSalary, HrFixingEmployeeSalaryVw>, IHrFixingEmployeeSalaryRepository
    {
        private readonly ApplicationDbContext context;

        public HrFixingEmployeeSalaryRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }


    } 
}
