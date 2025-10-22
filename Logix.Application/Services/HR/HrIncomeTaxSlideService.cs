using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
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
    public class HrIncomeTaxSlideService : GenericQueryService<HrIncomeTaxSlide, HrIncomeTaxSlideDto>, IHrIncomeTaxSlideService
    {
        private readonly IMapper mapper;
        private readonly IAccRepositoryManager accRepositoryManager;
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly ICurrentData session;
        private readonly IUnitOfWork unitOfWork;

        public HrIncomeTaxSlideService(IQueryRepository<HrIncomeTaxSlide> queryRepository,
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

        public async Task<IResult<HrIncomeTaxSlideDto>> Add(HrIncomeTaxSlideDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrIncomeTaxSlideDto>.FailAsync(localization.GetMessagesResource("AddNullEntity"));

            try
            {
                var item = mapper.Map<HrIncomeTaxSlide>(entity);
                item.IncomeTaxPeriodId = entity.IncomeTaxPeriodId;
                item.TaxSlideOrderNo = entity.TaxSlideOrderNo;
                item.TaxSlideValue = entity.TaxSlideValue;
                item.TaxSlideRate = entity.TaxSlideRate;
                item.TaxSlideStartingFromTheSlideNo = entity.TaxSlideStartingFromTheSlideNo;
                item.TaxSlideNote = entity.TaxSlideNote;
                item.CreatedBy = session.UserId;
                item.CreatedOn = DateTime.Now;

                var newEntity = await hrRepositoryManager.HrIncomeTaxSlideRepository.AddAndReturn(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = mapper.Map<HrIncomeTaxSlideDto>(newEntity);
                return await Result<HrIncomeTaxSlideDto>.SuccessAsync(entityMap, localization.GetMessagesResource("AddSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<HrIncomeTaxSlideDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrIncomeTaxSlideRepository.GetById(Id);
            if (item == null) 
                return Result<HrIncomeTaxSlideDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

            try
            {
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrIncomeTaxSlideRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrIncomeTaxSlideDto>.SuccessAsync(mapper.Map<HrIncomeTaxSlideDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrIncomeTaxSlideDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrIncomeTaxSlideRepository.GetById(Id);
            if (item == null)
                return Result<HrIncomeTaxSlideDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

            try
            {
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrIncomeTaxSlideRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrIncomeTaxSlideDto>.SuccessAsync(mapper.Map<HrIncomeTaxSlideDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrIncomeTaxSlideDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public Task<IResult<HrIncomeTaxSlideEditDto>> Update(HrIncomeTaxSlideEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
