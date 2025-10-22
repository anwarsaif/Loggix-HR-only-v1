using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrAttTimeTableDayService : IGenericQueryService<HrAttTimeTableDayDto, HrAttTimeTableDay>, IGenericWriteService<HrAttTimeTableDayDto, HrAttTimeTableDayEditDto>
    {

    }
}
