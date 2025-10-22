using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrDecisionsEmployeeService : IGenericQueryService<HrDecisionsEmployeeDto, HrDecisionsEmployeeVw>, IGenericWriteService<HrDecisionsEmployeeDto, HrDecisionsEmployeeEditDto>
    {

    } 
   
}
