using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrDecisionService : IGenericQueryService<HrDecisionDto, HrDecisionsVw>, IGenericWriteService<HrDecisionDto, HrDecisionEditDto>
    {
        Task<IResult<string>> SendEmail(long Id, CancellationToken cancellationToken = default);

    }

}
