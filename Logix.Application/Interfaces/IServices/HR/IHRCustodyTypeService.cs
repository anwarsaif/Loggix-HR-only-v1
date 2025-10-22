using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrCustodyTypeService : IGenericQueryService<HrCustodyTypeDto, HrCustodyType>, IGenericWriteService<HrCustodyTypeDto, HrCustodyTypeDto>
    {

    }
   
}
