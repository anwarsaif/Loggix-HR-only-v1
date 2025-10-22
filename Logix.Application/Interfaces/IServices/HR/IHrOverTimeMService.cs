using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System.Collections.Generic;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrOverTimeMService : IGenericQueryService<HrOverTimeMDto, HrOverTimeMVw>, IGenericWriteService<HrOverTimeMDto, HrOverTimeMEditDto>
    {
        Task<IResult<string>> Add(HrOverTimeMAddDto entity, CancellationToken cancellationToken = default);
        Task<IResult<string>> AddUsingExcel(HrOverTimeMAddUsingExcelDto entity, CancellationToken cancellationToken = default);
        Task<IResult<string>> Add4(HrOverTimeMAdd4Dto entity, CancellationToken cancellationToken = default);
        Task<IResult<HrEmpSalaryAndOverTimeDto>> getEmpSalaryAndOverData(string empCode,int TypeId, CancellationToken cancellationToken = default);

        Task<IResult<List<HrGetAttendanceButtonResult>>> GetAttendanceData(HrGetAttendanceButtonClickDto entity, CancellationToken cancellationToken = default);
        Task<IResult<string>> AddListOfOverTimeD(IEnumerable<HrOverTimeDDto> entities, CancellationToken cancellationToken = default);
		Task<IResult<List<HrOverTimeMVw>>> Search(HrOverTimeMFilterDto filter, CancellationToken cancellationToken = default);

	}


}
