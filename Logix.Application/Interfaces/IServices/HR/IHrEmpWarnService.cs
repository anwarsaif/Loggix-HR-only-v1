using Logix.Application.DTOs.HR;
using Logix.Domain.ACC;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrEmpWarnService : IGenericQueryService<HrEmpWarnDto, HrEmpWarnVw>, IGenericWriteService<HrEmpWarnDto, HrEmpWarnEditDto>
    {

    }
}
