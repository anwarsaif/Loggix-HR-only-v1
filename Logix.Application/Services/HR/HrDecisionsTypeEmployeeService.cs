using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrDecisionsTypeEmployeeService : GenericQueryService<HrDecisionsTypeEmployee, HrDecisionsTypeEmployeeDto, HrDecisionsTypeEmployeeVw>, IHrDecisionsTypeEmployeeService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrDecisionsTypeEmployeeService(IQueryRepository<HrDecisionsTypeEmployee> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrDecisionsTypeEmployeeDto>> Add(HrDecisionsTypeEmployeeDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrDecisionsTypeEmployeeDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrDecisionsTypeEmployeeDto>.FailAsync($"employee of Id :{entity.EmpCode}{localization.GetResource1("EmployeeNotFound")}");
                var checkDecisionExist = await hrRepositoryManager.HrDecisionsTypeRepository.GetOne(x => x.Id == entity.DecisionsTypeId && x.IsDeleted == false && x.IsDeleted == false);
                if (checkDecisionExist == null) return await Result<HrDecisionsTypeEmployeeDto>.FailAsync($"القرار ليس موجود ");

                var checkEmpExistInDecision = await hrRepositoryManager.HrDecisionsTypeEmployeeRepository.GetOne(x => x.EmpId == checkEmpExist.Id && x.IsDeleted == false && x.IsDeleted == false&&x.DecisionsTypeId==entity.DecisionsTypeId);
                if (checkEmpExistInDecision != null) return await Result<HrDecisionsTypeEmployeeDto>.FailAsync($"الموظف ضمن القرار مسبقاً");

                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                entity.EmpId = checkEmpExist.Id;
                var newEntity = await hrRepositoryManager.HrDecisionsTypeEmployeeRepository.AddAndReturn(_mapper.Map<HrDecisionsTypeEmployee>(entity));

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrDecisionsTypeEmployeeDto>(newEntity);
              
                return await Result<HrDecisionsTypeEmployeeDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {
                return await Result<HrDecisionsTypeEmployeeDto>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrDecisionsTypeEmployeeRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrDecisionsTypeEmployeeDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrDecisionsTypeEmployeeRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDecisionsTypeEmployeeDto>.SuccessAsync(_mapper.Map<HrDecisionsTypeEmployeeDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrDecisionsTypeEmployeeDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrDecisionsTypeEmployeeRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrDecisionsTypeEmployeeDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrDecisionsTypeEmployeeRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDecisionsTypeEmployeeDto>.SuccessAsync(_mapper.Map<HrDecisionsTypeEmployeeDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrDecisionsTypeEmployeeDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public Task<IResult<HrDecisionsTypeEmployeeEditDto>> Update(HrDecisionsTypeEmployeeEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
    }