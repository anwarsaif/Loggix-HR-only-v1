using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrRequestDetailsService : IGenericQueryService<HrRequestDetailsDto, HrRequestDetailesVw>, IGenericWriteService<HrRequestDetailsDto, HrRequestDetailsEditDto>
    {

    }


}
