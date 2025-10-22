using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrLicenseService : GenericQueryService<HrLicense, HrLicenseDto, HrLicensesVw>, IHrLicenseService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;

        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;


        public HrLicenseService(IQueryRepository<HrLicense> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrLicenseDto>> Add(HrLicenseDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrLicenseDto>.FailAsync(localization.GetMessagesResource("AddNullEntity"));

            try
            {
                var checkEmp = await _mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmp == null) return await Result<HrLicenseDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                var item = _mapper.Map<HrLicense>(entity);
                item.EmpId = checkEmp.Id;
                item.CreatedBy = session.UserId;
                item.CreatedOn = DateTime.Now;
                item.IsDeleted = false;

                await hrRepositoryManager.HrLicenseRepository.Add(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrLicenseDto>.SuccessAsync(localization.GetResource1("AddSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<HrLicenseDto>.FailAsync(localization.GetResource1("AddError") + exc);
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrLicenseRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrLicenseDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrLicenseRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrLicenseDto>.SuccessAsync(_mapper.Map<HrLicenseDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrLicenseDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrLicenseRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrLicenseDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrLicenseRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrLicenseDto>.SuccessAsync(_mapper.Map<HrLicenseDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrLicenseDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrLicenseEditDto>> Update(HrLicenseEditDto entity, CancellationToken cancellationToken = default)
        {

            try
            {
                if (entity == null) return await Result<HrLicenseEditDto>.FailAsync(localization.GetMessagesResource("AddNullEntity"));

                var item = await hrRepositoryManager.HrLicenseRepository.GetById(entity.Id);
                var checkEmp = await _mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmp == null) return await Result<HrLicenseEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                if (item == null) return await Result<HrLicenseEditDto>.FailAsync(localization.GetMessagesResource("NoDataWithId"));

                _mapper.Map(entity, item);
                item.EmpId = checkEmp.Id;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = session.UserId;
                hrRepositoryManager.HrLicenseRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrLicenseEditDto>.SuccessAsync(_mapper.Map<HrLicenseEditDto>(item), localization.GetResource1("UpdateSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrLicenseEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }

}
