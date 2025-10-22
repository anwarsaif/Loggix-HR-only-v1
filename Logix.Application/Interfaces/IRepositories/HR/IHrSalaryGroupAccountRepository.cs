using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrSalaryGroupAccountRepository : IGenericRepository<HrSalaryGroupAccount>
    {
        Task<IResult<CheckSalaryGroupDto>> CheckSalaryGroup(long msid);

    }

}
