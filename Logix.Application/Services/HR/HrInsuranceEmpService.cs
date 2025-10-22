using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Services.HR
{
    public class HrInsuranceEmpService : GenericQueryService<HrInsuranceEmp, HrInsuranceEmpDto, HrInsuranceEmpVw>, IHrInsuranceEmpService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;

        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;
        private readonly IMainServiceManager mainServiceManager;
        public HrInsuranceEmpService(Interfaces.IRepositories.IQueryRepository<HrInsuranceEmp> queryRepository,
            IHrRepositoryManager hrRepositoryManager,
            IMainRepositoryManager mainRepositoryManager,
            IMainServiceManager mainServiceManager,
            IMapper mapper, ICurrentData session, ILocalizationService localization) : base(queryRepository, mapper)

        {
            this.hrRepositoryManager = hrRepositoryManager;
            this.mainRepositoryManager = mainRepositoryManager;
            _mapper = mapper;

            this.session = session;
            this.localization = localization;
            this.mainServiceManager = mainServiceManager;

        }

        public Task<IResult<HrInsuranceEmpDto>> Add(HrInsuranceEmpDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrInsuranceEmpRepository.GetById(Id);
                if (item == null) return Result<HrInsuranceEmpDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;
                hrRepositoryManager.HrInsuranceEmpRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                return await Result<HrInsuranceEmpDto>.SuccessAsync(_mapper.Map<HrInsuranceEmpDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrInsuranceEmpDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrInsuranceEmpRepository.GetById(Id);
            if (item == null) return Result<HrOhadDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrInsuranceEmpRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrInsuranceEmpDto>.SuccessAsync(_mapper.Map<HrInsuranceEmpDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrInsuranceEmpDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrInsuranceEmpEditDto>> Update(HrInsuranceEmpEditDto entity, CancellationToken cancellationToken = default)
        {
            //if (entity.Id==0)
            //{
            //foreach (var singleItem in entity)
            //{
            var updateItem = await hrRepositoryManager.HrInsuranceEmpRepository.GetOne(e => e.Id == entity.Id);
            if (updateItem == null) return await Result<HrInsuranceEmpEditDto>.FailAsync("no resulte");
            try
            {
                var Employees = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == entity.EmpId.ToString() && e.IsDeleted == false);

                if (Employees == null)
                {
                    return await Result<HrInsuranceEmpEditDto>.FailAsync("  الموظف غير موجود في قائمة الموظفين");
                }
                var newsingleItem = new HrInsuranceEmp
                {
                    //Id = singleItem.Id,
                    InsuranceId = entity.Id,
                    DependentId = entity.DependentId,
                    EmpId = Employees.Id,
                    ClassId = entity.ClassId,
                    Amount = entity.Amount,
                    InsuranceCardNo = entity.InsuranceCardNo,
                    Note = entity.Note,
                    ModifiedBy = session.UserId,
                    ModifiedOn = DateTime.Now,
                };

                hrRepositoryManager.HrInsuranceEmpRepository.Update(newsingleItem);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                return await Result<HrInsuranceEmpEditDto>.SuccessAsync("Insurance Employee update successfully");

            }
            

            catch (Exception exp)
            {

                return await Result<HrInsuranceEmpEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {""}");

            }
        }
    }
}
