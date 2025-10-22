using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrEmpGoalIndicatorsCompetenceService : GenericQueryService<HrEmpGoalIndicatorsCompetence, HrEmpGoalIndicatorsCompetenceDto, HrEmpGoalIndicatorsCompetenceDto>, IHrEmpGoalIndicatorsCompetenceService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper mapper;
        private readonly ICurrentData session;


        public HrEmpGoalIndicatorsCompetenceService(IQueryRepository<HrEmpGoalIndicatorsCompetence> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this.mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public Task<IResult<HrEmpGoalIndicatorsCompetenceDto>> Add(HrEmpGoalIndicatorsCompetenceDto entity, CancellationToken cancellationToken = default)
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

        public Task<IResult<HrEmpGoalIndicatorsCompetenceEditDto>> Update(HrEmpGoalIndicatorsCompetenceEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }

}