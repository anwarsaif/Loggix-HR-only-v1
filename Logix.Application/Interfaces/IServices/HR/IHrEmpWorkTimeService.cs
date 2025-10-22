using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrEmpWorkTimeService : IGenericQueryService<HrEmpWorkTimeDto, HrEmpWorkTimeVw>, IGenericWriteService<HrEmpWorkTimeDto, HrEmpWorkTimeEditDto>
    {

    }


}
