using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrEvaluationAnnualIncreaseConfigService : IGenericQueryService<HrEvaluationAnnualIncreaseConfigDto, HrEvaluationAnnualIncreaseConfig>, IGenericWriteService<HrEvaluationAnnualIncreaseConfigDto, HrEvaluationAnnualIncreaseConfigEditDto>
    {

    }
}
