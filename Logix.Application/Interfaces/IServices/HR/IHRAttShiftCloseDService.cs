using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrAttShiftCloseDService : IGenericQueryService<HrAttShiftCloseDDto, HrAttShiftCloseD>, IGenericWriteService<HrAttShiftCloseDDto, HrAttShiftCloseDEditDto>
    {

    } 
   
}
