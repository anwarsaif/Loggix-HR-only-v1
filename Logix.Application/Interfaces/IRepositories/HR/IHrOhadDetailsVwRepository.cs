using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrOhadDetailsVwRepository : IGenericRepository<HrOhadDetailsVw>
    {
        Task<IEnumerable<HrOhadDetailsVw>> GetByInvestEmployeesAsync(long empId = 0, int transTypeId = 1);
    }

}
