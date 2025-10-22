using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrKpiTemplateService : GenericQueryService<HrKpiTemplate, HrKpiTemplateDto, HrKpiTemplatesVw>, IHrKpiTemplateService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;

        public HrKpiTemplateService(IQueryRepository<HrKpiTemplate> queryRepository,
            IHrRepositoryManager hrRepositoryManager,
            IMainRepositoryManager mainRepositoryManager,
            IMapper mapper, ICurrentData session) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this.mainRepositoryManager = mainRepositoryManager;
            _mapper = mapper;

            this.session = session;
        }

        public async Task<IResult<HrKpiTemplateDto>> Add(HrKpiTemplateDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrKpiTemplateDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {



                entity.IsDeleted = false;
                entity.KpiWeight ??= 0;
                entity.CompetencesWeight ??= 0;
                var item = _mapper.Map<HrKpiTemplate>(entity);
                item.CreatedBy = session.UserId;
                item.CreatedOn = DateTime.Now;
                var newEntity = await hrRepositoryManager.HrKpiTemplateRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrKpiTemplateDto>(newEntity);


                return await Result<HrKpiTemplateDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<HrKpiTemplateDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrKpiTemplateRepository.GetById(Id);
                if (item == null) return Result<HrKpiTemplateDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrKpiTemplateRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrKpiTemplateDto>.SuccessAsync(_mapper.Map<HrKpiTemplateDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrKpiTemplateDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrKpiTemplateRepository.GetById(Id);
                if (item == null) return Result<HrKpiTemplateDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrKpiTemplateRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrKpiTemplateDto>.SuccessAsync(_mapper.Map<HrKpiTemplateDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrKpiTemplateDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrKpiTemplateEditDto>> Update(HrKpiTemplateEditDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                if (entity == null) return await Result<HrKpiTemplateEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

                var item = await hrRepositoryManager.HrKpiTemplateRepository.GetById(entity.Id);

                if (item == null) return await Result<HrKpiTemplateEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                _mapper.Map(entity, item);

                hrRepositoryManager.HrKpiTemplateRepository.Update(item);


                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrKpiTemplateEditDto>.SuccessAsync(_mapper.Map<HrKpiTemplateEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrKpiTemplateEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }
    }


}