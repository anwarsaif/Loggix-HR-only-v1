using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrAttShiftTimeTableService : IGenericQueryService<HrAttShiftTimeTableDto, HrAttShiftTimeTableVw>, IGenericWriteService<HrAttShiftTimeTableDto, HrAttShiftTimeTableEditDto>
    {

    }


}
