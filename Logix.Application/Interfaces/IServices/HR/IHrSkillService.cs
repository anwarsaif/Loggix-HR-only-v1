using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrSkillService : IGenericQueryService<HrSkillDto, HrSkillsVw>, IGenericWriteService<HrSkillDto, HrSkillEditDto>
    {

    }

}
