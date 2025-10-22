using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrNotificationsSettingService : GenericQueryService<HrNotificationsSetting, HrNotificationsSettingDto, HrNotificationsSettingVw>, IHrNotificationsSettingService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;

        public HrNotificationsSettingService(IQueryRepository<HrNotificationsSetting> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrNotificationsSettingDto>> Add(HrNotificationsSettingDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrNotificationsSettingDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                if (entity.IsActiveInt.HasValue && entity.IsActiveInt != 0)
                {
                    bool isActiveValue = entity.IsActiveInt.Value == 1;
                    entity.IsActive = isActiveValue;
                }
                entity.IsDeleted = false;
                entity.FacilityId = session.FacilityId;
                var item = _mapper.Map<HrNotificationsSetting>(entity);
                var newEntity = await hrRepositoryManager.HrNotificationsSettingRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrNotificationsSettingDto>(newEntity);


                return await Result<HrNotificationsSettingDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<HrNotificationsSettingDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrNotificationsSettingRepository.GetById(Id);
            if (item == null) return Result<HrNotificationsSettingDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrNotificationsSettingRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrNotificationsSettingDto>.SuccessAsync(_mapper.Map<HrNotificationsSettingDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrNotificationsSettingDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrNotificationsSettingRepository.GetById(Id);
            if (item == null) return Result<HrNotificationsSettingDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrNotificationsSettingRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrNotificationsSettingDto>.SuccessAsync(_mapper.Map<HrNotificationsSettingDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrNotificationsSettingDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrNotificationsSettingEditDto>> Update(HrNotificationsSettingEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrNotificationsSettingEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

            var item = await hrRepositoryManager.HrNotificationsSettingRepository.GetById(entity.Id);

            if (item == null) return await Result<HrNotificationsSettingEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
            if (entity.IsActiveInt.HasValue && entity.IsActiveInt != 0)
            {
                bool isActiveValue = entity.IsActiveInt.Value == 1;
                entity.IsActive = isActiveValue;
            }
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            _mapper.Map(entity, item);

            hrRepositoryManager.HrNotificationsSettingRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrNotificationsSettingEditDto>.SuccessAsync(_mapper.Map<HrNotificationsSettingEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrNotificationsSettingEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }

}