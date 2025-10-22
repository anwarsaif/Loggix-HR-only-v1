using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrDecisionsTypeService : IGenericQueryService<HrDecisionsTypeDto, HrDecisionsTypeVw>, IGenericWriteService<HrDecisionsTypeDto, HrDecisionsTypeEditDto>
    {

    }


}
