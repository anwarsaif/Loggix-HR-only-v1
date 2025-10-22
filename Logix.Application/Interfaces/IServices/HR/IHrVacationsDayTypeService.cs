using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrVacationsDayTypeService : IGenericQueryService<HrVacationsDayTypeDto, HrVacationsDayType>, IGenericWriteService<HrVacationsDayTypeDto, HrVacationsDayTypeEditDto>
    {

    } 

}
