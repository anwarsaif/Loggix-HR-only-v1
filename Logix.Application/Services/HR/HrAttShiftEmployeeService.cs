using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using InvestEmployee = Logix.Domain.Main.InvestEmployee;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Logix.Domain.HR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Reflection.Emit;
using Castle.MicroKernel.SubSystems.Conversion;

namespace Logix.Application.Services.HR
{
    public class HrAttShiftEmployeeService : GenericQueryService<HrAttShiftEmployee, HrAttShiftEmployeeDto, HrAttShiftEmployeeVw>, IHrAttShiftEmployeeService
    {
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;


        public HrAttShiftEmployeeService(IQueryRepository<HrAttShiftEmployee> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;

        }

        public Task<IResult<HrAttShiftEmployeeDto>> Add(HrAttShiftEmployeeDto filter, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrAttShiftEmployeeRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrAttShiftEmployeeDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrAttShiftEmployeeRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAttShiftEmployeeDto>.SuccessAsync(_mapper.Map<HrAttShiftEmployeeDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrAttShiftEmployeeDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrAttShiftEmployeeRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrAttShiftEmployeeDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrAttShiftEmployeeRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrAttShiftEmployeeDto>.SuccessAsync(_mapper.Map<HrAttShiftEmployeeDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrAttShiftEmployeeDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<IEnumerable<InvestEmployee>>> Search1(HrAttShiftEmployeeFilterDto filter)
        {
            try
            {
                filter.ShitId ??= 0;
                var getFromHrAttShiftEmployees = await hrRepositoryManager.HrAttShiftEmployeeRepository.GetAll(x => x.IsDeleted == false && x.BeginDate != null && x.EndDate != null);
                var beginDate = DateHelper.StringToDate(filter.BeginDate);
                var endDate = DateHelper.StringToDate(filter.EndDate);
                var HrAttShiftEmployeesIdsList = getFromHrAttShiftEmployees.Where(x => !string.IsNullOrEmpty(x.BeginDate) && !string.IsNullOrEmpty(x.EndDate) &&
                (DateHelper.StringToDate(x.BeginDate) >= beginDate && DateHelper.StringToDate(x.BeginDate) <= endDate) ||
                (DateHelper.StringToDate(x.EndDate) >= beginDate && DateHelper.StringToDate(x.EndDate) <= endDate) ||
                (beginDate >= DateHelper.StringToDate(x.BeginDate) && beginDate <= DateHelper.StringToDate(x.EndDate)) ||
                (endDate >= DateHelper.StringToDate(x.BeginDate) && endDate <= DateHelper.StringToDate(x.EndDate))

                ).Select(x => x.EmpId).ToList();
                var items = await mainRepositoryManager.InvestEmployeeRepository.GetAll(e => e.IsDeleted == false && e.Isdel == false && e.StatusId == 1 && !HrAttShiftEmployeesIdsList.Contains(e.Id));

                if (items.Count() > 0)
                {

                    var res = items.AsEnumerable();
                    if (!string.IsNullOrEmpty(filter.EmpCode))
                    {
                        res = res.Where(c => c.EmpId != null && c.EmpId == filter.EmpCode);
                    }
                    if (!string.IsNullOrEmpty(filter.EmpName))
                    {
                        res = res.Where(c => (c.EmpName != null && c.EmpName.Contains(filter.EmpName)));
                    }

                    if (filter.BranchId != null && filter.BranchId > 0)
                    {
                        res = res.Where(c => c.BranchId != null && c.BranchId.Equals(filter.BranchId));
                    }
                    if (filter.DeptId != null && filter.DeptId > 0)
                    {
                        res = res.Where(c => c.DeptId != null && c.DeptId.Equals(filter.DeptId));
                    }
                    if (filter.Location != null && filter.Location > 0)
                    {
                        res = res.Where(c => c.Location != null && c.Location.Equals(filter.Location));
                    }
                    if (filter.JobCatagoriesId != null && filter.JobCatagoriesId > 0)
                    {
                        res = res.Where(c => c.JobCatagoriesId != null && c.JobCatagoriesId.Equals(filter.JobCatagoriesId));
                    }

                    if (res.Count() > 0)
                    {
                        return await Result<IEnumerable<InvestEmployee>>.SuccessAsync(res, "", 200);

                    }
                    return await Result<IEnumerable<InvestEmployee>>.SuccessAsync(res, localization.GetResource1("NosearchResult"));
                }
                return await Result<IEnumerable<InvestEmployee>>.SuccessAsync(items, localization.GetResource1("NosearchResult"));


            }
            catch (Exception exp)
            {

                return await Result<List<InvestEmployee>>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
        public async Task<IResult<IEnumerable<HrAttCheckShiftEmployeeVw>>> Search2(HrAttShiftEmployeeFilterDto filter)
        {
            try
            {

                var items = await hrRepositoryManager.HrAttCheckShiftEmployeeVwRepository.GetAll(e => e.IsDeleted == false && e.Isdel == false && e.StatusId == 1);

                if (items.Count() > 0)
                {

                    var res = items.AsEnumerable();
                    if (!string.IsNullOrEmpty(filter.EmpCode))
                    {
                        res = res.Where(c => c.EmpCode != null && c.EmpCode == filter.EmpCode);
                    }
                    if (!string.IsNullOrEmpty(filter.EmpName))
                    {
                        res = res.Where(c => (c.EmpName != null && c.EmpName.ToLower().Contains(filter.EmpName.ToLower())));
                    }

                    if (filter.BranchId != null && filter.BranchId > 0)
                    {
                        res = res.Where(c => c.BranchId != null && c.BranchId.Equals(filter.BranchId));
                    }
                    if (filter.DeptId != null && filter.DeptId > 0)
                    {
                        res = res.Where(c => c.DeptId != null && c.DeptId == filter.DeptId);
                    }
                    if (filter.Location != null && filter.Location > 0)
                    {
                        res = res.Where(c => c.Location != null && c.Location == filter.Location);
                    }
                    if (filter.JobCatagoriesId != null && filter.JobCatagoriesId > 0)
                    {
                        res = res.Where(c => c.JobCatagoriesId != null && c.JobCatagoriesId == filter.JobCatagoriesId);
                    }
                    if (filter.ShitId != null && filter.ShitId > 0)
                    {
                        res = res.Where(c => c.ShitId != null && c.ShitId == filter.ShitId);
                    }


                    if (res.Any())
                    {
                        return await Result<IEnumerable<HrAttCheckShiftEmployeeVw>>.SuccessAsync(res, "", 200);

                    }
                    return await Result<IEnumerable<HrAttCheckShiftEmployeeVw>>.SuccessAsync(res, localization.GetResource1("NosearchResult"));
                }
                return await Result<IEnumerable<HrAttCheckShiftEmployeeVw>>.SuccessAsync(items, localization.GetResource1("NosearchResult"));


            }
            catch (Exception exp)
            {

                return await Result<List<HrAttCheckShiftEmployeeVw>>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public Task<IResult<HrAttShiftEmployeeEditDto>> Update(HrAttShiftEmployeeEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }



        public async Task<IResult<string>> Cancel(List<long?> entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            try
            {
                if (entity.Any())
                {

                    await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                    foreach (var updateitem in entity)
                    {
                        var item = await hrRepositoryManager.HrAttShiftEmployeeRepository.GetOne(x => x.Id == updateitem && x.IsDeleted == false);
                        if (item == null)
                            return await Result<string>.FailAsync($"الصف  {updateitem}  غير موجود");
                        item.ModifiedBy = session.UserId;
                        item.ModifiedOn = DateTime.Now;
                        item.EmpId = updateitem;
                        item.IsDeleted = true;
                        hrRepositoryManager.HrAttShiftEmployeeRepository.Update(item);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                    await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                    return await Result<string>.SuccessAsync(localization.GetResource1("DeleteSuccess"), 200);

                }

                return await Result<string>.FailAsync(" لم يتم تحديد أي موظف من القائمة");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<string>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<string>> Assign(HrAttShiftEmployeeAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
            try
            {
                int Count = 0;
                if (entity.ShitId <= 0) return await Result<string>.FailAsync("يجب تحديد المجموعة");

                if (entity.EmpId.Any())
                {
                    await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                    var InvalidDate = string.Empty;
                    var EmpNotExist = string.Empty;
                    foreach (var newitem in entity.EmpId)
                    {
                        // check if Emp Is Exist
                        var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.Id == newitem && x.IsDeleted == false && x.Isdel == false);
                        if (checkEmpExist == null)
                        {
                            EmpNotExist += $"الموظف رقم  {newitem}  غير موجود" + " , ";
                            continue;
                        }
                        // check if Valid dATE
                        var checkiFValidDate = await IsFound(checkEmpExist.Id, entity.BeginDate, entity.EndDate);
                        if (checkiFValidDate == true)
                        {
                            InvalidDate += " الموظف رقم " + checkEmpExist.EmpId + "مرتبط بمجموعة" + "من تاريخ" + " (" + entity.BeginDate + ")" + "الى تاريخ" + " (" + entity.EndDate + ")" + " , ";
                            continue;
                        }
                        var newAttLocation = new HrAttShiftEmployee
                        {
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            IsDeleted = false,
                            BeginDate = entity.BeginDate,
                            EndDate = entity.EndDate,
                            EmpId = newitem,
                            ShitId = entity.ShitId,
                        };
                        var newEntity = await hrRepositoryManager.HrAttShiftEmployeeRepository.AddAndReturn(newAttLocation);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        Count += 1;

                    }

                    await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                    if (!string.IsNullOrEmpty(InvalidDate) || !string.IsNullOrEmpty(EmpNotExist))
                    {
                        return await Result<string>.SuccessAsync($"{localization.GetResource1("AddSuccess")}  لعدد {Count}" + (string.IsNullOrEmpty(InvalidDate) ? "" : InvalidDate.Substring(0, InvalidDate.Length - 1)) + (string.IsNullOrEmpty(EmpNotExist) ? "" : EmpNotExist.Substring(0, EmpNotExist.Length - 1)));

                    }
                    return await Result<string>.SuccessAsync(localization.GetResource1("AddSuccess"));

                }
                return await Result<string>.FailAsync(" لم يتم تحديد أي موظف من القائمة");
            }
            catch (Exception)
            {

                return await Result<string>.FailAsync(localization.GetResource1("AddError"));
            }
        }



        private async Task<bool> IsFound(long empId, string beginDate, string endDate)
        {
            try
            {
                var getFromHrAttShiftEmployees = await hrRepositoryManager.HrAttShiftEmployeeRepository.GetAll(x => x.IsDeleted == false && x.BeginDate != null && x.EndDate != null && x.EmpId == empId);
                var BeginDate = DateHelper.StringToDate(beginDate);
                var EndDate = DateHelper.StringToDate(endDate);

                var HrAttShiftEmployeesIdsList = getFromHrAttShiftEmployees.Where(x => !string.IsNullOrEmpty(x.BeginDate) && !string.IsNullOrEmpty(x.EndDate) &&
                (DateHelper.StringToDate(x.BeginDate) >= BeginDate && DateHelper.StringToDate(x.BeginDate) <= EndDate) ||
                (DateHelper.StringToDate(x.EndDate) >= BeginDate && DateHelper.StringToDate(x.EndDate) <= EndDate) ||
                (BeginDate >= DateHelper.StringToDate(x.BeginDate) && BeginDate <= DateHelper.StringToDate(x.EndDate)) ||
                (EndDate >= DateHelper.StringToDate(x.BeginDate) && EndDate <= DateHelper.StringToDate(x.EndDate))
                ).Select(x => x.EmpId).ToList();

                if (HrAttShiftEmployeesIdsList.Count() > 0)
                {
                    return true;

                }
                return false;

            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<IResult<string>> AssignUsingExcel(IEnumerable<HrAttShiftEmployeeAddFromExcelDto> entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
            try
            {
                int Count = 0;
                if (entity.Any())
                {
                    await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                    var InvalidDate = string.Empty;
                    var EmpNotExist = string.Empty;
                    foreach (var newitem in entity)
                    {
                        // check if Emp Is Exist
                        var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == newitem.empCode && x.IsDeleted == false && x.Isdel == false);
                        if (checkEmpExist == null)
                        {
                            EmpNotExist += $"الموظف رقم  {newitem}  غير موجود" + " , ";
                            continue;
                        }
                        // check if Valid dATE
                        var checkiFValidDate = await IsFound(checkEmpExist.Id, newitem.BeginDate, newitem.EndDate);
                        if (checkiFValidDate == true)
                        {
                            InvalidDate += " الموظف رقم " + checkEmpExist.EmpId + "مرتبط بمجموعة" + "من تاريخ" + " (" + newitem.BeginDate + ")" + "الى تاريخ" + " (" + newitem.EndDate + ")" + " , ";
                            continue;
                        }
                        var newAttLocation = new HrAttShiftEmployee
                        {
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            IsDeleted = false,
                            BeginDate = newitem.BeginDate,
                            EndDate = newitem.EndDate,
                            EmpId = checkEmpExist.Id,
                            ShitId = newitem.ShitId,
                        };
                        var newEntity = await hrRepositoryManager.HrAttShiftEmployeeRepository.AddAndReturn(newAttLocation);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        Count += 1;
                    }
                    await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                    if (!string.IsNullOrEmpty(InvalidDate) || !string.IsNullOrEmpty(EmpNotExist))
                    {
                        return await Result<string>.SuccessAsync($"{localization.GetResource1("AddSuccess")}  لعدد {Count}" + (string.IsNullOrEmpty(InvalidDate) ? "" : InvalidDate.Substring(0, InvalidDate.Length - 1)) + (string.IsNullOrEmpty(EmpNotExist) ? "" : EmpNotExist.Substring(0, EmpNotExist.Length - 1)));

                    }
                    return await Result<string>.SuccessAsync(localization.GetResource1("AddSuccess"));

                }
                return await Result<string>.FailAsync(" لم يتم تحديد أي موظف من القائمة");
            }
            catch (Exception)
            {

                return await Result<string>.FailAsync(localization.GetResource1("AddError"));
            }
        }
    }


}