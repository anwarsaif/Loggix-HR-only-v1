using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrVacationBalanceService : IGenericQueryService<HrVacationBalanceDto, HrVacationBalanceVw>, IGenericWriteService<HrVacationBalanceDto, HrVacationBalanceEditDto>
    {
        Task<IResult<IEnumerable<HrVacationBalanceALLFilterDto>>> VacationBalanceALL(HrVacationBalanceALLSendFilterDto filter);
       Task<IResult<HrVacationEmpBalanceDto>> HR_Vacation_Balance(HrVacationEmpBalanceDto filter);
		Task<IResult<List<HrVacationBalanceFilterDto>>> Search(HrVacationBalanceFilterDto filter, CancellationToken cancellationToken = default);
		Task<IResult<List<HrVacationEmpBalanceDto>>> VacationEmpBalanceSearch(HrVacationEmpBalanceDto filter, CancellationToken cancellationToken = default);

	}


}
