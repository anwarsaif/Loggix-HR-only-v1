using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IRepositories.HR
{
    public interface IHrGosiRepository : IGenericRepository<HrGosi>
    {
        Task<IEnumerable<HRGOSIAccEntryDto>> GetGosiEmployeeAcc(long gosiId, long facilityId);
        Task<IEnumerable<HRGOSIAccEntryDto>> GetHRGOSIAccbyCCID(long gosiId, long facilityId);
        Task<IEnumerable<HRGOSIAccEntryDto>> GetHRGOSIAccbyReferenceTypeID(long gosiId, long facilityId);

    }

}
