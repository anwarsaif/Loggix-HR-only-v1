using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Logix.Domain.Main;

namespace Logix.Application.Services.HR
{
    public class HrDependentService : GenericQueryService<HrDependent, HrDependentDto, HrDependentsVw>, IHrDependentService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrDependentService(IQueryRepository<HrDependent> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrDependentDto>> Add(HrDependentDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrDependentDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            if (string.IsNullOrEmpty(entity.EmpCode)) return await Result<HrDependentDto>.FailAsync(localization.GetMessagesResource("EmployeeNumberIsRequired"));
            try
            {
                // check if Emp Is Exist
                var CheckEmpExist = await _mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (CheckEmpExist == null) return await Result<HrDependentDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                var newItem = _mapper.Map<HrDependent>(entity);
                newItem.Relationship = entity.RelationshipId.ToString();
                newItem.EmpId = CheckEmpExist.Id;


                var newEntity = await hrRepositoryManager.HrDependentRepository.AddAndReturn(newItem);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrDependentDto>(newEntity);

                return await Result<HrDependentDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<HrDependentDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrDependentRepository.GetById(Id);
                if (item == null) return Result<HrDependentDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrDependentRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDependentDto>.SuccessAsync(_mapper.Map<HrDependentDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrDependentDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrDependentRepository.GetById(Id);
                if (item == null) return Result<HrDependentDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrDependentRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDependentDto>.SuccessAsync(_mapper.Map<HrDependentDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrDependentDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


        public async Task<IResult<HrDependentEditDto>> Update(HrDependentEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrDependentEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
            if (string.IsNullOrEmpty(entity.EmpCode)) return await Result<HrDependentEditDto>.FailAsync($"Employee Code Is Required");

            try
            {
                // check if Emp Is Exist
                var checkEmpExist = await _mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrDependentEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                var item = await hrRepositoryManager.HrDependentRepository.GetById(entity.Id);

                if (item == null) return await Result<HrDependentEditDto>.FailAsync("the Dependent Is Not Found");

                entity.ModifiedBy = session.UserId;
                entity.ModifiedOn = DateTime.Now;

                _mapper.Map(entity, item);
                item.EmpId = checkEmpExist.Id;
                item.Relationship = entity.RelationshipId.ToString();
                hrRepositoryManager.HrDependentRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrDependentEditDto>.SuccessAsync(_mapper.Map<HrDependentEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrDependentEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }

}
