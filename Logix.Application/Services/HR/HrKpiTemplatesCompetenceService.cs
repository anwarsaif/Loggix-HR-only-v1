using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrKpiTemplatesCompetenceService : GenericQueryService<HrKpiTemplatesCompetence, HrKpiTemplatesCompetenceDto, HrKpiTemplatesCompetencesVw>, IHrKpiTemplatesCompetenceService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;

        public HrKpiTemplatesCompetenceService(IQueryRepository<HrKpiTemplatesCompetence> queryRepository,
            IHrRepositoryManager hrRepositoryManager,
            IMainRepositoryManager mainRepositoryManager,
            IMapper mapper, ICurrentData session) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this.mainRepositoryManager = mainRepositoryManager;
            _mapper = mapper;

            this.session = session;
        }

        public async Task<IResult<HrKpiTemplatesCompetenceDto>> Add(HrKpiTemplatesCompetenceDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrKpiTemplatesCompetenceDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {



                var item = _mapper.Map<HrKpiTemplatesCompetence>(entity);
                item.CreatedBy = session.UserId;
                item.CreatedOn = DateTime.Now;
                var newEntity = await hrRepositoryManager.HrKpiTemplatesCompetenceRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrKpiTemplatesCompetenceDto>(newEntity);


                return await Result<HrKpiTemplatesCompetenceDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<HrKpiTemplatesCompetenceDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }



        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrKpiTemplatesCompetenceRepository.GetById(Id);
            if (item == null) return Result<HrKpiTemplatesCompetenceDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrKpiTemplatesCompetenceRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrKpiTemplatesCompetenceDto>.SuccessAsync(_mapper.Map<HrKpiTemplatesCompetenceDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrKpiTemplatesCompetenceDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrKpiTemplatesCompetenceRepository.GetById(Id);
            if (item == null) return Result<HrKpiTemplatesCompetenceDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrKpiTemplatesCompetenceRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrKpiTemplatesCompetenceDto>.SuccessAsync(_mapper.Map<HrKpiTemplatesCompetenceDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrKpiTemplatesCompetenceDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrKpiTemplatesCompetenceEditDto>> Update(HrKpiTemplatesCompetenceEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrKpiTemplatesCompetenceEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

            var item = await hrRepositoryManager.HrKpiTemplatesCompetenceRepository.GetById(entity.Id);

            if (item == null) return await Result<HrKpiTemplatesCompetenceEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            _mapper.Map(entity, item);

            hrRepositoryManager.HrKpiTemplatesCompetenceRepository.Update(item);

            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrKpiTemplatesCompetenceEditDto>.SuccessAsync(_mapper.Map<HrKpiTemplatesCompetenceEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrKpiTemplatesCompetenceEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

    }

}