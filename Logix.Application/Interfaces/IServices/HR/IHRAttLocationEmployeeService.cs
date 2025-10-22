using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrAttLocationEmployeeService : IGenericQueryService<HrAttLocationEmployeeDto, HrAttLocationEmployeeVw>, IGenericWriteService<HrAttLocationEmployeeDto, HrAttLocationEmployeeEditeDto>
    {
        Task<IResult<string>> Cancel(HrAttLocationEmployeeCancelDto entity, CancellationToken cancellationToken = default);

    }

}
