using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrAllowanceVwRepository : IGenericRepository<HrAllowanceVw>
    {
        Task<IEnumerable<HrAllowanceDto>> GetAllowanceFixedAndTemporary(long? empId, int? FinancelYear, string? monthCode, int ?Attendance, int IsPrepar = 0);

    }

}
