using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
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
    public class HrAttLocationEmployeeService : GenericQueryService<HrAttLocationEmployee, HrAttLocationEmployeeDto, HrAttLocationEmployeeVw>, IHrAttLocationEmployeeService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrAttLocationEmployeeService(IQueryRepository<HrAttLocationEmployee> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrAttLocationEmployeeDto>> Add(HrAttLocationEmployeeDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrAttLocationEmployeeDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
            try
            {
                if (entity.LocationId <= 0) return await Result<HrAttLocationEmployeeDto>.FailAsync("يجب تحديد الموقع");

                if (entity.EmpIds.Count()>0 )
                {
                    await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                    foreach (var newitem in entity.EmpIds)
                    {
                        // check if Emp Is Exist
                        var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.Id == newitem && x.IsDeleted == false && x.Isdel == false);
                        if (checkEmpExist == null) return await Result<HrAttLocationEmployeeDto>.FailAsync($"الموظف رقم  {newitem}  غير موجود");
 
                        var newAttLocation = new HrAttLocationEmployee
                        {
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            IsDeleted = false,
                            BeginDate = entity.BeginDate,
                            EndDate = entity.EndDate,
                            EmpId = newitem,
                            LocationId = entity.LocationId,
                        };
                        var newEntity = await hrRepositoryManager.HrAttLocationEmployeeRepository.AddAndReturn(newAttLocation);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                    await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                    return await Result<HrAttLocationEmployeeDto>.SuccessAsync(entity, localization.GetResource1("AddSuccess"));

                }
                return await Result<HrAttLocationEmployeeDto>.FailAsync(" لم يتم تحديد أي موظف من القائمة");
            }
            catch (Exception)
            {

                return await Result<HrAttLocationEmployeeDto>.FailAsync(localization.GetResource1("AddError"));
            }
        }

        public async Task<IResult<string>> Cancel(HrAttLocationEmployeeCancelDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            try
            {
             if(entity.LocationId<=0) return await Result<string>.FailAsync("يجب تحديد الموقع");
                if (entity.EmpId.Any())
                {
                    await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                    foreach (var updateitem in entity.EmpId)
                    {
                        var item = await hrRepositoryManager.HrAttLocationEmployeeRepository.GetOne(x => x.EmpId == updateitem && x.LocationId == entity.LocationId);
                        if (item == null) return await Result<string>.FailAsync($"الموظف رقم  {updateitem}  غير موجود");
                        item.ModifiedBy = session.UserId;
                        item.ModifiedOn = DateTime.Now;
                        item.EmpId = updateitem;
                        item.IsDeleted = true;
                        hrRepositoryManager.HrAttLocationEmployeeRepository.Update(item);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                    await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                    return await Result<string>.SuccessAsync(localization.GetResource1("DeleteSuccess"),200);

                }

                return await Result<string>.FailAsync(" لم يتم تحديد أي موظف من القائمة");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<string>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrAttLocationEmployeeRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrAttLocationEmployeeDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrAttLocationEmployeeRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAttLocationEmployeeDto>.SuccessAsync(_mapper.Map<HrAttLocationEmployeeDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrAttLocationEmployeeDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrAttLocationEmployeeRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrAttLocationEmployeeDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrAttLocationEmployeeRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAttLocationEmployeeDto>.SuccessAsync(_mapper.Map<HrAttLocationEmployeeDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrAttLocationEmployeeDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrAttLocationEmployeeEditeDto>> Update(HrAttLocationEmployeeEditeDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrAttLocationEmployeeEditeDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            try
            {
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrAttLocationEmployeeEditeDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                if (checkEmpExist.StatusId == 2) return await Result<HrAttLocationEmployeeEditeDto>.FailAsync(localization.GetHrResource("EmpNotActive"));

                var item = await hrRepositoryManager.HrAttLocationEmployeeRepository.GetOne(x => x.Id == entity.Id);
                if (item == null) return await Result<HrAttLocationEmployeeEditeDto>.FailAsync("the Record Is Not Found");
                entity.ModifiedBy = session.UserId;
                entity.ModifiedOn = DateTime.Now;
                _mapper.Map(entity, item);
                item.EmpId = checkEmpExist.Id;
                hrRepositoryManager.HrAttLocationEmployeeRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                return await Result<HrAttLocationEmployeeEditeDto>.SuccessAsync(_mapper.Map<HrAttLocationEmployeeEditeDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrAttLocationEmployeeEditeDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}
