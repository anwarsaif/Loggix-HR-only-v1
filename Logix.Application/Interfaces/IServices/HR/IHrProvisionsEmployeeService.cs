using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrProvisionsEmployeeService : IGenericQueryService<HrProvisionsEmployeeDto, HrProvisionsEmployeeVw>, IGenericWriteService<HrProvisionsEmployeeDto, HrProvisionsEmployeeEditDto>
    {

    }

}
