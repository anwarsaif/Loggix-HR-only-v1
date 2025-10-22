using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Services.HR
{
    public class HrIncomeTaxPeriodService : GenericQueryService<HrIncomeTaxPeriod, HrIncomeTaxPeriodDto, HrIncomeTaxPeriod>, IHrIncomeTaxPeriodService
    {
        private readonly IMapper mapper;
        private readonly IAccRepositoryManager accRepositoryManager;
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly ICurrentData session;
        private readonly IUnitOfWork unitOfWork;

        public HrIncomeTaxPeriodService(IQueryRepository<HrIncomeTaxPeriod> queryRepository,
            IMapper mapper,
            IAccRepositoryManager accRepositoryManager,
            IHrRepositoryManager hrRepositoryManager,
            ILocalizationService localization,
            ICurrentData session,
            IUnitOfWork unitOfWork
            ) : base(queryRepository, mapper)
        {
            this.mapper = mapper;
            this.accRepositoryManager = accRepositoryManager;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
            this.session = session;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IResult<HrIncomeTaxPeriodDto>> Add(HrIncomeTaxPeriodDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                var incomeTask = await hrRepositoryManager.HrIncomeTaxRepository.GetById(entity.IncomeTaxId??0);
                if (incomeTask == null)
                    return await Result<HrIncomeTaxPeriodDto>.FailAsync(localization.GetMessagesResource("InCorrectId"));

                var item = mapper.Map<HrIncomeTaxPeriod>(entity);
                item.IncomeTaxId = entity.IncomeTaxId;
                item.FromDate = entity.FromDate?.Replace('/','-');
                item.ToDate = entity.ToDate?.Replace('/', '-');
                item.PersonalExemption = entity.PersonalExemption;
                item.CreatedBy = session.UserId;

                var newEntity = await hrRepositoryManager.HrIncomeTaxPeriodRepository.AddAndReturn(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = mapper.Map<HrIncomeTaxPeriodDto>(newEntity);
                return await Result<HrIncomeTaxPeriodDto>.SuccessAsync(entityMap, localization.GetMessagesResource("AddSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<HrIncomeTaxPeriodDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<HrIncomeTaxPeriodEditDto>> Update(HrIncomeTaxPeriodEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
