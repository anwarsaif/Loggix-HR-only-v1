using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrTrainingBagService : IGenericQueryService<HrTrainingBagDto, HrTrainingBagVw>, IGenericWriteService<HrTrainingBagDto, HrTrainingBagEditDto>
    {

    }


}
