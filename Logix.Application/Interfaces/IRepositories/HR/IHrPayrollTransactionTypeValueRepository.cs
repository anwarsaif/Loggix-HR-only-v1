using Logix.Domain.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrPayrollTransactionTypeValueRepository : IGenericRepository<HrPayrollTransactionTypeValue, HrPayrollTransactionTypeValuesVw>
    {
        Task<string> save(HrPayrollTransactionTypeValue val);
    }
}
