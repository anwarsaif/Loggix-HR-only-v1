using System.Linq.Expressions;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrVacationsRepository : IGenericRepository<HrVacation, HrVacationsVw>
    {
        Task<decimal> Vacation_Balance_FN(string Curr_Date, long ID_Emp);
        Task<decimal> Vacation_Balance2_FN(string Curr_Date, long ID_Emp, int VacationTypeId);
        Task<IEnumerable<HrVacationsVw>> GetAllFromView(Expression<Func<HrVacationsVw, bool>> expression);
        Task<long> Check_Have_Vacation_Type_Id(int? Vacation_Type_Id);
        Task<decimal> GetCountDays(string StartDate, string EndDate, int VacationTypeId);
    }

}
