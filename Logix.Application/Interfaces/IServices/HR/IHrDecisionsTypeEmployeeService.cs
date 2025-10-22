using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrDecisionsTypeEmployeeService : IGenericQueryService<HrDecisionsTypeEmployeeDto, HrDecisionsTypeEmployeeVw>, IGenericWriteService<HrDecisionsTypeEmployeeDto, HrDecisionsTypeEmployeeEditDto>
    {

    }

}
