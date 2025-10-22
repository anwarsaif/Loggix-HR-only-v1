using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Logix.Infrastructure.Repositories.HR
{
	public class HrJobLevelsAllowanceDeductionRepository : GenericRepository<HrJobLevelsAllowanceDeduction>, IHrJobLevelsAllowanceDeductionRepository

	{
		private readonly ApplicationDbContext context;

		public HrJobLevelsAllowanceDeductionRepository(ApplicationDbContext context) : base(context)
        {
			this.context = context;
		}
		public async Task<IEnumerable<HrJobLevelsAllowanceDeduction>> GetByJobIdAndTypeAsync(long jobId, int typeId)
		{
			return await context.Set<HrJobLevelsAllowanceDeduction>()
		   .Where(j => j.LevelId == jobId && j.TypeId == typeId).ToListAsync();
		}

	}  


}
