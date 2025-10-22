using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrFlexibleWorkingService : IGenericQueryService<HrFlexibleWorkingDto, HrFlexibleWorkingVw>, IGenericWriteService<HrFlexibleWorkingDto, HrFlexibleWorkingEditDto>
    {
        Task<IResult<IEnumerable<HrFlexibleWorkingResultDto>>> SearchInAdd(HrFlexibleWorkingMasterFilterDto filter);
        Task<IResult<string>> ApproveWork(List<long> Ids, CancellationToken cancellationToken = default);

    }

}
