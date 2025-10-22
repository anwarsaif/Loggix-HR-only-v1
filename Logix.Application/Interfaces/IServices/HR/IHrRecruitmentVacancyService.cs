using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrRecruitmentVacancyService : IGenericQueryService<HrRecruitmentVacancyDto, HrRecruitmentVacancyVw>, IGenericWriteService<HrRecruitmentVacancyDto, HrRecruitmentVacancyEditDto>
    {
        Task<IResult<IEnumerable<HrRecruitmentVacancyVwDto>>> GetAllHRRecruitmentVacancy(CancellationToken cancellationToken = default);
        Task<IResult<IEnumerable<HrRecruitmentVacancyVwDto>>> SearchHRRecruitmentVacancy(HrRecruitmentVacancyFilterDto filter, CancellationToken cancellationToken = default);


    }


}
