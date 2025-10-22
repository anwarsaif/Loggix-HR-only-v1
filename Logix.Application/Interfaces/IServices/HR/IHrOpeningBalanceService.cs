using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrOpeningBalanceService : IGenericQueryService<HrOpeningBalanceDto, HrOpeningBalanceVw>, IGenericWriteService<HrOpeningBalanceDto, HrOpeningBalanceEditDto>
    {
        //Task<IResult<List<OtherBalanceDto>>> HR_Other_balances_SP(string currDate, long? empId, int TypeID, int CMDTYPE);
        Task<IResult<List<HrOpeningBalanceFilterDto>>> Search(HrOpeningBalanceFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<List<OtherBalanceDto>>> CurrBalanceSearch(CurrentBalanceFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<List<OtherBalanceDto>>> CurrBalanceAllSearch(CurrentBalanceFilterDto filter, CancellationToken cancellationToken = default);
    }
}
