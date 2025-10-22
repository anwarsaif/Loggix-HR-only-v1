using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrAttShiftCloseService : IGenericQueryService<HrAttShiftCloseDto, HrAttShiftClose>, IGenericWriteService<HrAttShiftCloseDto, HrAttShiftCloseEditDto>
    {

    }
   
}
