using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrRecruitmentApplicationService : IGenericQueryService<HrRecruitmentApplicationDto, HrRecruitmentApplicationVw>, IGenericWriteService<HrRecruitmentApplicationDto, HrRecruitmentApplicationEditDto>
    {

    }


}
