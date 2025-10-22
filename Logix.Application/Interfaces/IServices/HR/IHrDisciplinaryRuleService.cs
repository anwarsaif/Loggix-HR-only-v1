using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrDisciplinaryRuleService : IGenericQueryService<HrDisciplinaryRuleDto, HrDisciplinaryRuleVw>, IGenericWriteService<HrDisciplinaryRuleDto, HrDisciplinaryRuleEditDto>
    {

    }


}
