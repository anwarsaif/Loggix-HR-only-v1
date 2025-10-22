using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrAttShiftRepository : IGenericRepository<HrAttShift>
    {
        Task<string> GetShiftNameAsync(long empId);

    }

}
