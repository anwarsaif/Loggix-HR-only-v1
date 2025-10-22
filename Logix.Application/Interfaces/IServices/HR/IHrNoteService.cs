using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrNoteService : IGenericQueryService<HrNoteDto, HrNoteVw>, IGenericWriteService<HrNoteDto, HrNoteEditDto>
    {

    }


}
