using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrJobLevelService : IGenericQueryService<HrJobLevelDto, HrJobLevelsVw>, IGenericWriteService<HrJobLevelDto, HrJobLevelEditDto>
    {

    }


}
