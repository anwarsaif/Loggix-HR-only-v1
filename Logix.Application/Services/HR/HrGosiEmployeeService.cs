using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrGosiEmployeeService : GenericQueryService<HrGosiEmployee, HrGosiEmployeeDto, HrGosiEmployeeVw>, IHrGosiEmployeeService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly IAccRepositoryManager accRepositoryManager;

        public HrGosiEmployeeService(IQueryRepository<HrGosiEmployee> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization, IAccRepositoryManager accRepositoryManager) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
            this.accRepositoryManager = accRepositoryManager;
        }

        public Task<IResult<HrGosiEmployeeDto>> Add(HrGosiEmployeeDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrGosiEmployeeRepository.GetById(Id);
                if (item == null) return Result<HrGosiEmployeeDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrGosiEmployeeRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrGosiEmployeeDto>.SuccessAsync(_mapper.Map<HrGosiEmployeeDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                return await Result<HrGosiEmployeeDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<HrGosiEmployeeEditDto>> Update(HrGosiEmployeeEditDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                if (entity == null) return await Result<HrGosiEmployeeEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

                var Gosi = await hrRepositoryManager.HrGosiRepository.GetById((long)entity.GosiId);
                if (Gosi == null) return await Result<HrGosiEmployeeEditDto>.FailAsync($"--- there is no Data with this id: {entity.GosiId}---");

                int? status = await accRepositoryManager.AccJournalMasterRepository.GetJournalMasterStatuse(Convert.ToInt32(entity.GosiId), 37);
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                if (status == 2)
                {
                    return await Result<HrGosiEmployeeEditDto>.FailAsync($"لايمكن تعديل الاستحقاق وذلك لترحيله  ");

                }


                var item = await hrRepositoryManager.HrGosiEmployeeRepository.GetById(entity.Id);

                if (item == null) return await Result<HrGosiEmployeeEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrGosiEmployeeEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                _mapper.Map(entity, item);
                item.EmpId = checkEmpExist.Id;
                hrRepositoryManager.HrGosiEmployeeRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);


                return await Result<HrGosiEmployeeEditDto>.SuccessAsync(_mapper.Map<HrGosiEmployeeEditDto>(item), localization.GetResource1("UpdateSuccess"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrGosiEmployeeEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }
    }
}