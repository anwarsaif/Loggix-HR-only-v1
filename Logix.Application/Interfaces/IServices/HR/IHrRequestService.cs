using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrRequestService : IGenericQueryService<HrRequestDto, HrRequestVw>, IGenericWriteService<HrRequestDto, HrRequestEditDto>
    {
        Task<IResult<string>> Add(HrRequestAddDto entity, CancellationToken cancellationToken = default);
        Task<IResult<string>> UpdateRequest(HrRequestEditDto entity, CancellationToken cancellationToken = default);

    }


}
