using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrRecruitmentCandidateKpiDService : GenericQueryService<HrRecruitmentCandidateKpiD, HrRecruitmentCandidateKpiDDto, HrRecruitmentCandidateKpiDVw>, IHrRecruitmentCandidateKpiDService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;

        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;



        public HrRecruitmentCandidateKpiDService(IQueryRepository<HrRecruitmentCandidateKpiD> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public Task<IResult<HrRecruitmentCandidateKpiDDto>> Add(HrRecruitmentCandidateKpiDDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<HrRecruitmentCandidateKpiDEditDto>> Update(HrRecruitmentCandidateKpiDEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}