using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrSalaryGroupRefranceService : GenericQueryService<HrSalaryGroupRefrance, HrSalaryGroupRefranceDto, HrSalaryGroupRefranceVw>, IHrSalaryGroupRefranceService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;

        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;
        private readonly IAccRepositoryManager accRepositoryManager;


        public HrSalaryGroupRefranceService(IQueryRepository<HrSalaryGroupRefrance> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization, IAccRepositoryManager accRepositoryManager) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
            this.accRepositoryManager = accRepositoryManager;   
        }

        public async Task<IResult<HrSalaryGroupRefranceDto>> Add(HrSalaryGroupRefranceDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrSalaryGroupRefranceDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {

                var item = new HrSalaryGroupRefrance();
                item.CreatedBy = session.UserId;
                item.CreatedOn = DateTime.Now;
                item.GroupId = entity.GroupId;
                item.GroupId = entity.GroupId;
                item.ReferenceTypeId = entity.ReferenceTypeId;
                // AccountCode
                item.AccountId = 0;
                if (!string.IsNullOrEmpty(entity.AccountCode))
                {
                    var getAccountByCode = await accRepositoryManager.AccAccountsSubHelpeVwRepository.GetOne(x => x.Isdel == false && x.IsActive == true &&
                        ( session.FacilityId == x.FacilityId) &&
                        x.AccAccountCode == entity.AccountCode);

                    if (getAccountByCode == null)
                        return await Result<HrSalaryGroupRefranceDto>.FailAsync($"رقم الحساب غير موجود في دليل الحسابات");

                    item.AccountId = getAccountByCode?.AccAccountId ?? 0;
                }
                if (item.AccountId == 0) return await Result<HrSalaryGroupRefranceDto>.FailAsync($"رقم الحساب غير موجود في دليل الحسابات");




                var newEntity = await hrRepositoryManager.HrSalaryGroupRefranceRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrSalaryGroupRefranceDto>(newEntity);


                return await Result<HrSalaryGroupRefranceDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<HrSalaryGroupRefranceDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrSalaryGroupRefranceRepository.GetById(Id);
                if (item == null) return Result<HrSalaryGroupRefranceDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");

                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = session.UserId;
                hrRepositoryManager.HrSalaryGroupRefranceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrSalaryGroupRefranceDto>.SuccessAsync(_mapper.Map<HrSalaryGroupRefranceDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrSalaryGroupRefranceDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrSalaryGroupRefranceRepository.GetById(Id);
                if (item == null) return Result<HrSalaryGroupRefranceDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");

                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = session.UserId;
                hrRepositoryManager.HrSalaryGroupRefranceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrSalaryGroupRefranceDto>.SuccessAsync(_mapper.Map<HrSalaryGroupRefranceDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrSalaryGroupRefranceDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrSalaryGroupRefranceEditDto>> Update(HrSalaryGroupRefranceEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrSalaryGroupRefranceEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");


            try
            {
                var item = await hrRepositoryManager.HrSalaryGroupRefranceRepository.GetById(entity.Id);

                if (item == null) return await Result<HrSalaryGroupRefranceEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                _mapper.Map(entity, item);

                hrRepositoryManager.HrSalaryGroupRefranceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrSalaryGroupRefranceEditDto>.SuccessAsync(_mapper.Map<HrSalaryGroupRefranceEditDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrSalaryGroupRefranceEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }

}
