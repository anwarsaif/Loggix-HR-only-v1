using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrEmpGoalIndicatorsEmployeeService : GenericQueryService<HrEmpGoalIndicatorsEmployee, HrEmpGoalIndicatorsEmployeeDto, HrEmpGoalIndicatorsEmployeeVw>, IHrEmpGoalIndicatorsEmployeeService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper mapper;
        private readonly ICurrentData session;


        public HrEmpGoalIndicatorsEmployeeService(IQueryRepository<HrEmpGoalIndicatorsEmployee> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this.mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public Task<IResult<HrEmpGoalIndicatorsEmployeeDto>> Add(HrEmpGoalIndicatorsEmployeeDto entity, CancellationToken cancellationToken = default)
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

        public Task<IResult<HrEmpGoalIndicatorsEmployeeEditDto>> Update(HrEmpGoalIndicatorsEmployeeEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }

}