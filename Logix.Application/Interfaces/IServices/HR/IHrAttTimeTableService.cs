using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrAttTimeTableService : IGenericQueryService<HrAttTimeTableDto, HrAttTimeTableVw, HrAttTimeTable>, IGenericWriteService<HrAttTimeTableDto, HrAttTimeTableEditDto>
    {
        Task<IResult<List<HrAttTimeTableDto>>> Search(CancellationToken cancellationToken = default);

    }


}
