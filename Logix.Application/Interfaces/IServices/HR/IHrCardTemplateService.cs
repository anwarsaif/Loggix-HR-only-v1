using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrCardTemplateService : IGenericQueryService<HrCardTemplateDto, HrCardTemplate>, IGenericWriteService<HrCardTemplateDto, HrCardTemplateEditDto>
    {
        Task<IResult> UpdateTemplateStatus(long templateId,int status, CancellationToken cancellationToken = default);

    }


}
