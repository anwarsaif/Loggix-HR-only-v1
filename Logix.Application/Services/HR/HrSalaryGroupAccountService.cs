using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrSalaryGroupAccountService : GenericQueryService<HrSalaryGroupAccount, HrSalaryGroupAccountDto, HrSalaryGroupAccount>, IHrSalaryGroupAccountService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;

        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;
        private readonly IAccRepositoryManager accRepositoryManager;


        public HrSalaryGroupAccountService(IQueryRepository<HrSalaryGroupAccount> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization, IAccRepositoryManager accRepositoryManager) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;   
            this.accRepositoryManager = accRepositoryManager;   
        }

        public async Task<IResult<HrSalaryGroupAccountDto>> Add(HrSalaryGroupAccountDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrSalaryGroupAccountDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            var CheckSalaryGroupAccountsExists =await hrRepositoryManager.HrSalaryGroupAccountRepository.GetAll(a=>a.GroupId==entity.GroupId&&a.TypeId==entity.TypeId&&a.IsDeleted==false&&a.AdId==entity.AdId);   
            if(CheckSalaryGroupAccountsExists.Count()>=1) return await Result<HrSalaryGroupAccountDto>.FailAsync(   session.Language==1?   $"تم اعداد الحساب مسبقاً" : $"The Account is Already Set Up");
            try
            {

                var item = new HrSalaryGroupAccount();
                item.CreatedBy = session.UserId;
                item.CreatedOn = DateTime.Now;
                item.TypeId = entity.TypeId;
                item.AdId = entity.AdId;
                item.GroupId = entity.GroupId;
                
                
                // AccountDueCode
                item.AccountDueId = 0;
                if (!string.IsNullOrEmpty(entity.AccountDueCode))
                {
                    var getAccountByCode = await accRepositoryManager.AccAccountsSubHelpeVwRepository.GetOne(x => x.Isdel == false && x.IsActive == true &&
                        (entity.FacilityId != null && x.FacilityId == entity.FacilityId) && x.AccAccountCode == entity.AccountDueCode && x.SystemId  == 2);

                    if (getAccountByCode == null)
                        return await Result<HrSalaryGroupAccountDto>.FailAsync($"  حساب المستحق غير موجود  ");

                    item.AccountDueId = getAccountByCode?.AccAccountId ?? 0;
                }
                if(item.AccountDueId==0) return await Result<HrSalaryGroupAccountDto>.FailAsync($"  حساب المستحق غير موجود  ");


                // AccountExpCode
                item.AccountExpId = 0;
                if (!string.IsNullOrEmpty(entity.AccountExpCode))
                {
                    var getAccountByCode = await accRepositoryManager.AccAccountsSubHelpeVwRepository.GetOne(x => x.Isdel == false && x.IsActive == true &&
                        (entity.FacilityId == 0 || entity.FacilityId == null || entity.FacilityId == x.FacilityId) &&
                        x.AccAccountCode == entity.AccountExpCode);

                    if (getAccountByCode == null)
                        return await Result<HrSalaryGroupAccountDto>.FailAsync($"  حساب المصروف غير موجود  ");

                    item.AccountExpId = getAccountByCode?.AccAccountId ?? 0;
                }
                if (item.AccountExpId == 0) return await Result<HrSalaryGroupAccountDto>.FailAsync($"  حساب المصروف غير موجود  ");



                var newEntity = await hrRepositoryManager.HrSalaryGroupAccountRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrSalaryGroupAccountDto>(newEntity);


                return await Result<HrSalaryGroupAccountDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception exc)
            {

                return await Result<HrSalaryGroupAccountDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {

            try
            {
                var item = await hrRepositoryManager.HrSalaryGroupAccountRepository.GetById(Id);
                if (item == null) return Result<HrSalaryGroupAccountDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");

                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrSalaryGroupAccountRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrSalaryGroupAccountDto>.SuccessAsync(_mapper.Map<HrSalaryGroupAccountDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrSalaryGroupAccountDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrSalaryGroupAccountRepository.GetById(Id);
                if (item == null) return Result<HrSalaryGroupAccountDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");

                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrSalaryGroupAccountRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrSalaryGroupAccountDto>.SuccessAsync(_mapper.Map<HrSalaryGroupAccountDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrSalaryGroupAccountDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrSalaryGroupAccountEditDto>> Update(HrSalaryGroupAccountEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrSalaryGroupAccountEditDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {
                var item = await hrRepositoryManager.HrSalaryGroupAccountRepository.GetById(entity.Id);

                if (item == null) return await Result<HrSalaryGroupAccountEditDto>.FailAsync($"{localization.GetMessagesResource("NoItemFoundToEdit")}");
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = session.UserId;
                item.TypeId = entity.TypeId;
                item.AdId = entity.AdId;
                item.GroupId = entity.GroupId;

                hrRepositoryManager.HrSalaryGroupAccountRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrSalaryGroupAccountEditDto>.SuccessAsync(_mapper.Map<HrSalaryGroupAccountEditDto>(item), localization.GetResource1("SaveSuccess"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrSalaryGroupAccountEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }

}
