using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrWorkExperienceService : IGenericQueryService<HrWorkExperienceDto, HrWorkExperience, HrWorkExperience>, IGenericWriteService<HrWorkExperienceDto, HrWorkExperienceEditDto>
    {

    }

}
