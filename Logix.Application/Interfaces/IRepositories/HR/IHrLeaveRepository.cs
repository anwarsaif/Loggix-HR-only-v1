using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrLeaveRepository : IGenericRepository<HrLeave, HrLeaveVw>
    {
        Task<decimal> HR_End_Service_Due(string Curr_Date, long ID_Emp, int LeaveTypeId = 0);

    }


}
