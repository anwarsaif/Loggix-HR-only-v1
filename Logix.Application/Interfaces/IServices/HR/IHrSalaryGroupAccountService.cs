using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrSalaryGroupAccountService : IGenericQueryService<HrSalaryGroupAccountDto, HrSalaryGroupAccount>, IGenericWriteService<HrSalaryGroupAccountDto, HrSalaryGroupAccountEditDto>
    {

    }


}
