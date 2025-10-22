using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrClearanceMonthService : IGenericQueryService<HrClearanceMonthDto, HrClearanceMonthsVw>, IGenericWriteService<HrClearanceMonthDto, HrClearanceMonthEditDto>
    {

    }

}
