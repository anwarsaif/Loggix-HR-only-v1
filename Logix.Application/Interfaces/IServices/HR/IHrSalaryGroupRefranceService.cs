using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrSalaryGroupRefranceService : IGenericQueryService<HrSalaryGroupRefranceDto, HrSalaryGroupRefranceVw>, IGenericWriteService<HrSalaryGroupRefranceDto, HrSalaryGroupRefranceEditDto>
    {

    }


}
