using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrVisaService : IGenericQueryService<HrVisaDto, HrVisaVw>, IGenericWriteService<HrVisaDto, HrVisaEditDto>
    {

    }

}
