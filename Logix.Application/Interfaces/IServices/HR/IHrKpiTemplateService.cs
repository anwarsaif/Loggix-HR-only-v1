using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrKpiTemplateService : IGenericQueryService<HrKpiTemplateDto, HrKpiTemplatesVw>, IGenericWriteService<HrKpiTemplateDto, HrKpiTemplateEditDto>
    {

    }
}
