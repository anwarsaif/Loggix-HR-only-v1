using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrCompetencesCatagoryService : IGenericQueryService<HrCompetencesCatagoryDto, HrCompetencesCatagory>, IGenericWriteService<HrCompetencesCatagoryDto, HrCompetencesCatagoryEditDto>
    {

    }


}
