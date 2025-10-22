using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Logix.Domain.Main;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrDisciplinaryCaseService : IGenericQueryService<HrDisciplinaryCaseDto, HrDisciplinaryCase>, IGenericWriteService<HrDisciplinaryCaseDto, HrDisciplinaryCaseEditDto>
    {
        Task<IResult<HrDisciplinaryRuleDto>> AddNewRule(HrDisciplinaryRuleDto entity, CancellationToken cancellationToken = default);
        Task<IResult<HrDisciplinaryRuleEditDto>> EditRule(HrDisciplinaryRuleEditDto entity, CancellationToken cancellationToken = default);

    }


}
