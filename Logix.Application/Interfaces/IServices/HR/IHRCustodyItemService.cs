using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrCustodyItemService : IGenericQueryService<HrCustodyItemDto, HrCustodyItemsVw>, IGenericWriteService<HrCustodyItemDto, HrCustodyItemEditDto>
    {

    }
   
}
