using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrAttShiftRepository : GenericRepository<HrAttShift>, IHrAttShiftRepository
    {
        private readonly ApplicationDbContext context;

        public HrAttShiftRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<string> GetShiftNameAsync(long empId)
        {
            var result = string.Empty;
          var   getData=await (from shiftEmp in context.HrAttShiftEmployees
                          join shift in context.HrAttShifts on shiftEmp.ShitId equals shift.Id
                          where shiftEmp.EmpId == empId && shiftEmp.IsDeleted == false
                          select shift.Name).FirstOrDefaultAsync();
            result = getData ?? "";
            return result;
        }

    }



}
