using AutoMapper;
using Castle.MicroKernel.Registration;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Logix.Domain.Main;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvestEmployee = Logix.Domain.Main.InvestEmployee;

namespace Logix.Application.Services.HR
{
    public class HrInsuranceService : GenericQueryService<HrInsurance, HrInsuranceDto, HrInsurance>, IHrInsuranceService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;

        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;
        private readonly IMainServiceManager mainServiceManager;
        private readonly IWorkflowHelper workflowHelper;
        public HrInsuranceService(IQueryRepository<HrInsurance> queryRepository, IHrRepositoryManager hrRepositoryManager, IMainRepositoryManager mainRepositoryManager, IMainServiceManager mainServiceManager, IMapper mapper, ICurrentData session, ILocalizationService localization, IWorkflowHelper workflowHelper) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
            this.mainServiceManager = mainServiceManager;
            this.workflowHelper = workflowHelper;   
        }


        public async Task<IResult<HrInsuranceDto>> Add(HrInsuranceDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrInsuranceDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                long? Code = 0;
                entity.AppId ??= 0;


                var InsurancePolicyItem = await hrRepositoryManager.HrInsurancePolicyRepository.GetOne(e => e.Id == entity.PolicyId);
                if (InsurancePolicyItem == null)
                    return await Result<HrInsuranceDto>.FailAsync("بوليصة التأمين غير موجودة");
                var getcodeCount = await hrRepositoryManager.HrInsuranceRepository.GetAll(x => x.TransTypeId == 1);
                if (getcodeCount.Count() >= 1)
                {

                    Code = getcodeCount.Count() + 1;
                }
                var GetApp_ID = await workflowHelper.Send(session.EmpId, 1252, entity.AppId);

                var InsurancePolicy = new HrInsurance
                {
                    Code = Code,
                    IsDeleted = false,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    TransTypeId = 1,
                    InsuranceType = entity.InsuranceType,
                    PolicyId = entity.PolicyId,
                    InsuranceDate = entity.InsuranceDate,
                    Total = entity.Total,
                    Note = entity.Note,
                    WfStatusId = 1,
                    AppId = GetApp_ID,



                };

                var newEntity = await hrRepositoryManager.HrInsuranceRepository.AddAndReturn(InsurancePolicy);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                var entityMap = _mapper.Map<HrInsuranceDto>(newEntity);

                if (entity.InsuranceEmp.Any())
                {
                    var AllEmployee = await mainRepositoryManager.InvestEmployeeRepository.GetAll(e => e.IsDeleted == false && e.Isdel == false);
                    foreach (var singleItem in entity.InsuranceEmp)
                    {
                        var Employee = AllEmployee.Where(e => e.EmpId == singleItem.EmpCode).FirstOrDefault();

                        if (Employee == null)
                            return await Result<HrInsuranceDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                        if (singleItem.DependentId == 0)
                        {
                            singleItem.DependentId = 0;
                            singleItem.ToDependents = false;
                        }
                        else
                        {
                            singleItem.ToDependents = true;
                            singleItem.DependentId = singleItem.DependentId;
                        }
                        var newsingleItem = new HrInsuranceEmp
                        {
                            DependentId = singleItem.DependentId,
                            ClassId = singleItem.ClassId,
                            //ClassId = Employee.InsuranceCategory,
                            ToDependents = singleItem.ToDependents,
                            EmpId = Employee.Id,
                            Amount = singleItem.Amount,
                            InsuranceCardNo = singleItem.InsuranceCardNo,
                            Note = singleItem.Note,
                            InsuranceId = newEntity.Id,
                            RefranceInsEmpId = 0,
                            IsDeleted = false,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,


                        };
                        await hrRepositoryManager.HrInsuranceEmpRepository.Add(newsingleItem);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                        //  تحديث البيانات على جدول الموظفين بفئة التأمين ورقم بطاقة التأمين وصلاحية التأمين 

                        Employee.InsuranceDateValidity = InsurancePolicyItem.EndDate;
                        Employee.InsuranceCompany = ((int?)InsurancePolicyItem.CompanyId);
                        Employee.InsuranceCardNo = singleItem.InsuranceCardNo;
                        Employee.InsuranceCategory = singleItem.ClassId;
                        Employee.ModifiedBy = session.UserId;
                        Employee.ModifiedOn = DateTime.Now;
                        mainRepositoryManager.InvestEmployeeRepository.Update(Employee);
                        await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }



                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, newEntity.Id, 82);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<HrInsuranceDto>.SuccessAsync(localization.GetResource1("CreateSuccess"));

            }


            catch (Exception exc)
            {
                return await Result<HrInsuranceDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }


        //public async Task<IResult<HrInsuranceDto>> AddInsuranceExclude(HrInsuranceDto entity, CancellationToken cancellationToken = default)
        //{
        //	if (entity == null)
        //		return await Result<HrInsuranceDto>.FailAsync(localization.GetMessagesResource("AddNullEntity"));

        //	try
        //	{
        //		await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

        //		entity.AppId ??= 0;

        //		// التحقق من البوليصة
        //		var InsurancePolicyItem = await hrRepositoryManager.HrInsurancePolicyRepository
        //			.GetOne(e => e.Id == entity.PolicyId);
        //		if (InsurancePolicyItem == null)
        //			return await Result<HrInsuranceDto>.FailAsync("بوليصة التأمين غير موجودة");

        //		// جلب آخر كود
        //		var lastCode = await hrRepositoryManager.HrInsuranceRepository
        //			.Entities
        //			.Where(x => x.TransTypeId == 1)
        //			.OrderByDescending(x => x.Code)
        //			.Select(x => x.Code)
        //			.FirstOrDefaultAsync(cancellationToken);

        //		var Code = (lastCode ?? 0) + 1;

        //		// إرسال إلى سير العمل
        //		var GetApp_ID = await workflowHelper.Send(session.EmpId, 1253, entity.AppId);

        //		// إنشاء كيان رئيسي للتأمين
        //		var InsurancePolicy = new HrInsurance
        //		{
        //			Code = Code,
        //			IsDeleted = false,
        //			CreatedBy = session.UserId,
        //			CreatedOn = DateTime.Now,
        //			TransTypeId = 2,
        //			InsuranceType = entity.InsuranceType,
        //			PolicyId = entity.PolicyId,
        //			InsuranceDate = entity.InsuranceDate,
        //			Total = entity.Total,
        //			Note = entity.Note,
        //			WfStatusId = 1,
        //			AppId = GetApp_ID,
        //		};

        //		var newEntity = await hrRepositoryManager.HrInsuranceRepository.AddAndReturn(InsurancePolicy);

        //		// إضافة تفاصيل الموظفين
        //		if (entity.InsuranceEmp?.Any() == true)
        //		{
        //			var AllEmployee = await mainRepositoryManager.InvestEmployeeRepository
        //				.GetAll(e => e.IsDeleted==false && e.Isdel==false);

        //			foreach (var singleItem in entity.InsuranceEmp)
        //			{
        //				var Employee = AllEmployee.FirstOrDefault(e => e.EmpId == singleItem.EmpCode);
        //				if (Employee == null)
        //					return await Result<HrInsuranceDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
        //				if (singleItem.DependentId == 0)
        //				{
        //					singleItem.DependentId = 0;
        //					singleItem.ToDependents = false;
        //				}
        //				else
        //				{
        //					singleItem.ToDependents = true;
        //					singleItem.DependentId = singleItem.DependentId;
        //				}

        //				var newsingleItem = new HrInsuranceEmp
        //				{
        //					DependentId = singleItem.DependentId,
        //					ClassId = singleItem.ClassId,
        //					ToDependents = singleItem.ToDependents,
        //					EmpId = Employee.Id,
        //					Amount = singleItem.Amount,
        //					InsuranceCardNo = singleItem.InsuranceCardNo,
        //					Note = singleItem.Note,
        //					InsuranceId = newEntity.Id,
        //					RefranceInsEmpId = 0,
        //					IsDeleted = false,
        //					CreatedBy = session.UserId,
        //					CreatedOn = DateTime.Now,
        //				};

        //				await hrRepositoryManager.HrInsuranceEmpRepository.Add(newsingleItem);

        //				// تحديث بيانات الموظف
        //				Employee.InsuranceDateValidity = InsurancePolicyItem.EndDate;
        //				Employee.InsuranceCompany = (int?)InsurancePolicyItem.CompanyId;
        //				Employee.InsuranceCardNo = singleItem.InsuranceCardNo;
        //				Employee.InsuranceCategory = singleItem.ClassId;
        //				Employee.ModifiedBy = session.UserId;
        //				Employee.ModifiedOn = DateTime.Now;

        //				mainRepositoryManager.InvestEmployeeRepository.Update(Employee);
        //			}
        //		}

        //		// حفظ المرفقات
        //		if (entity.fileDtos?.Any() == true)
        //		{
        //			await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, newEntity.Id, 82);
        //		}

        //		await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
        //		await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

        //		return await Result<HrInsuranceDto>.SuccessAsync(localization.GetResource1("CreateSuccess"));
        //	}
        //	catch (Exception exc)
        //	{
        //		await hrRepositoryManager.UnitOfWork.RollbackTransactionAsync(cancellationToken);
        //		return await Result<HrInsuranceDto>.FailAsync($"EXP in {this.GetType()}, Message: {exc.Message}");
        //	}
        //}








        public async Task<IResult<HrInsuranceDto>> AddInsuranceExclude(HrInsuranceDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrInsuranceDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                long? Code = 0;
                entity.AppId ??= 0;


                var InsurancePolicyItem = await hrRepositoryManager.HrInsurancePolicyRepository.GetOne(e => e.Id == entity.PolicyId);
                if (InsurancePolicyItem == null)
                    return await Result<HrInsuranceDto>.FailAsync("بوليصة التأمين غير موجودة");
                var getcodeCount = await hrRepositoryManager.HrInsuranceRepository.GetAll(x => x.TransTypeId == 1);
                if (getcodeCount.Count() >= 1)
                {

                    Code = getcodeCount.Count() + 1;
                }
                var GetApp_ID = await workflowHelper.Send(session.EmpId, 1253, entity.AppId);

                var InsurancePolicy = new HrInsurance
                {
                    Code = Code,
                    IsDeleted = false,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    TransTypeId = 2,
                    InsuranceType = entity.InsuranceType,
                    PolicyId = entity.PolicyId,
                    InsuranceDate = entity.InsuranceDate,
                    Total = entity.Total,
                    Note = entity.Note,
                    WfStatusId = 1,
                    AppId = GetApp_ID,



                };

                var newEntity = await hrRepositoryManager.HrInsuranceRepository.AddAndReturn(InsurancePolicy);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                var entityMap = _mapper.Map<HrInsuranceDto>(newEntity);

                if (entity.InsuranceEmp.Any())
                {
                    var AllEmployee = await mainRepositoryManager.InvestEmployeeRepository.GetAll(e => e.IsDeleted == false && e.Isdel == false);
                    foreach (var singleItem in entity.InsuranceEmp)
                    {
                        var Employee = AllEmployee.Where(e => e.EmpId == singleItem.EmpCode).FirstOrDefault();

                        if (Employee == null)
                            return await Result<HrInsuranceDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                        if (singleItem.DependentId == 0)
                        {
                            singleItem.DependentId = 0;
                            singleItem.ToDependents = false;
                        }
                        else
                        {
                            singleItem.ToDependents = true;
                            singleItem.DependentId = singleItem.DependentId;
                        }
                        var newsingleItem = new HrInsuranceEmp
                        {
                            DependentId = singleItem.DependentId,
                            ClassId = singleItem.ClassId,
                            ToDependents = singleItem.ToDependents,
                            EmpId = Employee.Id,
                            Amount = singleItem.Amount,
                            InsuranceCardNo = singleItem.InsuranceCardNo,
                            Note = singleItem.Note,
                            InsuranceId = newEntity.Id,
                            RefranceInsEmpId = 0,
                            IsDeleted = false,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,


                        };
                        await hrRepositoryManager.HrInsuranceEmpRepository.Add(newsingleItem);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                        //  تحديث البيانات على جدول الموظفين بفئة التأمين ورقم بطاقة التأمين وصلاحية التأمين 

                        Employee.InsuranceDateValidity = InsurancePolicyItem.EndDate;
                        Employee.InsuranceCompany = ((int?)InsurancePolicyItem.CompanyId);
                        Employee.InsuranceCardNo = singleItem.InsuranceCardNo;
                        Employee.InsuranceCategory = singleItem.ClassId;
                        Employee.ModifiedBy = session.UserId;
                        Employee.ModifiedOn = DateTime.Now;
                        mainRepositoryManager.InvestEmployeeRepository.Update(Employee);
                        await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }



                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, newEntity.Id, 82);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<HrInsuranceDto>.SuccessAsync(localization.GetResource1("CreateSuccess"));

            }


            catch (Exception exc)
            {
                return await Result<HrInsuranceDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var item = await hrRepositoryManager.HrInsuranceRepository.GetById(Id);
                if (item == null) return Result<HrInsuranceDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;
                hrRepositoryManager.HrInsuranceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var getAllInsuranceEmployee = await hrRepositoryManager.HrInsuranceEmpRepository.GetAll(x => x.IsDeleted == false && x.InsuranceId == Id);
                if (getAllInsuranceEmployee != null)
                {
                    foreach (var SingleItem in getAllInsuranceEmployee)
                    {
                        SingleItem.ModifiedBy = session.UserId;
                        SingleItem.ModifiedOn = DateTime.Now;
                        SingleItem.IsDeleted = true;
                        hrRepositoryManager.HrInsuranceEmpRepository.Update(SingleItem);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrInsuranceDto>.SuccessAsync(_mapper.Map<HrInsuranceDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrInsuranceDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                var item = await hrRepositoryManager.HrInsuranceRepository.GetById(Id);
                if (item == null) return Result<HrInsuranceDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;
                hrRepositoryManager.HrInsuranceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var getAllInsuranceEmployee = await hrRepositoryManager.HrInsuranceEmpRepository.GetAll(x => x.IsDeleted == false && x.InsuranceId == Id);
                if (getAllInsuranceEmployee != null)
                {
                    foreach (var SingleItem in getAllInsuranceEmployee)
                    {
                        SingleItem.ModifiedBy = session.UserId;
                        SingleItem.ModifiedOn = DateTime.Now;
                        SingleItem.IsDeleted = true;
                        hrRepositoryManager.HrInsuranceEmpRepository.Update(SingleItem);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrInsuranceDto>.SuccessAsync(_mapper.Map<HrInsuranceDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrInsuranceDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }


        public async Task<IResult<HrInsuranceEditDto>> Update(HrInsuranceEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrInsuranceEditDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);


                var InsurancePolicyItem = await hrRepositoryManager.HrInsurancePolicyRepository.GetOne(e => e.Id == entity.PolicyId);
                if (InsurancePolicyItem == null)
                    return await Result<HrInsuranceEditDto>.FailAsync("بوليصة التأمين غير موجودة");
                var getcodeCount = await hrRepositoryManager.HrInsuranceRepository.GetAll(x => x.TransTypeId == 1);

                var checkInsuranceExist = await hrRepositoryManager.HrInsuranceRepository.GetById(entity.Id);
                if (checkInsuranceExist == null) return await Result<HrInsuranceEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");


                checkInsuranceExist.IsDeleted = false;
                checkInsuranceExist.ModifiedBy = session.UserId;
                checkInsuranceExist.ModifiedOn = DateTime.Now;
                checkInsuranceExist.InsuranceType = entity.InsuranceType;
                checkInsuranceExist.PolicyId = entity.PolicyId;
                checkInsuranceExist.InsuranceDate = entity.InsuranceDate;
                checkInsuranceExist.Total = entity.Total;
                checkInsuranceExist.Note = entity.Note;
                hrRepositoryManager.HrInsuranceRepository.Update(checkInsuranceExist);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);



                if (entity.InsuranceEmp.Any())
                {
                    var AllEmployee = await mainRepositoryManager.InvestEmployeeRepository.GetAll(e => e.IsDeleted == false && e.Isdel == false);
                    var AllEmployeeInsurance = await hrRepositoryManager.HrInsuranceEmpRepository.GetAll(e => e.IsDeleted == false && e.InsuranceId == entity.Id);
                    foreach (var singleItem in entity.InsuranceEmp)
                    {
                        var Employee = AllEmployee.Where(e => e.EmpId == singleItem.EmpCode).FirstOrDefault();
                        if (Employee == null)
                            return await Result<HrInsuranceEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                        if (singleItem.DependentId == 0)
                        {
                            singleItem.DependentId = 0;
                            singleItem.ToDependents = false;
                        }
                        else
                        {
                            singleItem.ToDependents = true;
                            singleItem.DependentId = singleItem.DependentId;
                        }
                        if (singleItem.IsDeleted == false && singleItem.Id == 0)
                        {

                            var newsingleItem = new HrInsuranceEmp
                            {
                                DependentId = singleItem.DependentId,
                                ClassId = singleItem.ClassId,
                                ToDependents = singleItem.ToDependents,
                                EmpId = Employee.Id,
                                Amount = singleItem.Amount,
                                InsuranceCardNo = singleItem.InsuranceCardNo,
                                Note = singleItem.Note,
                                InsuranceId = entity.Id,
                                RefranceInsEmpId = 0,
                                IsDeleted = false,
                                CreatedBy = session.UserId,
                                CreatedOn = DateTime.Now,

                            };
                            await hrRepositoryManager.HrInsuranceEmpRepository.Add(newsingleItem);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }
                        else
                        {
                            var getEmpInsurance = AllEmployeeInsurance.Where(x => x.Id == singleItem.Id).SingleOrDefault();
                            if (getEmpInsurance == null) continue;
                            getEmpInsurance.IsDeleted = singleItem.IsDeleted;
                            getEmpInsurance.ModifiedBy = session.UserId;
                            getEmpInsurance.ModifiedOn = DateTime.Now;
                            getEmpInsurance.Amount = singleItem.Amount;
                            getEmpInsurance.DependentId = singleItem.DependentId;
                            getEmpInsurance.ClassId = singleItem.ClassId;
                            getEmpInsurance.ToDependents = singleItem.ToDependents;
                            getEmpInsurance.EmpId = Employee.Id;
                            getEmpInsurance.InsuranceCardNo = singleItem.InsuranceCardNo;
                            getEmpInsurance.Note = singleItem.Note;
                            getEmpInsurance.InsuranceId = entity.Id;
                            getEmpInsurance.RefranceInsEmpId = 0;
                            hrRepositoryManager.HrInsuranceEmpRepository.Update(getEmpInsurance);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }
                        //  تحديث البيانات على جدول الموظفين بفئة التأمين ورقم بطاقة التأمين وصلاحية التأمين 

                        Employee.InsuranceDateValidity = InsurancePolicyItem.EndDate;
                        Employee.InsuranceCompany = ((int?)InsurancePolicyItem.CompanyId);
                        Employee.InsuranceCardNo = singleItem.InsuranceCardNo;
                        Employee.InsuranceCategory = singleItem.ClassId;
                        Employee.ModifiedBy = session.UserId;
                        Employee.ModifiedOn = DateTime.Now;
                        mainRepositoryManager.InvestEmployeeRepository.Update(Employee);
                        await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }



                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, entity.Id, 82);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<HrInsuranceEditDto>.SuccessAsync(localization.GetResource1("UpdateSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<HrInsuranceEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public Task<IResult<bool>> viewInsuranceEmp(List<HrInsuranceEmpVM> viewInsuranceEmp, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}

