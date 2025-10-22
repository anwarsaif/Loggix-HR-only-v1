using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrKpiTemplatesJobService : GenericQueryService<HrKpiTemplatesJob, HrKpiTemplatesJobDto, HrKpiTemplatesJobsVw>, IHrKpiTemplatesJobService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper mapper;
        private readonly ICurrentData session;


        public HrKpiTemplatesJobService(IQueryRepository<HrKpiTemplatesJob> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this.mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrKpiTemplatesJobDto>> Add(HrKpiTemplatesJobDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrKpiTemplatesJobDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                if (entity.CatJobId <= 0)
                    return await Result<HrKpiTemplatesJobDto>.FailAsync($"يجب نحديد المسمى الوظيفي ");
                if (entity.TemplateId <= 0)
                    return await Result<HrKpiTemplatesJobDto>.FailAsync($"  يجب نحديد نموذج التقييم");

                // نفحص هل المسمى الوظيفي موجود سابقا


                var checkIfExist = await hrRepositoryManager.HrKpiTemplatesJobRepository.GetAll(x => x.IsDeleted == false && x.CatJobId == entity.CatJobId && x.FacilityId == session.FacilityId);
                if (checkIfExist.Count() > 0)return  await Result<HrKpiTemplatesJobDto>.FailAsync(localization.GetHrResource("JobAlreadyExists"));
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                entity.FacilityId = session.FacilityId;
                var newItem = mapper.Map<HrKpiTemplatesJob>(entity);

                var newAddEntity = await hrRepositoryManager.HrKpiTemplatesJobRepository.AddAndReturn(newItem);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = mapper.Map<HrKpiTemplatesJobDto>(newAddEntity);

                return await Result<HrKpiTemplatesJobDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {
                return await Result<HrKpiTemplatesJobDto>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrKpiTemplatesJobRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrKpiTemplatesJobDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrKpiTemplatesJobRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrKpiTemplatesJobDto>.SuccessAsync(mapper.Map<HrKpiTemplatesJobDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrPerformanceDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrKpiTemplatesJobRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrKpiTemplatesJobDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrKpiTemplatesJobRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrKpiTemplatesJobDto>.SuccessAsync(mapper.Map<HrKpiTemplatesJobDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrPerformanceDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public Task<IResult<HrKpiTemplatesJobEditDto>> Update(HrKpiTemplatesJobEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}