using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrPsAllowanceDeductionService : GenericQueryService<HrPsAllowanceDeduction, HrPsAllowanceDeductionDto, HrPsAllowanceDeductionVw>, IHrPsAllowanceDeductionService
    {
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        public HrPsAllowanceDeductionService(IQueryRepository<HrPsAllowanceDeduction> queryRepository, IMainRepositoryManager mainRepositoryManager, ILocalizationService localization, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public Task<IResult<HrPsAllowanceDeductionDto>> Add(HrPsAllowanceDeductionDto entity, CancellationToken cancellationToken = default)
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

        public Task<IResult<HrPsAllowanceDeductionEditDto>> Update(HrPsAllowanceDeductionEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
    }
