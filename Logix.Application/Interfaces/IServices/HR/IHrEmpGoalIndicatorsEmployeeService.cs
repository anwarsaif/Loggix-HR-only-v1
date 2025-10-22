using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrEmpGoalIndicatorsEmployeeService : IGenericQueryService<HrEmpGoalIndicatorsEmployeeDto, HrEmpGoalIndicatorsEmployeeVw>, IGenericWriteService<HrEmpGoalIndicatorsEmployeeDto, HrEmpGoalIndicatorsEmployeeEditDto>
    {

    } 

}
