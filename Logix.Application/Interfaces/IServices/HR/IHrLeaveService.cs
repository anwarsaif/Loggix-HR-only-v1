using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrLeaveService : IGenericQueryService<HrLeaveDto, HrLeaveVw>, IGenericWriteService<HrLeaveDto, HrLeaveEditDto>
    {
        Task<IResult<bool>> AddNewLeave(HrLeaveAddDto entity, CancellationToken cancellationToken = default);
        Task<IResult<Dictionary<string, string>>> PayrollTransfer(List<int> leaveIds, CancellationToken cancellationToken = default);
        Task<decimal> HR_End_Service_Due(string Curr_Date, long ID_Emp, int LeaveTypeId=0);
        Task<IResult<object>> GetEmployeeLeaveData(HrLeaveGetDataDto obj, CancellationToken cancellationToken = default);
		Task<IResult<List<HrLeaveFilterDto>>> Search(HrLeaveFilterDto filter, CancellationToken cancellationToken = default);
        Task<IResult<HrLeaveEditDto>> PayrollTransferFromEdit(HrLeaveEditDto entity, CancellationToken cancellationToken = default);



    }


}
