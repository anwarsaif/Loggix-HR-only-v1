using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrJobDescriptionService : GenericQueryService<HrJobDescription, HrJobDescriptionDto, HrJobDescription>, IHrJobDescriptionService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;


        public HrJobDescriptionService(IQueryRepository<HrJobDescription> queryRepository,
            IHrRepositoryManager hrRepositoryManager,
            IMainRepositoryManager mainRepositoryManager,
            IMapper mapper, ICurrentData session, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this.mainRepositoryManager = mainRepositoryManager;
            _mapper = mapper;
            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrJobDescriptionDto>> Add(HrJobDescriptionDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                var getJobTitle = await mainRepositoryManager.SysLookupDataRepository.GetOne(x => x.Name, x => x.Code == entity.JobCatId&& x.Isdel==false&&x.CatagoriesId==5);
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                entity.JobTitle = getJobTitle ?? "";
                var newEntity = await hrRepositoryManager.HrJobDescriptionRepository.AddAndReturn(_mapper.Map<HrJobDescription>(entity));

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrJobDescriptionDto>(newEntity);

                return await Result<HrJobDescriptionDto>.SuccessAsync(entityMap, localization.GetResource1("CreateSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<HrJobDescriptionDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrJobDescriptionRepository.GetById(Id);
                if (item == null) return Result<HrJobDescriptionDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrJobDescriptionRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrJobDescriptionDto>.SuccessAsync(_mapper.Map<HrJobDescriptionDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrJobDescriptionDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrJobDescriptionRepository.GetById(Id);
                if (item == null) return Result<HrJobDescriptionDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrJobDescriptionRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrJobDescriptionDto>.SuccessAsync(_mapper.Map<HrJobDescriptionDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrJobDescriptionDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrJobDescriptionEditDto>> Update(HrJobDescriptionEditDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                var getJobTitle = await mainRepositoryManager.SysLookupDataRepository.GetOne(x => x.Name, x => x.Code == entity.JobCatId && x.Isdel == false && x.CatagoriesId == 5);

                var item = await hrRepositoryManager.HrJobDescriptionRepository.GetById(entity.Id);
                if (item == null) return await Result<HrJobDescriptionEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");

                entity.ModifiedBy = session.UserId;
                entity.ModifiedOn = DateTime.Now;
                entity.JobTitle = getJobTitle ?? "";
                _mapper.Map(entity, item);

                hrRepositoryManager.HrJobDescriptionRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrJobDescriptionEditDto>.SuccessAsync(_mapper.Map<HrJobDescriptionEditDto>(item), "Item updated successfully");

            }
            catch (Exception exc)
            {
                return await Result<HrJobDescriptionEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }
    }
}