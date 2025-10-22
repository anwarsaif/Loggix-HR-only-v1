using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrJobDescriptionService : IGenericQueryService<HrJobDescriptionDto, HrJobDescription>, IGenericWriteService<HrJobDescriptionDto, HrJobDescriptionEditDto>
    {

    }


}
