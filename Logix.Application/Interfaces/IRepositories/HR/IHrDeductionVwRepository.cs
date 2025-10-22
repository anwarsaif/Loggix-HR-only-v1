using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrDeductionVwRepository : IGenericRepository<HrDeductionVw>
    {
        Task<IEnumerable<HrDeductionDto>> GetDeductionFixedAndTemporary(long? empId, int? FinancelYear, string? monthCode, int? @Attendance, int IsPrepar = 0);
    }

}
