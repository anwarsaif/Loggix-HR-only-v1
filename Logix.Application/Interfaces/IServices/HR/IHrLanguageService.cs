using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrLanguageService : IGenericQueryService<HrLanguageDto, HrLanguagesVw>, IGenericWriteService<HrLanguageDto, HrLanguageEditDto>
    {

    }

}
