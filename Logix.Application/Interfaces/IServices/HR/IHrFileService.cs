using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrFileService : IGenericQueryService<HrFileDto, HrFile>, IGenericWriteService<HrFileDto, HrFileEditDto>
    {

    }

}
