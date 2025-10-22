using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrArchiveFilesDetailService : IGenericQueryService<HrArchiveFilesDetailDto, HrArchiveFilesDetailsVw>, IGenericWriteService<HrArchiveFilesDetailDto, HrArchiveFilesDetailEditDto>
    {

    }
}
