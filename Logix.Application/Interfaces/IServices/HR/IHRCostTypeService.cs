using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrCostTypeService : IGenericQueryService<HrCostTypeDto, HrCostTypeVw>, IGenericWriteService<HrCostTypeDto, HrCostTypeEditDto>
    {

    }    
   
}
