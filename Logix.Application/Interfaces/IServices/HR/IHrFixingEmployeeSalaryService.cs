using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrFixingEmployeeSalaryService : IGenericQueryService<HrFixingEmployeeSalaryDto, HrFixingEmployeeSalaryVw>, IGenericWriteService<HrFixingEmployeeSalaryDto, HrFixingEmployeeSalaryEditDto>
    {

    }

}
