using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrCompensatoryVacationService : IGenericQueryService<HrCompensatoryVacationDto, HrCompensatoryVacationsVw>, IGenericWriteService<HrCompensatoryVacationDto, HrCompensatoryVacationEditDto>
    {
        Task<IResult< bool>> AddNewHrCompensatoryVacation(HrCompensatoryVacationAddDto entity, CancellationToken cancellationToken = default);
        Task<IResult< string>> GetVacationDaysCount(string SDate,string EDate, int VacationTypeId);
        Task<IResult< string>> CompensatoryVacationApprove(long CompensatoryId);

    } 
   
}
