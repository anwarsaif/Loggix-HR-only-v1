using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.TS;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrStructureService : GenericQueryService<HrStructure, HrStructureDto, HrStructureVw>, IHrStructureService
    {
        private readonly IMapper mapper;
        private readonly ILocalizationService localization;
        private readonly ICurrentData session;
        private readonly IHrRepositoryManager hrRepositoryManager;
        public HrStructureService(IQueryRepository<HrStructure> queryRepository,
            IMapper mapper,
            ILocalizationService localization,
            ICurrentData session, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
        }

        public async Task<IResult<HrStructureDto>> Add(HrStructureDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                return await Result<HrStructureDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {
                var chkCode  = await hrRepositoryManager.HrStructureRepository.GetOne(x => x.Code == entity.Code);
                if (chkCode != null)
                    return await Result<HrStructureDto>.FailAsync(localization.GetMessagesResource("StructureCodeExist"));
                
                var MappedEntity = mapper.Map<HrStructure>(entity);

                MappedEntity.IsDeleted = false;
                MappedEntity.CreatedBy = session.UserId;
                MappedEntity.CreatedOn = DateTime.UtcNow;
                var newEntity = await hrRepositoryManager.HrStructureRepository.AddAndReturn(MappedEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = mapper.Map<HrStructureDto>(newEntity);

                return await Result<HrStructureDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {
                return await Result<HrStructureDto>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrStructureRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrStructureDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");
                item.IsDeleted = true;
                item.ModifiedBy = session.UserId;
                hrRepositoryManager.HrStructureRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                return await Result<HrStructureDto>.SuccessAsync(mapper.Map<HrStructureDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrStructureDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<HrStructureEditDto>> Update(HrStructureEditDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                if (entity == null) return await Result<HrStructureEditDto>.FailAsync(localization.GetMessagesResource("UpdateNullEntity"));

                var item = await hrRepositoryManager.HrStructureRepository.GetById(entity.Id);

                if (item == null)
                    return await Result<HrStructureEditDto>.FailAsync(localization.GetMessagesResource("NoData") + $" with this id: {entity.Id}---");


                mapper.Map(entity, item);
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.UtcNow;

                hrRepositoryManager.HrStructureRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrStructureEditDto>.SuccessAsync(mapper.Map<HrStructureEditDto>(item), localization.GetMessagesResource("UpdateSuccess"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrStructureEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}
