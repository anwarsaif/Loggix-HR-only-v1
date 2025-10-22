using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    //   اعتماد مسير الرواتب من قبل الإدارة المالية
    public class HrEvaluationAnnualIncreaseConfigService : GenericQueryService<HrEvaluationAnnualIncreaseConfig, HrEvaluationAnnualIncreaseConfigDto, HrEvaluationAnnualIncreaseConfig>, IHrEvaluationAnnualIncreaseConfigService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;

        public HrEvaluationAnnualIncreaseConfigService(IQueryRepository<HrEvaluationAnnualIncreaseConfig> queryRepository,
            IHrRepositoryManager hrRepositoryManager,
            IMainRepositoryManager mainRepositoryManager,
            ILocalizationService localization,
            IMapper mapper, ICurrentData session) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this.mainRepositoryManager = mainRepositoryManager;
            this.localization = localization;
            _mapper = mapper;

            this.session = session;
        }

        public async Task<IResult<HrEvaluationAnnualIncreaseConfigDto>> Add(HrEvaluationAnnualIncreaseConfigDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrEvaluationAnnualIncreaseConfigDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {


                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                entity.FacilityId = session.FacilityId;

                var item = _mapper.Map<HrEvaluationAnnualIncreaseConfig>(entity);
                var newEntity = await hrRepositoryManager.HrEvaluationAnnualIncreaseConfigRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrEvaluationAnnualIncreaseConfigDto>(newEntity);


                return await Result<HrEvaluationAnnualIncreaseConfigDto>.SuccessAsync(entityMap, localization.GetMessagesResource("success"));
            }
            catch (Exception exc)
            {

                return await Result<HrEvaluationAnnualIncreaseConfigDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {

                var item = await hrRepositoryManager.HrEvaluationAnnualIncreaseConfigRepository.GetById(Id);
                if (item == null) return Result<HrEvaluationAnnualIncreaseConfigDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrEvaluationAnnualIncreaseConfigRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrEvaluationAnnualIncreaseConfigDto>.SuccessAsync(_mapper.Map<HrEvaluationAnnualIncreaseConfigDto>(item), localization.GetMessagesResource("DeletedSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrEvaluationAnnualIncreaseConfigDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {

                var item = await hrRepositoryManager.HrEvaluationAnnualIncreaseConfigRepository.GetById(Id);
                if (item == null) return Result<HrEvaluationAnnualIncreaseConfigDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrEvaluationAnnualIncreaseConfigRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrEvaluationAnnualIncreaseConfigDto>.SuccessAsync(_mapper.Map<HrEvaluationAnnualIncreaseConfigDto>(item), localization.GetMessagesResource("DeletedSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrEvaluationAnnualIncreaseConfigDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }


        }

        public async Task<IResult<HrEvaluationAnnualIncreaseConfigEditDto>> Update(HrEvaluationAnnualIncreaseConfigEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrEvaluationAnnualIncreaseConfigEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            var item = await hrRepositoryManager.HrEvaluationAnnualIncreaseConfigRepository.GetById(Convert.ToInt64(entity.Id));

            if (item == null) return await Result<HrEvaluationAnnualIncreaseConfigEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            _mapper.Map(entity, item);
            item.FacilityId = session.FacilityId;
            hrRepositoryManager.HrEvaluationAnnualIncreaseConfigRepository.Update(item);

            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrEvaluationAnnualIncreaseConfigEditDto>.SuccessAsync(_mapper.Map<HrEvaluationAnnualIncreaseConfigEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                return await Result<HrEvaluationAnnualIncreaseConfigEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }
    }


}