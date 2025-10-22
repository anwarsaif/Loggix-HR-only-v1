using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrTrainingBagService : GenericQueryService<HrTrainingBag, HrTrainingBagDto, HrTrainingBagVw>, IHrTrainingBagService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;

        public HrTrainingBagService(IQueryRepository<HrTrainingBag> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrTrainingBagDto>> Add(HrTrainingBagDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrTrainingBagDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                var item = _mapper.Map<HrTrainingBag>(entity);
                var newEntity = await hrRepositoryManager.HrTrainingBagRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrTrainingBagDto>(newEntity);


                return await Result<HrTrainingBagDto>.SuccessAsync(entityMap, localization.GetMessagesResource("success"));
            }
            catch (Exception exc)
            {

                return await Result<HrTrainingBagDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrTrainingBagRepository.GetById(Id);
            if (item == null) return Result<HrTrainingBagDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrTrainingBagRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrTrainingBagDto>.SuccessAsync(_mapper.Map<HrTrainingBagDto>(item), localization.GetMessagesResource("DeletedSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrTrainingBagDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrTrainingBagRepository.GetById(Id);
            if (item == null) return Result<HrTrainingBagDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrTrainingBagRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrTrainingBagDto>.SuccessAsync(_mapper.Map<HrTrainingBagDto>(item), localization.GetMessagesResource("DeletedSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrTrainingBagDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrTrainingBagEditDto>> Update(HrTrainingBagEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrTrainingBagEditDto>.FailAsync(localization.GetMessagesResource("UpdateNullEntity"));

            var item = await hrRepositoryManager.HrTrainingBagRepository.GetById(entity.Id);

            if (item == null) return await Result<HrTrainingBagEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            _mapper.Map(entity, item);

            hrRepositoryManager.HrTrainingBagRepository.Update(item);

            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrTrainingBagEditDto>.SuccessAsync(_mapper.Map<HrTrainingBagEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrTrainingBagEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}