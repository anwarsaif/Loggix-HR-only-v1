using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrAssignmenService : IGenericQueryService<HrAssignmenDto, HrAssignmenVw>, IGenericWriteService<HrAssignmenDto, HrAssignmenEditDto>
    {
        Task<IResult<HrAssignmen2AddDto>> Assignment2Add(HrAssignmen2AddDto entity, CancellationToken cancellationToken = default);

    }
}
