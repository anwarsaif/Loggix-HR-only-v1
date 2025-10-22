using AutoMapper;
using Logix.Application.Common;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrAttShiftEmployeeMVwService : GenericQueryService<HrAttShiftEmployeeMVw, HrAttShiftEmployeeMVw, HrAttShiftEmployeeMVw>, IHrAttShiftEmployeeMVwService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrAttShiftEmployeeMVwService(IQueryRepository<HrAttShiftEmployeeMVw> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }
    }
    }
