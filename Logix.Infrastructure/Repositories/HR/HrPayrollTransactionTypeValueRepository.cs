using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrPayrollTransactionTypeValueRepository : GenericRepository<HrPayrollTransactionTypeValue, HrPayrollTransactionTypeValuesVw>, IHrPayrollTransactionTypeValueRepository
    {
        private readonly ApplicationDbContext context;

        public HrPayrollTransactionTypeValueRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<string> save(HrPayrollTransactionTypeValue val)
        {
            try
            {
                context.HrPayrollTransactionTypeValues.Update(val);
                await context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

    }
}
