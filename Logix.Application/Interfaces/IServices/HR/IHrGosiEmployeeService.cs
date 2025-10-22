using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrGosiEmployeeService : IGenericQueryService<HrGosiEmployeeDto, HrGosiEmployeeVw>, IGenericWriteService<HrGosiEmployeeDto, HrGosiEmployeeEditDto>
    {

    }

}
