using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrDelayService : IGenericQueryService<HrDelayDto, HrDelayVw>, IGenericWriteService<HrDelayDto, HrDelayEditDto>
    {
        Task<IResult<HrDelayDto>> DeleteAllSelected( List<long> Ids, CancellationToken cancellationToken = default);
        Task<IResult<HrDelayNonCheckoutDto>> DelayNonCheckout(HrDelayNonCheckoutDto entity, CancellationToken cancellationToken = default);
        Task<IResult<string>> MakeApprove(string EmpCode,string ApproveDate, string HoursMins, CancellationToken cancellationToken = default);
        Task<IResult<HrDelayDto>> Add(HrDelayAddDto entity, CancellationToken cancellationToken = default);
		Task<IResult<List<HrDelayFilterDto>>> Search(HrDelayFilterDto filter, CancellationToken cancellationToken = default);


	}
}
