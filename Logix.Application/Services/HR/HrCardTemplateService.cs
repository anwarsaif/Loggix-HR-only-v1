using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrCardTemplateService : GenericQueryService<HrCardTemplate, HrCardTemplateDto, HrCardTemplate>, IHrCardTemplateService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;

        public HrCardTemplateService(IQueryRepository<HrCardTemplate> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrCardTemplateDto>> Add(HrCardTemplateDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrCardTemplateDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {
                entity.FacilityId = session.FacilityId;
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;

                var item = _mapper.Map<HrCardTemplate>(entity);
                var newEntity = await hrRepositoryManager.HrCardTemplateRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrCardTemplateDto>(newEntity);


                return await Result<HrCardTemplateDto>.SuccessAsync(entityMap, localization.GetMessagesResource("success"));
            }
            catch (Exception exc)
            {

                return await Result<HrCardTemplateDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrCardTemplateRepository.GetById(Id);
            if (item == null) return Result<HrCardTemplateDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrCardTemplateRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrCardTemplateDto>.SuccessAsync(_mapper.Map<HrCardTemplateDto>(item), localization.GetMessagesResource("DeletedSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrCardTemplateDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrCardTemplateRepository.GetById(Id);
            if (item == null) return Result<HrCardTemplateDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrCardTemplateRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrCardTemplateDto>.SuccessAsync(_mapper.Map<HrCardTemplateDto>(item), localization.GetMessagesResource("DeletedSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrCardTemplateDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrCardTemplateEditDto>> Update(HrCardTemplateEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrCardTemplateEditDto>.FailAsync(localization.GetMessagesResource("UpdateNullEntity"));

            var item = await hrRepositoryManager.HrCardTemplateRepository.GetById(entity.Id);

            if (item == null) return await Result<HrCardTemplateEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            _mapper.Map(entity, item);

            hrRepositoryManager.HrCardTemplateRepository.Update(item);

            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrCardTemplateEditDto>.SuccessAsync(_mapper.Map<HrCardTemplateEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrCardTemplateEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> UpdateTemplateStatus(long templateId, int status, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrCardTemplateRepository.GetById(templateId);
            if (item == null) return Result<HrCardTemplateDto>.Fail(localization.GetMessagesResource("NoIdInUpdate"));
            item.Status = status;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrCardTemplateRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrCardTemplateDto>.SuccessAsync(_mapper.Map<HrCardTemplateDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrCardTemplateDto>.FailAsync($"EXP in Update Status at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }

}