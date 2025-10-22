using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IRepositories.HR
{
	public interface IHrJobAllowanceDeductionRepository : IGenericRepository<HrJobAllowanceDeduction>
    {
		Task<IEnumerable<HrJobAllowanceDeduction>> GetByJobIdAndTypeAsync(long jobId, int typeId);
	} 
   
}
