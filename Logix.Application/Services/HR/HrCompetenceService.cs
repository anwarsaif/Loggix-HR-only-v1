using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrCompetenceService : GenericQueryService<HrCompetence, HrCompetenceDto, HrCompetencesVw>, IHrCompetenceService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;

        public HrCompetenceService(IQueryRepository<HrCompetence> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager=hrRepositoryManager;
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrCompetenceDto>> Add(HrCompetenceDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrCompetenceDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {


                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                entity.Score = entity.Score==null?0:entity.Score;

                var item = _mapper.Map<HrCompetence>(entity);
                var newEntity = await hrRepositoryManager.HrCompetenceRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrCompetenceDto>(newEntity);


                return await Result<HrCompetenceDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<HrCompetenceDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }

        public async  Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrCompetenceRepository.GetById(Id);
            if (item == null) return Result<HrCompetenceDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrCompetenceRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrCompetenceDto>.SuccessAsync(_mapper.Map<HrCompetenceDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrCompetenceDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrCompetenceRepository.GetById(Id);
            if (item == null) return Result<HrCompetenceDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrCompetenceRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrCompetenceDto>.SuccessAsync(_mapper.Map<HrCompetenceDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrCompetenceDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrCompetenceEditDto>> Update(HrCompetenceEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrCompetenceEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

            var item = await hrRepositoryManager.HrCompetenceRepository.GetById(entity.Id);

            if (item == null) return await Result<HrCompetenceEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            _mapper.Map(entity, item);

            hrRepositoryManager.HrCompetenceRepository.Update(item);

            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrCompetenceEditDto>.SuccessAsync(_mapper.Map<HrCompetenceEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrCompetenceEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
    }