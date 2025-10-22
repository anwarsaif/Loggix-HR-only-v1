using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrCustodyService : IGenericQueryService<HrCustodyDto, HrCustodyVw>, IGenericWriteService<HrCustodyDto, HrCustodyEditDto>
    {

    } 
   
}
