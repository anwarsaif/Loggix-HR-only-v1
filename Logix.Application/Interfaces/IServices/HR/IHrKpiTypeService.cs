using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrKpiTypeService : IGenericQueryService<HrKpiTypeDto, HrKpiType>, IGenericWriteService<HrKpiTypeDto, HrKpiTypeEditDto>
    {

    }


}
