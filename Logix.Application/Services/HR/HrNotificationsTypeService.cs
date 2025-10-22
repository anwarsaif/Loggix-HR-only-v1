using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{

    public class HrNotificationsTypeService : GenericQueryService<HrNotificationsType, HrNotificationsTypeDto, HrNotificationsTypeVw>, IHrNotificationsTypeService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;

        public HrNotificationsTypeService(IQueryRepository<HrNotificationsType> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrNotificationsTypeDto>> Add(HrNotificationsTypeDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrNotificationsTypeDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {


                var item = _mapper.Map<HrNotificationsType>(entity);

                item.IsDeleted = false;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                item.FacilityId = session.FacilityId;
                var newEntity = await hrRepositoryManager.HrNotificationsTypeRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrNotificationsTypeDto>(newEntity);


                return await Result<HrNotificationsTypeDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception exc)
            {

                return await Result<HrNotificationsTypeDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrNotificationsTypeRepository.GetById(Id);
            if (item == null) return Result<HrNotificationsTypeDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            item.FacilityId = session.FacilityId;
            hrRepositoryManager.HrNotificationsTypeRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrNotificationsTypeDto>.SuccessAsync(_mapper.Map<HrNotificationsTypeDto>(item), localization.GetMessagesResource("DeletedSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrNotificationsTypeDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrNotificationsTypeRepository.GetById(Id);
            if (item == null) return Result<HrNotificationsTypeDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            item.FacilityId = session.FacilityId;
            hrRepositoryManager.HrNotificationsTypeRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrNotificationsTypeDto>.SuccessAsync(_mapper.Map<HrNotificationsTypeDto>(item), localization.GetMessagesResource("DeletedSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrNotificationsTypeDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrNotificationsTypeEditDto>> Update(HrNotificationsTypeEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrNotificationsTypeEditDto>.FailAsync(localization.GetMessagesResource("UpdateNullEntity"));

            try
            {
                var item = await hrRepositoryManager.HrNotificationsTypeRepository.GetById(entity.Id);

                if (item == null) return await Result<HrNotificationsTypeEditDto>.FailAsync($"{localization.GetMessagesResource("NoItemFoundToEdit")}");
                item.IsDeleted = false;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                item.FacilityId = session.FacilityId;
                item.IsActive = entity.IsActive;
                item.Detailes = entity.Detailes;

                hrRepositoryManager.HrNotificationsTypeRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrNotificationsTypeEditDto>.SuccessAsync(_mapper.Map<HrNotificationsTypeEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrNotificationsTypeEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }

}