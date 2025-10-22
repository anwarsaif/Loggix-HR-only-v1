using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrIncrementService : IGenericQueryService<HrIncrementDto, HrIncrementsVw>, IGenericWriteService<HrIncrementDto, HrIncrementEditDto>
    {
        Task<IResult<string>> ApplyIncrement(long IncrementId, int TransTypeID, CancellationToken cancellationToken = default);
        Task<IResult<HrIncrementDto>> Add(HrIncrementsAddDto Entity, CancellationToken cancellationToken = default);
        Task<IResult<IEnumerable<HrEmployeeIncremenResultDto>>> IncrementsEvaluationsSearch(IncrementsBothFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<string>> MakeApprove(List<MakeApproveDto> entities, CancellationToken cancellationToken = default);
		Task<IResult<List<HrIncrementsVw>>> Search(HrIncrementFilterDto filter, CancellationToken cancellationToken = default);

	}


}
