using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrEmpGoalIndicatorService : IGenericQueryService<HrEmpGoalIndicatorDto, HrEmpGoalIndicatorsVw>, IGenericWriteService<HrEmpGoalIndicatorDto, HrEmpGoalIndicatorEditDto>
    {
        Task<IResult<string>> Add(HrEmpGoalIndicatorAddDto entity, CancellationToken cancellationToken = default);
        Task<IResult<HrKpiTemplateDto>> AddEmployee(long? EmpId, long? TemplateId, CancellationToken cancellationToken = default);
        Task<IResult<string>> Edit(HrEmpGoalIndicatorAddDto entity, CancellationToken cancellationToken = default);

    }

}
