using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrVacationsCatagoryService : IGenericQueryService<HrVacationsCatagoryDto, HrVacationsCatagory>, IGenericWriteService<HrVacationsCatagoryDto, HrVacationsCatagoryEditDto>
    {

    }
}
