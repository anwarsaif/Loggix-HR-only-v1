using Logix.Domain.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrPsAllowanceVwService :IGenericQueryService<HrPsAllowanceVw, HrPsAllowanceVw>, IGenericWriteService<HrPsAllowanceVw, HrPsAllowanceVw>
    {
    }
}
