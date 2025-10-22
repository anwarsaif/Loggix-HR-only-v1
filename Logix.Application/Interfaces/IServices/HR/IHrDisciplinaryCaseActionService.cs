using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrDisciplinaryCaseActionService : IGenericQueryService<HrDisciplinaryCaseActionDto, HrDisciplinaryCaseActionVw>, IGenericWriteService<HrDisciplinaryCaseActionDto, HrDisciplinaryCaseActionEditDto>
    {
        Task<IResult<decimal>> Apply_Policies(long Facility_ID, long Policie_ID, long Emp_ID);


    }


}
