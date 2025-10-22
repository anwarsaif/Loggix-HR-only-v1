using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrClearanceTypeService : IGenericQueryService<HrClearanceTypeDto, HrClearanceTypeVw>, IGenericWriteService<HrClearanceTypeDto, HrClearanceTypeDto>
    {

    }  
   
}
