using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrDisciplinaryActionTypeService : IGenericQueryService<HrDisciplinaryActionTypeDto, HrDisciplinaryActionType>, IGenericWriteService<HrDisciplinaryActionTypeDto, HrDisciplinaryActionTypeEditDto>
    {

    }


}
