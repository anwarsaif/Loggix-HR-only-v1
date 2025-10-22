using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrDisciplinaryRuleService : GenericQueryService<HrDisciplinaryRule, HrDisciplinaryRuleDto, HrDisciplinaryRuleVw>, IHrDisciplinaryRuleService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;


        public HrDisciplinaryRuleService(IQueryRepository<HrDisciplinaryRule> queryRepository, IHrRepositoryManager hrRepositoryManager, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this.hrRepositoryManager = hrRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrDisciplinaryRuleDto>> Add(HrDisciplinaryRuleDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrDisciplinaryRuleDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {

                var item = _mapper.Map<HrDisciplinaryRule>(entity);
                var newEntity = await hrRepositoryManager.HrDisciplinaryRuleRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrDisciplinaryRuleDto>(newEntity);


                return await Result<HrDisciplinaryRuleDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<HrDisciplinaryRuleDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrDisciplinaryRuleRepository.GetById(Id);
            if (item == null) return Result<HrDisciplinaryRuleDto>.Fail($"--- there is no Data with this id: {Id}---");
            hrRepositoryManager.HrDisciplinaryRuleRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDisciplinaryRuleDto>.SuccessAsync(_mapper.Map<HrDisciplinaryRuleDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrDisciplinaryRuleDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrDisciplinaryRuleRepository.GetById(Id);
            if (item == null) return Result<HrDisciplinaryRuleDto>.Fail($"--- there is no Data with this id: {Id}---");
            hrRepositoryManager.HrDisciplinaryRuleRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDisciplinaryRuleDto>.SuccessAsync(_mapper.Map<HrDisciplinaryRuleDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrDisciplinaryRuleDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrDisciplinaryRuleEditDto>> Update(HrDisciplinaryRuleEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrDisciplinaryRuleEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

            var item = await hrRepositoryManager.HrDisciplinaryRuleRepository.GetById(entity.Id);

            if (item == null) return await Result<HrDisciplinaryRuleEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
            _mapper.Map(entity, item);

            hrRepositoryManager.HrDisciplinaryRuleRepository.Update(item);

            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDisciplinaryRuleEditDto>.SuccessAsync(_mapper.Map<HrDisciplinaryRuleEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrDisciplinaryRuleEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }
    }

}