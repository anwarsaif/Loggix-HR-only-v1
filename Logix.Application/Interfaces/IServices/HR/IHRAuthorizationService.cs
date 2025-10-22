using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrAuthorizationService : IGenericQueryService<HrAuthorizationDto, HrAuthorizationVw>, IGenericWriteService<HrAuthorizationDto, HrAuthorizationEditDto>
    {

    } 
   
}
