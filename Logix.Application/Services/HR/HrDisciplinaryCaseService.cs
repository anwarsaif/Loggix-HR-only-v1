using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrDisciplinaryCaseService : GenericQueryService<HrDisciplinaryCase, HrDisciplinaryCaseDto, HrDisciplinaryCase>, IHrDisciplinaryCaseService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;


        public HrDisciplinaryCaseService(IQueryRepository<HrDisciplinaryCase> queryRepository, IHrRepositoryManager hrRepositoryManager, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this.hrRepositoryManager = hrRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrDisciplinaryCaseDto>> Add(HrDisciplinaryCaseDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrDisciplinaryCaseDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {

                var item = _mapper.Map<HrDisciplinaryCase>(entity);
                item.CaseName = entity.CaseName;
                item.CaseName2 = entity.CaseName2;
                item.TypeId = entity.TypeId;
                item.FromDay = entity.FromDay;
                item.ToDay = entity.ToDay;
                item.CBegin = entity.FromMinutes;
                item.CEnd = entity.ToMinutes;
                item.ApplyOfAbsence = entity.ApplyOfAbsence;
                item.ApplyOfDelay = entity.ApplyOfDelay;
                if (entity.TypeId == 1)
                {
                    item.FromMinutes = entity.FromMinutes ?? 0;
                    item.ToMinutes = entity.ToMinutes ?? 0;
                }
                else
                {
                    item.FromMinutes = entity.FromMinutes ?? 0;
                    item.ToMinutes = entity.ToMinutes ?? 0;
                }
                item.ApplyOfEarly = entity.ApplyOfEarly;
                item.CreatedBy = session.UserId;
                item.CreatedOn = DateTime.Now;

                if (entity.TypeId == 1)
                {
                    item.ApplyOfAbsence = false;
                    item.ApplyOfDelay = entity.ApplyOfDelay;
                    item.ApplyOfEarly = false;
                    item.FromDay = 0;
                    item.ToDay = 0;
                }

                if (entity.TypeId == 2)
                {
                    item.ApplyOfAbsence = entity.ApplyOfAbsence;
                    item.ApplyOfDelay = false;
                    item.ApplyOfEarly = false;
                    item.CBegin = 0;
                    item.CEnd = 0;
                    item.FromMinutes = 0;
                    item.ToMinutes = 0;
                }

                if (entity.TypeId == 3)
                {
                    item.ApplyOfAbsence = false;
                    item.ApplyOfDelay = false;
                    item.ApplyOfEarly = false;
                    item.CBegin = 0;
                    item.CEnd = 0;
                    item.FromMinutes = 0;
                    item.ToMinutes = 0;
                    item.FromDay = 0;
                    item.ToDay = 0;
                }

                if (entity.TypeId == 4)
                {
                    item.ApplyOfAbsence = false;
                    item.ApplyOfDelay = false;
                    item.ApplyOfEarly = entity.ApplyOfEarly;
                    item.FromDay = 0;
                    item.ToDay = 0;
                }

                var newEntity = await hrRepositoryManager.HrDisciplinaryCaseRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrDisciplinaryCaseDto>(newEntity);


                return await Result<HrDisciplinaryCaseDto>.SuccessAsync(entityMap, localization.GetResource1("CreateSuccess"));
            }
            catch (Exception exc)
            {

                return await Result<HrDisciplinaryCaseDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }



        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrDisciplinaryCaseRepository.GetById(Id);
            if (item == null) return Result<HrDisciplinaryCaseDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}"); ;
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrDisciplinaryCaseRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDisciplinaryCaseDto>.SuccessAsync(_mapper.Map<HrDisciplinaryCaseDto>(item), localization.GetMessagesResource("DeletedSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrDisciplinaryCaseDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrDisciplinaryCaseRepository.GetById(Id);
            if (item == null) return Result<HrDisciplinaryCaseDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrDisciplinaryCaseRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDisciplinaryCaseDto>.SuccessAsync(_mapper.Map<HrDisciplinaryCaseDto>(item), localization.GetMessagesResource("DeletedSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrDisciplinaryCaseDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrDisciplinaryCaseEditDto>> Update(HrDisciplinaryCaseEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrDisciplinaryCaseEditDto>.FailAsync(localization.GetMessagesResource("UpdateNullEntity"));

            var item = await hrRepositoryManager.HrDisciplinaryCaseRepository.GetById(entity.Id);

            if (item == null) return await Result<HrDisciplinaryCaseEditDto>.FailAsync(localization.GetMessagesResource("UpdateNullEntity"));
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            _mapper.Map(entity, item);

            item.CaseName = entity.CaseName;
            item.CaseName2 = entity.CaseName2;
            item.TypeId = entity.TypeId;
            item.FromDay = entity.FromDay;
            item.ToDay = entity.ToDay;
            item.CBegin = entity.FromMinutes;
            item.CEnd = entity.ToMinutes;
            item.ApplyOfAbsence = entity.ApplyOfAbsence;
            item.ApplyOfDelay = entity.ApplyOfDelay;
            if (entity.TypeId == 1)
            {
                item.FromMinutes = entity.FromMinutes ?? 0;
                item.ToMinutes = entity.ToMinutes ?? 0;
            }
            else
            {
                item.FromMinutes = entity.FromMinutes ?? 0;
                item.ToMinutes = entity.ToMinutes ?? 0;
            }
            item.ApplyOfEarly = entity.ApplyOfEarly;

            if (entity.TypeId == 1)
            {
                item.ApplyOfAbsence = false;
                item.ApplyOfDelay = entity.ApplyOfDelay;
                item.ApplyOfEarly = false;
                item.FromDay = 0;
                item.ToDay = 0;
            }

            if (entity.TypeId == 2)
            {
                item.ApplyOfAbsence = entity.ApplyOfAbsence;
                item.ApplyOfDelay = false;
                item.ApplyOfEarly = false;
                item.CBegin = 0;
                item.CEnd = 0;
                item.FromMinutes = 0;
                item.ToMinutes = 0;
            }

            if (entity.TypeId == 3)
            {
                item.ApplyOfAbsence = false;
                item.ApplyOfDelay = false;
                item.ApplyOfEarly = false;
                item.CBegin = 0;
                item.CEnd = 0;
                item.FromMinutes = 0;
                item.ToMinutes = 0;
                item.FromDay = 0;
                item.ToDay = 0;
            }

            if (entity.TypeId == 4)
            {
                item.ApplyOfAbsence = false;
                item.ApplyOfDelay = false;
                item.ApplyOfEarly = entity.ApplyOfEarly;
                item.FromDay = 0;
                item.ToDay = 0;
            }
            hrRepositoryManager.HrDisciplinaryCaseRepository.Update(item);

            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDisciplinaryCaseEditDto>.SuccessAsync(_mapper.Map<HrDisciplinaryCaseEditDto>(item), localization.GetMessagesResource("NoIdInUpdate"));
            }
            catch (Exception exp)
            {
                return await Result<HrDisciplinaryCaseEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }
        public async Task<IResult<HrDisciplinaryRuleDto>> AddNewRule(HrDisciplinaryRuleDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrDisciplinaryRuleDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {


                var item = _mapper.Map<HrDisciplinaryRule>(entity);

                var newEntity = await hrRepositoryManager.HrDisciplinaryRuleRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrDisciplinaryRuleDto>(newEntity);


                return await Result<HrDisciplinaryRuleDto>.SuccessAsync(entityMap, localization.GetResource1("CreateSuccess"));
            }
            catch (Exception exc)
            {

                return await Result<HrDisciplinaryRuleDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<HrDisciplinaryRuleEditDto>> EditRule(HrDisciplinaryRuleEditDto entity, CancellationToken cancellationToken = default)
        {

            if (entity == null) return await Result<HrDisciplinaryRuleEditDto>.FailAsync(localization.GetMessagesResource("UpdateNullEntity"));


            try
            {
                var item = await hrRepositoryManager.HrDisciplinaryRuleRepository.GetById(entity.Id);

                if (item == null) return await Result<HrDisciplinaryRuleEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
                _mapper.Map(entity, item);

                hrRepositoryManager.HrDisciplinaryRuleRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDisciplinaryRuleEditDto>.SuccessAsync(_mapper.Map<HrDisciplinaryRuleEditDto>(item), localization.GetResource1("CreateSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrDisciplinaryRuleEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }


        }
    }

}