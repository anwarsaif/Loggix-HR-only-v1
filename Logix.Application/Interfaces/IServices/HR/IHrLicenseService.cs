using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrLicenseService : IGenericQueryService<HrLicenseDto, HrLicensesVw>, IGenericWriteService<HrLicenseDto, HrLicenseEditDto>
    {

    }


}
