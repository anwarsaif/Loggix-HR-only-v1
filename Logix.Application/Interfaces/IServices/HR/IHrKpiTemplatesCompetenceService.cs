using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrKpiTemplatesCompetenceService : IGenericQueryService<HrKpiTemplatesCompetenceDto, HrKpiTemplatesCompetencesVw>, IGenericWriteService<HrKpiTemplatesCompetenceDto, HrKpiTemplatesCompetenceEditDto>
    {

    }
}
