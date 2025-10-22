using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.ACC;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.ACC;
using Logix.Domain.HR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Linq;
using System.Security;
using IResult = Logix.Application.Wrapper.IResult;

namespace Logix.Application.Services.HR
{
	public class HrProvisionsMedicalInsuranceService : GenericQueryService<HrProvisionsMedicalInsurance, HrProvisionsMedicalInsuranceDto, HrProvisionsMedicalInsuranceVw>, IHrProvisionsMedicalInsuranceService
	{
		private readonly IHrRepositoryManager hrRepositoryManager;
		private readonly ILocalizationService localization;
		private readonly IMainRepositoryManager mainRepositoryManager;
		private readonly IMapper _mapper;
		private readonly ICurrentData session;
		private readonly IAccRepositoryManager accRepositoryManager;
		private readonly ISysConfigurationAppHelper sysConfigurationAppHelper;


		public HrProvisionsMedicalInsuranceService(IQueryRepository<HrProvisionsMedicalInsurance> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization, IAccRepositoryManager accRepositoryManager, ISysConfigurationAppHelper sysConfigurationAppHelper) : base(queryRepository, mapper)
		{
			this.mainRepositoryManager = mainRepositoryManager;
			this._mapper = mapper;
			this.session = session;
			this.hrRepositoryManager = hrRepositoryManager;
			this.localization = localization;
			this.accRepositoryManager = accRepositoryManager;
			this.sysConfigurationAppHelper = sysConfigurationAppHelper;
		}


		#region Generic Functions

		public async Task<IResult<HrProvisionsMedicalInsuranceDto>> Add(HrProvisionsMedicalInsuranceDto entity, CancellationToken cancellationToken = default)
		{
			if (entity == null) return await Result<HrProvisionsMedicalInsuranceDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

			try
			{
				if (entity.EmpCodes.Count() <= 0)
					return await Result<HrProvisionsMedicalInsuranceDto>.FailAsync($"لم يتم تحديد اي مخصص");

				int countMonth = (entity.YearlyOrMonthly == 1) ? 1 : 12;

				long getMaxForCode = 0;
				string Code = "PRO-";
				await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

				getMaxForCode = hrRepositoryManager.HrProvisionsMedicalInsuranceRepository.Entities.Where(x => x.FacilityId == session.FacilityId).Max(x => x.Id);
				getMaxForCode++;
				Code += getMaxForCode.ToString();

				var newHrProvisionEntity = new HrProvisionsMedicalInsurance
				{
					Code = Code,
					CreatedBy = session.UserId,
					CreatedOn = DateTime.Now,
					FacilityId = session.FacilityId,
					YearlyOrMonthly = entity.YearlyOrMonthly,
					Description = entity.Description,
					MonthId = entity.MonthId,
					FinYear = entity.FinYear,
					PDate = entity.PDate,
					No = 0,
					//IsDeleted = false

				};

				var AddedProvisionEntity = await hrRepositoryManager.HrProvisionsMedicalInsuranceRepository.AddAndReturn(newHrProvisionEntity);
				await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				//  Add  Provision Employee

				var getFromEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);

				var getAllAllowanceDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAllFromView(e =>
					e.IsDeleted == false &&
					e.Status == true &&
					e.FixedOrTemporary == 1);
				foreach (var empCode in entity.EmpCodes)
				{
					var empDate = getFromEmployees.Where(x => x.EmpId == empCode).FirstOrDefault();
					if (empDate == null)
						return await Result<HrProvisionsMedicalInsuranceDto>.FailAsync((session.Language == 1) ? $"لا يوجد موظف بهذا الرقم : {empCode}" : $"There is No Employee With This Code {empCode}");

					var getApplyPloicyValue = await mainRepositoryManager.DbFunctionsRepository.ApplyPolicies(session.FacilityId, 4, empDate.Id);
					var CalculateAmount = (getApplyPloicyValue / countMonth / 30 * empDate.VacationDaysYear) ?? 0;
					if (CalculateAmount <= 0) continue;

					decimal salary = empDate.Salary ?? 0;
					decimal totalAllowance = getAllAllowanceDeduction
						.Where(a => a.EmpId == empDate.Id && a.TypeId == 1)
						.Sum(a => a.Amount) ?? 0;

					decimal totalDeduction = getAllAllowanceDeduction
						.Where(a => a.EmpId == empDate.Id && a.TypeId == 2)
						.Sum(a => a.Amount) ?? 0;


					var newHrProvisionsEmployeeEntity = new HrProvisionsMedicalInsuranceEmployee
					{
						EmpId = empDate.Id,
						CreatedBy = session.UserId,
						CreatedOn = DateTime.Now,
						IsDeleted = false,
						PId = AddedProvisionEntity.Id,
						LocationId = empDate.Location ?? 0,
						DeptId = empDate.DeptId,
						BranchId = empDate.BranchId ?? 0,
						FacilityId = session.FacilityId,
						CcId = empDate.CcId ?? 0,
						CurrentInsuranceAmount = totalAllowance,
						TotalPreInsuranceAmount = totalDeduction,
						PolicyId = (long?)getApplyPloicyValue,
						//BasicSalary = salary,
						//TotalSalary = salary + totalAllowance,
						ExcludedValue = salary + totalAllowance - totalDeduction,
						InsuranceAmount = CalculateAmount,

					};
					var AddedHrProvisionsEmployeeEntity = await hrRepositoryManager.HrProvisionsMedicalInsuranceEmployeeRepository.AddAndReturn(newHrProvisionsEmployeeEntity);
					await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
				}

				await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
				return await Result<HrProvisionsMedicalInsuranceDto>.SuccessAsync(localization.GetResource1("AddSuccess"), 200);
			}
			catch (Exception exp)
			{
				return await Result<HrProvisionsMedicalInsuranceDto>.FailAsync($"EXP  at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
			}
		}

		//public async Task<IResult<HrProvisionsMedicalInsuranceDto>> Add(HrProvisionsMedicalInsuranceDto entity, CancellationToken cancellationToken = default)
		//{
		//	if (entity == null) return await Result<HrProvisionsMedicalInsuranceDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

		//	try
		//	{
		//		if (entity.EmpCodes.Count() <= 0)
		//			return await Result<HrProvisionsMedicalInsuranceDto>.FailAsync($"لم يتم تحديد اي مخصص");

		//		int countMonth = (entity.YearlyOrMonthly == 1) ? 1 : 12;

		//		long getMaxForCode = 0;
		//		string Code = "PRO-";
		//		await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

		//		// FIXED: Safe way to get maximum ID
		//		getMaxForCode = hrRepositoryManager.HrProvisionsMedicalInsuranceRepository.Entities
		//			.Where(x => x.FacilityId == session.FacilityId)
		//			.Select(x => x.Id)
		//			.DefaultIfEmpty(0)
		//			.Max();

		//		getMaxForCode++;
		//		Code += getMaxForCode.ToString();

		//		var newHrProvisionEntity = new HrProvisionsMedicalInsurance
		//		{
		//			Code = Code,
		//			CreatedBy = session.UserId,
		//			CreatedOn = DateTime.Now,
		//			FacilityId = session.FacilityId,
		//			YearlyOrMonthly = entity.YearlyOrMonthly,
		//			Description = entity.Description,
		//			MonthId = entity.MonthId,
		//			FinYear = entity.FinYear,
		//			PDate = entity.PDate,
		//			No = 0,
		//			IsDeleted=false
		//		};

		//		var AddedProvisionEntity = await hrRepositoryManager.HrProvisionsMedicalInsuranceRepository.AddAndReturn(newHrProvisionEntity);
		//		await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

		//		// Rest of your code remains the same...
		//		var getFromEmployees = await hrRepositoryManager.HrEmployeeRepository.GetAll(x => x.IsDeleted == false && x.Isdel == false);

		//		var getAllAllowanceDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAllFromView(e =>
		//			e.IsDeleted == false &&
		//			e.Status == true &&
		//			e.FixedOrTemporary == 1);

		//		foreach (var empCode in entity.EmpCodes)
		//		{
		//			var empDate = getFromEmployees.Where(x => x.EmpId == empCode).FirstOrDefault();
		//			if (empDate == null)
		//				return await Result<HrProvisionsMedicalInsuranceDto>.FailAsync((session.Language == 1) ? $"لا يوجد موظف بهذا الرقم : {empCode}" : $"There is No Employee With This Code {empCode}");

		//			var getApplyPloicyValue = await mainRepositoryManager.DbFunctionsRepository.ApplyPolicies(session.FacilityId, 4, empDate.Id);
		//			var CalculateAmount = (getApplyPloicyValue / countMonth / 30 * empDate.VacationDaysYear) ?? 0;
		//			if (CalculateAmount <= 0) continue;

		//			decimal salary = empDate.Salary ?? 0;
		//			decimal totalAllowance = getAllAllowanceDeduction
		//				.Where(a => a.EmpId == empDate.Id && a.TypeId == 1)
		//				.Sum(a => a.Amount) ?? 0;

		//			decimal totalDeduction = getAllAllowanceDeduction
		//				.Where(a => a.EmpId == empDate.Id && a.TypeId == 2)
		//				.Sum(a => a.Amount) ?? 0;

		//			var newHrProvisionsEmployeeEntity = new HrProvisionsMedicalInsuranceEmployee
		//			{
		//				EmpId = empDate.Id,
		//				CreatedBy = session.UserId,
		//				CreatedOn = DateTime.Now,
		//				IsDeleted = false,
		//				PId = AddedProvisionEntity.Id,
		//				LocationId = empDate.Location ?? 0,
		//				DeptId = empDate.DeptId,
		//				BranchId = empDate.BranchId ?? 0,
		//				FacilityId = session.FacilityId,
		//				CcId = empDate.CcId ?? 0,
		//				CurrentInsuranceAmount = totalAllowance,
		//				TotalPreInsuranceAmount = totalDeduction,
		//				PolicyId = (long)getApplyPloicyValue,
		//				InsuranceAmount = salary + totalAllowance - totalDeduction,
		//				ExcludedValue = CalculateAmount,
		//			};

		//			var AddedHrProvisionsEmployeeEntity = await hrRepositoryManager.HrProvisionsMedicalInsuranceEmployeeRepository.AddAndReturn(newHrProvisionsEmployeeEntity);
		//			await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
		//		}

		//		await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
		//		return await Result<HrProvisionsMedicalInsuranceDto>.SuccessAsync(localization.GetResource1("AddSuccess"), 200);
		//	}
		//	catch (Exception exp)
		//	{
		//		await hrRepositoryManager.UnitOfWork.RollbackTransactionAsync(cancellationToken);
		//		return await Result<HrProvisionsMedicalInsuranceDto>.FailAsync($"EXP at ({this.GetType()}), Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
		//	}
		//}

		public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
		{
			try
			{
				await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

				var item = await hrRepositoryManager.HrProvisionsMedicalInsuranceRepository.GetOne(x => x.Id == Id);
				if (item == null) return Result<HrProvisionsMedicalInsuranceDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");
				// Start of Check Status ID

				int? status = await accRepositoryManager.AccJournalMasterRepository.GetJournalMasterStatuse(Convert.ToInt32(Id), 54);

				if (status == 2)
				{
					return await Result<HrProvisionsMedicalInsuranceDto>.FailAsync($"{localization.GetResource1("AccrualCannotBeDeletedDueToMigration")} ");

				}
				else
				{
					await accRepositoryManager.AccJournalMasterRepository.DeleteJournalWithDetailsbyReference(Id, 54);
					await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
				}
				// End of Check Status ID


				// start of delelte Provision 

				item.IsDeleted = true;
				item.ModifiedBy = session.UserId;
				item.ModifiedOn = DateTime.Now;
				hrRepositoryManager.HrProvisionsMedicalInsuranceRepository.Update(item);
				await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
				// end  of delelte Provision 

				// start of delelte Provision Employee

				var getAllProvisionEmployee = await hrRepositoryManager.HrProvisionsMedicalInsuranceEmployeeRepository.GetAll(x => x.IsDeleted == false && x.PId == Id);
				if (getAllProvisionEmployee != null)
				{
					foreach (var singleProvisionEmployee in getAllProvisionEmployee)
					{
						singleProvisionEmployee.IsDeleted = true;
						singleProvisionEmployee.ModifiedBy = session.UserId;
						singleProvisionEmployee.ModifiedOn = DateTime.Now;
						hrRepositoryManager.HrProvisionsMedicalInsuranceEmployeeRepository.Update(singleProvisionEmployee);
						await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
					}
				}

				// End of delelte Provision Employee

				await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

				return await Result<HrProvisionsMedicalInsuranceDto>.SuccessAsync(_mapper.Map<HrProvisionsMedicalInsuranceDto>(item), localization.GetResource1("DeleteSuccess"));
			}
			catch (Exception exp)
			{
				return await Result<HrProvisionsMedicalInsuranceDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
			}
		}

		public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
		{
			try
			{
				await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

				var item = await hrRepositoryManager.HrProvisionsMedicalInsuranceRepository.GetOne(x => x.Id == Id);
				if (item == null) return Result<HrProvisionsMedicalInsuranceDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");
				// Start of Check Status ID

				int? status = await accRepositoryManager.AccJournalMasterRepository.GetJournalMasterStatuse(Convert.ToInt32(Id), 54);

				if (status == 2)
				{
					return await Result<HrProvisionsMedicalInsuranceDto>.FailAsync($"{localization.GetResource1("AccrualCannotBeDeletedDueToMigration")} ");

				}
				else
				{
					await accRepositoryManager.AccJournalMasterRepository.DeleteJournalWithDetailsbyReference(Id, 54);
					await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
				}
				// End of Check Status ID


				// start of delelte Provision 

				item.IsDeleted = true;
				item.ModifiedBy = session.UserId;
				item.ModifiedOn = DateTime.Now;
				hrRepositoryManager.HrProvisionsMedicalInsuranceRepository.Update(item);
				await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
				// end  of delelte Provision 

				// start of delelte Provision Employee

				var getAllProvisionEmployee = await hrRepositoryManager.HrProvisionsMedicalInsuranceEmployeeRepository.GetAll(x => x.IsDeleted == false && x.PId == Id);
				if (getAllProvisionEmployee != null)
				{
					foreach (var singleProvisionEmployee in getAllProvisionEmployee)
					{
						singleProvisionEmployee.IsDeleted = true;
						singleProvisionEmployee.ModifiedBy = session.UserId;
						singleProvisionEmployee.ModifiedOn = DateTime.Now;
						hrRepositoryManager.HrProvisionsMedicalInsuranceEmployeeRepository.Update(singleProvisionEmployee);
						await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
					}
				}

				// End of delelte Provision Employee

				await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

				return await Result<HrProvisionsMedicalInsuranceDto>.SuccessAsync(_mapper.Map<HrProvisionsMedicalInsuranceDto>(item), localization.GetResource1("DeleteSuccess"));
			}
			catch (Exception exp)
			{
				return await Result<HrProvisionsMedicalInsuranceDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
			}
		}

		public async Task<IResult<HrProvisionsMedicalInsuranceEditDto>> Update(HrProvisionsMedicalInsuranceEditDto entity, CancellationToken cancellationToken = default)
		{
			try
			{
				await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

				var item = await hrRepositoryManager.HrProvisionsMedicalInsuranceRepository.GetById(entity.Id);
				if (item == null) return await Result<HrProvisionsMedicalInsuranceEditDto>.FailAsync($"{localization.GetMessagesResource("NoItemFoundToEdit")}");


				int? status = await accRepositoryManager.AccJournalMasterRepository.GetJournalMasterStatuse(Convert.ToInt32(entity.Id), 54);

				if (status == 2)
					return await Result<HrProvisionsMedicalInsuranceEditDto>.FailAsync($"{localization.GetResource1("AccrualCannotBeEditDueToMigration")} ");

				item.ModifiedBy = session.UserId;
				item.ModifiedOn = DateTime.Now;
				item.Description = entity.Description;

				hrRepositoryManager.HrProvisionsMedicalInsuranceRepository.Update(item);
				await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
				var getFromProvisionEmployees = await hrRepositoryManager.HrProvisionsMedicalInsuranceEmployeeRepository.GetAll(x => x.IsDeleted == false && x.PId == entity.Id);
				// نمر على جميع العناصر لمعرفه حالة العنصر اذا كان  محذوف
				foreach (var singleItem in entity.ProvisionsMedicalInsuranceEmployee)
				{
					if (singleItem.IsDeleted)
					{
						var CheckIfRecordExist = getFromProvisionEmployees.Where(x => x.IsDeleted == false && x.Id == singleItem.Id).FirstOrDefault();
						if (CheckIfRecordExist == null) return await Result<HrProvisionsMedicalInsuranceEditDto>.FailAsync($"--- لاتوجد مخصص للموظف بهذا الرقم: {entity.Id}---");
						CheckIfRecordExist.IsDeleted = true;
						CheckIfRecordExist.ModifiedBy = session.UserId;
						CheckIfRecordExist.ModifiedOn = DateTime.Now;
						hrRepositoryManager.HrProvisionsMedicalInsuranceEmployeeRepository.Update(CheckIfRecordExist);
						await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
					}
				}

				await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
				return await Result<HrProvisionsMedicalInsuranceEditDto>.SuccessAsync(localization.GetResource1("SaveSuccess"));

			}
			catch (Exception exc)
			{
				return await Result<HrProvisionsMedicalInsuranceEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message} && {localization.GetResource1("UpdateError")}");
			}

		}

		public async Task<IResult<string>> CreateProvisionEntry(HrProvisionsMedicalInsuranceEntryAddDto entity, CancellationToken cancellationToken = default)
		{
			try
			{
				//if (entity.Type <= 0) return await Result<string>.FailAsync($"يجب تحديد نوع العمليه");
				//int type = (int)entity.Type;
				await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

				int? JournalStatus = 0;
				var item = await hrRepositoryManager.HrProvisionsMedicalInsuranceRepository.GetOne(x => x.Id == entity.Id);
				if (item == null) return await Result<string>.FailAsync($"{localization.GetMessagesResource("NoItemFoundToEdit")}");
				JournalStatus = await accRepositoryManager.AccJournalMasterRepository.GetJournalMasterStatuse(Convert.ToInt32(entity.Id), 109);
				if (JournalStatus == 2)
					return await Result<string>.FailAsync($"{localization.GetResource1("AccrualCannotBeEditDueToMigration")} ");

				//var StatusId = await accRepositoryManager.AccFacilityRepository.GetOne(x => x.Posting, x => x.FacilityId == session.FacilityId);

				//  جلب كود الفترة المحاسبية
				var getPeriodId = await accRepositoryManager.AccPeriodsRepository.GetPreiodIDByDate(entity.JournalDate, session.FacilityId);
				var AccjournalMasterItem = new AccJournalMasterDto();
				var Description = $"{localization.GetResource1("JournalOfVacationAccrual")}";
				AccjournalMasterItem.JDateHijri = entity.JournalDate;
				AccjournalMasterItem.JDateGregorian = entity.JournalDate;
				AccjournalMasterItem.Amount = 0;
				AccjournalMasterItem.AmountWrite = "";
				AccjournalMasterItem.JDescription = Description;
				AccjournalMasterItem.JTime = "";
				AccjournalMasterItem.PaymentTypeId = 2;
				AccjournalMasterItem.PeriodId = getPeriodId;
				AccjournalMasterItem.StatusId = 1;
				AccjournalMasterItem.CreatedBy = (int?)session.UserId;
				AccjournalMasterItem.InsertUserId = (int?)session.UserId;
				AccjournalMasterItem.CreatedOn = DateTime.Now;
				AccjournalMasterItem.InsertDate = DateTime.Now;
				AccjournalMasterItem.FinYear = session.FinYear;
				AccjournalMasterItem.FacilityId = session.FacilityId;
				AccjournalMasterItem.DocTypeId = entity.DocTypeId;
				AccjournalMasterItem.ReferenceNo = entity.Id;
				AccjournalMasterItem.JBian = Description;
				AccjournalMasterItem.BankId = 0;
				AccjournalMasterItem.CcId = session.BranchId;
				AccjournalMasterItem.CurrencyId = 1;
				AccjournalMasterItem.ExchangeRate = 1;
				AccjournalMasterItem.ChequNo = "";
				var addJournalMaster = await accRepositoryManager.AccJournalMasterRepository.AddACCJournalMaster(AccjournalMasterItem);
				await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
				if (!addJournalMaster.Succeeded)
					return await Result<string>.FailAsync(addJournalMaster.Status.message);

				var JID = addJournalMaster.Data.JId;
				//تمت الاضافة للخاصية بواسطة فارس بتاريخ 13-04-2024 الهدف منها استخدام مركز التكلفة بالجانب الدائن
				string UseCCInCredit = "";
				UseCCInCredit = await sysConfigurationAppHelper.GetValue(57, session.FacilityId);


				var getProvisionsAcc = await hrRepositoryManager.HrProvisionsMedicalInsuranceEmployeeAccVwRepository.GetAccDetailsAsync(entity.Id);

				if (getProvisionsAcc.Count() > 0)
				{
					foreach (var SingleRecord in getProvisionsAcc.ToList())
					{
						//هل العملة الحالية تختلف عن العملة المستخدمة في النظام والتي تعتبر الرئيسية

						var AccountCurreny = await accRepositoryManager.AccAccountRepository.GetCuureny(0, session.FacilityId);

						var newDetail = new AccJournalDetaileDto();
						newDetail.JId = JID;
						newDetail.AccAccountId = SingleRecord.AccountID;
						newDetail.Credit = SingleRecord.Credit;
						newDetail.Debit = SingleRecord.Debit;
						newDetail.Description = SingleRecord.Description;
						if ((SingleRecord.Credit > 0 && UseCCInCredit == "1") || SingleRecord.Debit > 0)
						{
							newDetail.CcId = (SingleRecord.CCID);
							newDetail.Cc2Id = (SingleRecord.CCID2);
							newDetail.Cc3Id = (SingleRecord.CCID3);
							newDetail.Cc4Id = (SingleRecord.CCID4);
							newDetail.Cc5Id = (SingleRecord.CCID5);
						}
						else
						{
							newDetail.CcId = 0;
							newDetail.Cc2Id = 0;
							newDetail.Cc3Id = 0;
							newDetail.Cc4Id = 0;
							newDetail.Cc5Id = 0;
						}

						newDetail.ReferenceNo = SingleRecord.ReferenceNo;
						newDetail.ReferenceTypeId = SingleRecord.ReferenceTypeID;
						newDetail.ExchangeRate = 1;
						newDetail.CurrencyId = (int?)AccountCurreny;
						newDetail.JDateGregorian = entity.JournalDate;
						var AddedItem = await accRepositoryManager.AccJournalDetaileRepository.AddAccJournalDetail(newDetail);
						await accRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
					}

				}
				await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
				// still Use SendToWebHook

				return await Result<string>.SuccessAsync(localization.GetResource1("SaveSuccess"));

			}
			catch (Exception exp)
			{
				return await Result<string>.FailAsync($"EXP in CreateExpensesEntry at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");

			}
		}

		#endregion



		#region Provisions MedicalInsurance


		//public async Task<IResult<HrProvisionsMedicalInsuranceDto>> AddMedicalInsuranceProvision(HrProvisionsMedicalInsuranceDto entity, CancellationToken cancellationToken = default)
		//{
		//	if (entity == null) return await Result<HrProvisionsMedicalInsuranceDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

		//	try
		//	{
		//		if (entity.EmpCodes.Count() <= 0)
		//			return await Result<HrProvisionsMedicalInsuranceDto>.FailAsync($"لم يتم تحديد اي مخصص");

		//		int countMonth = (entity.YearlyOrMonthly == 1) ? 1 : 12;

		//		//long getMaxForCode = 0;
		//		//string Code = "PRO-";
		//		//await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

		//		//getMaxForCode = hrRepositoryManager.HrProvisionsMedicalInsuranceRepository.Entities.Where(x => x.FacilityId == session.FacilityId).Max(x => x.Id);
		//		//getMaxForCode++;
		//		//Code += getMaxForCode.ToString();
		//		long getMaxForCode = 0;
		//		string Code = "PRO-";
		//		await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

		//		// الحصول على أقصى Id مع التعامل مع الحالة عندما لا يوجد سجلات
		//		var maxId = hrRepositoryManager.HrProvisionsMedicalInsuranceRepository.Entities
		//			.Where(x => x.FacilityId == session.FacilityId)
		//			.Select(x => (long?)x.Id) // تحويل إلى nullable لتجنب الخطأ
		//			.Max();

		//		getMaxForCode = maxId.HasValue ? maxId.Value + 1 : 1;
		//		Code += getMaxForCode.ToString();

		//		var newHrProvisionEntity = new HrProvisionsMedicalInsurance
		//		{
		//			Code = Code,
		//			CreatedBy = session.UserId,
		//			CreatedOn = DateTime.Now,
		//			FacilityId = session.FacilityId,
		//			YearlyOrMonthly = entity.YearlyOrMonthly,
		//			Description = entity.Description,
		//			MonthId = entity.MonthId,
		//			FinYear = entity.FinYear,
		//			PDate = entity.PDate,
		//			No = 0

		//		};

		//		var AddedProvisionEntity = await hrRepositoryManager.HrProvisionsMedicalInsuranceRepository.AddAndReturn(newHrProvisionEntity);
		//		await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

		//		//  Add  Provision Employee

		//		var getFromEmployees = await hrRepositoryManager.HrInsuranceEmpRepository.GetAllVw(x => x.IsDeleted == false);

		//		var getAllAllowanceDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAllFromView(e =>
		//			e.IsDeleted == false &&
		//			e.Status == true &&
		//			e.FixedOrTemporary == 1);
		//		foreach (var empCode in entity.EmpCodes)
		//		{
		//			var empDate = getFromEmployees.Where(x => x.EmpCode == empCode).FirstOrDefault();
		//			if (empDate == null)
		//				return await Result<HrProvisionsMedicalInsuranceDto>.FailAsync((session.Language == 1) ? $"لا يوجد موظف بهذا الرقم : {empCode}" : $"There is No Employee With This Code {empCode}");

		//			var getApplyPloicyValue = await mainRepositoryManager.DbFunctionsRepository.ApplyPolicies(session.FacilityId, 4, empDate.Id);
		//			var CalculateAmount = (getApplyPloicyValue / countMonth / 30 * empDate.VacationDaysYear) ?? 0;
		//			if (CalculateAmount <= 0) continue;

		//			decimal salary = empDate.Salary ?? 0;
		//			decimal totalAllowance = getAllAllowanceDeduction
		//				.Where(a => a.EmpId == empDate.Id && a.TypeId == 1)
		//				.Sum(a => a.Amount) ?? 0;

		//			decimal totalDeduction = getAllAllowanceDeduction
		//				.Where(a => a.EmpId == empDate.Id && a.TypeId == 2)
		//				.Sum(a => a.Amount) ?? 0;


		//			var newHrProvisionsEmployeeEntity = new HrProvisionsMedicalInsuranceEmployee
		//			{
		//				EmpId = empDate.Id,
		//				CreatedBy = session.UserId,
		//				CreatedOn = DateTime.Now,
		//				IsDeleted = false,
		//				PId = AddedProvisionEntity.Id,
		//				LocationId = empDate.Location ?? 0,
		//				DeptId = empDate.DeptId,
		//				BranchId = empDate.BranchId ?? 0,
		//				FacilityId = session.FacilityId,
		//				CcId = empDate.CcId ?? 0,
		//				CurrentInsuranceAmount = totalAllowance,
		//				TotalPreInsuranceAmount = totalDeduction,
		//				PolicyId = (long?)getApplyPloicyValue,
		//				//BasicSalary = salary,
		//				//TotalSalary = salary + totalAllowance,
		//				ExcludedValue = salary + totalAllowance - totalDeduction,
		//				InsuranceAmount = CalculateAmount,
		//				//TotalAllowances = totalAllowance,
		//				//TotalDeductions = totalDeduction,
		//				//SalaryGroupId = empDate.SalaryGroupId,
		//				//BasicSalary = salary,
		//				//TotalSalary = salary + totalAllowance,
		//				//NetSalary = salary + totalAllowance - totalDeduction,
		//				//Amount = CalculateAmount,

		//			};
		//			var AddedHrProvisionsEmployeeEntity = await hrRepositoryManager.HrProvisionsMedicalInsuranceEmployeeRepository.AddAndReturn(newHrProvisionsEmployeeEntity);
		//			await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
		//		}

		//		await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
		//		return await Result<HrProvisionsMedicalInsuranceDto>.SuccessAsync(localization.GetResource1("AddSuccess"), 200);
		//	}
		//	catch (Exception exp)
		//	{
		//		return await Result<HrProvisionsMedicalInsuranceDto>.FailAsync($"EXP  at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
		//	}
		//}




		public async Task<IResult<HrProvisionsMedicalInsuranceDto>> AddMedicalInsuranceProvision(HrProvisionsMedicalInsuranceDto entity,CancellationToken cancellationToken = default)
		{
			if (entity == null)
				return await Result<HrProvisionsMedicalInsuranceDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

			try
			{
				if (entity.EmpId == null || entity.EmpId.Count() <= 0)
					return await Result<HrProvisionsMedicalInsuranceDto>.FailAsync("لم يتم تحديد أي مخصص");

				int countMonth = (entity.YearlyOrMonthly == 1) ? 1 : 12;

				long getMaxForCode = 0;
				string Code = "PRO-";
				await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

				// الحصول على أقصى ID لتوليد كود جديد
				var maxId = hrRepositoryManager.HrProvisionsMedicalInsuranceRepository.Entities
					.Where(x => x.FacilityId == session.FacilityId)
					.Select(x => (long?)x.Id)
					.Max();

				getMaxForCode = maxId.HasValue ? maxId.Value + 1 : 1;
				Code += getMaxForCode.ToString();

				// إضافة سجل المخصص الرئيسي
				var newHrProvisionEntity = new HrProvisionsMedicalInsurance
				{
					Code = Code,
					CreatedBy = session.UserId,
					CreatedOn = DateTime.Now,
					FacilityId = session.FacilityId,
					YearlyOrMonthly = entity.YearlyOrMonthly,
					Description = entity.Description,
					MonthId = entity.MonthId,
					FinYear = entity.FinYear,
					PDate = entity.PDate,
					No = 0
				};

				var AddedProvisionEntity = await hrRepositoryManager.HrProvisionsMedicalInsuranceRepository.AddAndReturn(newHrProvisionEntity);
				await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

				// جلب جميع الموظفين من HR_Insurance_Emp_VW مع الفلاتر
				var getFromEmployees = await hrRepositoryManager.HrInsuranceEmpRepository.GetAllVw(e =>
					e.IsDeleted == false &&
					e.TransTypeId == 1 &&
					e.InsuranceType == 1 &&
					e.FacilityID == session.FacilityId &&
					//(string.IsNullOrEmpty(entity.StatusList) || (e.StatusId.HasValue && statusList.Contains(e.StatusId.Value))) &&
					(string.IsNullOrEmpty(entity.EmpCode) || entity.EmpCode == e.EmpCode) &&
					(string.IsNullOrEmpty(entity.EmpName) ||
						(e.EmpName != null && e.EmpName.ToLower().Contains(entity.EmpName)) ||
						(e.Empname2 != null && e.Empname2.ToLower().Contains(entity.EmpName))) &&
					(entity.LocationId == null || entity.LocationId == e.Location) &&
					(entity.DeptId == null || entity.DeptId == e.DeptId) &&
					(entity.NationalityID == null || entity.NationalityID == e.NationalityID) &&
					(entity.JobCatagoriesID == null || entity.JobCatagoriesID == e.JobCatagoriesID) &&
					(entity.BranchId == null || entity.BranchId == e.BranchId) &&
					(entity.SalaryGroupID == null || entity.SalaryGroupID == e.SalaryGroupID)
				);

				foreach (var empCodes in entity.EmpId)
				{
					var empData = getFromEmployees.FirstOrDefault(x => x.EmpId == empCodes);
					if (empData == null)
						return await Result<HrProvisionsMedicalInsuranceDto>.FailAsync(
							(session.Language == 1)
								? $"لا يوجد موظف بهذا الرقم : {empCodes}"
								: $"There is no employee with this code {empCodes}");

					// جلب بيانات البوليصة لحساب عدد الأيام
					var insurancePolicy = await hrRepositoryManager.HrInsurancePolicyRepository.GetById((long)empData.PolicyId);
					if (insurancePolicy == null) continue;

					// تحويل النصوص إلى DateTime باستخدام الدالة الجاهزة StringToDate
					DateTime startDate = DateHelper.StringToDate(insurancePolicy.StartDate);
					DateTime endDate = DateHelper.StringToDate(insurancePolicy.EndDate);

					// حساب عدد الأيام بين بداية ونهاية الوثيقة
					var daysCoverage = (endDate - startDate).Days;

					// تحويل قيمة FinYear و MonthId إلى int قبل استخدامها
					int finYear = entity.FinYear.HasValue ? (int)entity.FinYear.Value : DateTime.Now.Year;
					int monthId = entity.MonthId.HasValue ? entity.MonthId.Value : DateTime.Now.Month;

					// حساب عدد الأيام في الشهر المحدد
					var daysInMonth = DateTime.DaysInMonth(finYear, monthId);

					// إذا كنت بحاجة لإرجاع التاريخ مرة أخرى كسلسلة نصية
					string formattedStart = DateHelper.DateToString(startDate);
					string formattedEnd = DateHelper.DateToString(endDate);


					// حساب مجموع المبالغ السابقة لهذا الموظف
					decimal totalPreInsuranceAmount = await hrRepositoryManager.HrProvisionsMedicalInsuranceEmployeeRepository.Entities
						.Where(x => x.IsDeleted==false && x.EmpId == empData.Id && x.PolicyId == empData.PolicyId)
						.SumAsync(x => (decimal?)x.CurrentInsuranceAmount) ?? 0;

					// حساب المبالغ المستثناة
					decimal excludedValue = await (
	                       from emp in hrRepositoryManager.HrInsuranceEmpRepository.Entities
	                       join empVw in hrRepositoryManager.HrInsuranceEmpVwRepository.Entities
		                   on emp.EmpId equals empVw.EmpId
	                       where emp.EmpId == empData.Id
		                       && !emp.IsDeleted
		                       && empVw.TransTypeId == 2
		                       && empVw.PolicyId == empData.PolicyId
		                       && empVw.ClassId == empData.ClassId
	                       select (decimal?)emp.Amount).SumAsync() ?? 0;


					// حساب مبلغ التأمين الأساسي
					decimal insuranceAmount = empData.Amount ?? 0;

					// حساب مبلغ التأمين الحالي بناءً على المعادلة في الاستعلام
					decimal calculatedInsurance = ((insuranceAmount / daysCoverage) * daysInMonth);
					decimal currentInsuranceAmount = (insuranceAmount - totalPreInsuranceAmount - excludedValue) <
													 (calculatedInsurance - excludedValue)
						? (insuranceAmount - totalPreInsuranceAmount - excludedValue)
						: (calculatedInsurance - excludedValue);

					if (currentInsuranceAmount <= 0) continue;

					// المبلغ المتبقي
					decimal remainingInsuranceAmount = insuranceAmount - totalPreInsuranceAmount;

					// إنشاء سجل جديد للموظف
					var newHrProvisionsEmployeeEntity = new HrProvisionsMedicalInsuranceEmployee
					{
						EmpId = empData.Id,
						CreatedBy = session.UserId,
						CreatedOn = DateTime.Now,
						IsDeleted = false,
						PId = AddedProvisionEntity.Id,
						LocationId = empData.Location ?? 0,
						DeptId = empData.DeptId,
						BranchId = empData.BranchId ?? 0,
						FacilityId = session.FacilityId,
						CcId = empData.CcId ?? 0,
						PolicyId = empData.PolicyId,
						InsuranceAmount = insuranceAmount,
						CurrentInsuranceAmount = currentInsuranceAmount,
						TotalPreInsuranceAmount = totalPreInsuranceAmount,
						//RemainingInsuranceAmount = remainingInsuranceAmount,
						ExcludedValue = excludedValue
					};

					await hrRepositoryManager.HrProvisionsMedicalInsuranceEmployeeRepository.AddAndReturn(newHrProvisionsEmployeeEntity);
					await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
				}

				await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
				return await Result<HrProvisionsMedicalInsuranceDto>.SuccessAsync(localization.GetResource1("AddSuccess"), 200);
			}
			catch (Exception exp)
			{
				return await Result<HrProvisionsMedicalInsuranceDto>.FailAsync(
					$"EXP at ({this.GetType()}), Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
			}
		}






		//public async Task<IResult<List<HrProvisionsMedicalInsuranceEmployeeResultDto>>> GetEmployeeProvisionMedicalInsuranceData(HrProvisionsMedicalInsuranceSearchOnAddFilter filter)
		//{
		//	try
		//	{
		//		List<HrProvisionsMedicalInsuranceEmployeeResultDto> resultList = new List<HrProvisionsMedicalInsuranceEmployeeResultDto>();
		//		int countMonth = (filter.YearlyOrMonthly == 1) ? 1 : 12;
		//		var StatusList = !string.IsNullOrEmpty(filter.StatusList)
		//			? filter.StatusList.Split(',').Select(int.Parse).ToList() : new List<int>();
		//		var items = await hrRepositoryManager.HrInsuranceEmpRepository.GetAllVw(e =>
		//			e.IsDeleted == false &&
		//			e.TransTypeId == 1 &&
		//			e.InsuranceType == 1 &&
		//			(string.IsNullOrEmpty(filter.StatusList) || (e.StatusId.HasValue && StatusList.Contains(e.StatusId.Value))) &&
		//			e.FacilityID == session.FacilityId &&
		//			(string.IsNullOrEmpty(filter.EmpCode) || filter.EmpCode == e.EmpCode) &&
		//			(string.IsNullOrEmpty(filter.EmpName) || (e.EmpName != null && e.EmpName.ToLower().Contains(filter.EmpName)) || (e.Empname2 != null && e.Empname2.ToLower().Contains(filter.EmpName))) &&
		//			(filter.LocationId == 0 || filter.LocationId == e.Location) &&
		//			(filter.DepartmentId == 0 || filter.DepartmentId == e.DeptId) &&
		//			(filter.NationalityId == 0 || filter.NationalityId == e.NationalityID) &&
		//			(filter.JobCategory == 0 || filter.JobCategory == e.JobCatagoriesID) &&
		//			(filter.BranchId == 0 || filter.BranchId == e.BranchId) &&
		//			//(filter.PolicyId == 0 || filter.PolicyId == e.PolicyId) &&
		//			(filter.SalaryGroupId == 0 || filter.SalaryGroupId == e.SalaryGroupID)
		//		);

		//		if (!items.Any())
		//			return await Result<List<HrProvisionsMedicalInsuranceEmployeeResultDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));

		//		var getAllProvisionsEmployeeAcc = await hrRepositoryManager.HrProvisionsMedicalInsuranceEmployeeAccVwRepository.GetAll(x => x.EmpId ,
		//			x => x.YearlyOrMonthly == filter.YearlyOrMonthly &&
		//			x.FinYear == filter.FinYear &&
		//			x.MonthId == filter.MonthId
		//			);
		//		var AllEmpList = getAllProvisionsEmployeeAcc.ToList();
		//		var res = items.AsQueryable();

		//		res = res.Where(x => !AllEmpList.Contains(x.Id));

		//		if (!res.Any())
		//			return await Result<List<HrProvisionsMedicalInsuranceEmployeeResultDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));


		//		var getAllAllowanceDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAllFromView(e =>
		//			e.IsDeleted == false &&
		//			e.Status == true &&
		//			e.FixedOrTemporary == 1);

		//		foreach (var item in res)
		//		{
		//			decimal salary = item.Amount ?? 0;
		//			decimal totalAllowance = getAllAllowanceDeduction
		//				.Where(a => a.EmpId == item.Id && a.TypeId == 1)
		//				.Sum(a => a.Amount) ?? 0;

		//			decimal totalDeduction = getAllAllowanceDeduction
		//				.Where(a => a.EmpId == item.Id && a.TypeId == 2)
		//				.Sum(a => a.Amount) ?? 0;
		//			var getApplyPloicyValue = await mainRepositoryManager.DbFunctionsRepository.ApplyPolicies(session.FacilityId, 4, item.Id);

		//			var Amount = (getApplyPloicyValue / countMonth / 30 * item.VacationDaysYear) ?? 0;
		//			resultList.Add(new HrProvisionsMedicalInsuranceEmployeeResultDto
		//			{
		//				Id = item.Id,
		//				//PolicyCode = item.p,
		//				EmpCode = item.EmpCode,
		//				EmpName = (session.Language == 1) ? item.EmpName : item.Empname2,
		//				//BasicSalary = Math.Round(salary, 2),
		//				//TotalSalary = Math.Round(salary + totalAllowance, 2),
		//				DepName = (session.Language == 1) ? item.DepName : item.DepName,
		//				LocationName = (session.Language == 1) ? item.LocationName : item.LocationName2,
		//				TotalPreInsuranceAmount = Math.Round(totalAllowance, 2),
		//				CurrentInsuranceAmount = Math.Round(totalDeduction, 2),
		//				ExcludedValue = Math.Round((salary + totalAllowance - totalDeduction), 2),
		//				//DOAppointment = item.Doappointment,
		//				InsuranceAmount = Math.Round(Amount, 2),
		//				//SalaryGroup = item.SalaryGroupId
		//			});
		//		}

		//		return await Result<List<HrProvisionsMedicalInsuranceEmployeeResultDto>>.SuccessAsync(resultList);
		//	}
		//	catch (Exception ex)
		//	{
		//		return await Result<List<HrProvisionsMedicalInsuranceEmployeeResultDto>>.FailAsync($"Error occurred while processing the OPeration: {ex.Message}");
		//	}
		//}


		public async Task<IResult<List<HrProvisionsMedicalInsuranceEmployeeResultDto>>> GetEmployeeProvisionMedicalInsuranceData(HrProvisionsMedicalInsuranceSearchOnAddFilter filter)
		{
			try
			{
				var resultList = new List<HrProvisionsMedicalInsuranceEmployeeResultDto>();

				// عدد الأشهر لتقسيم مبلغ التأمين
				int countMonth = (filter.YearlyOrMonthly == 1) ? 1 : 12;

				// معالجة قائمة الحالات
				var statusList = !string.IsNullOrEmpty(filter.StatusList)
					? filter.StatusList.Split(',').Select(int.Parse).ToList()
					: new List<int>();

				// جلب الموظفين من الفيو HR_Insurance_Emp_VW
				var items = await hrRepositoryManager.HrInsuranceEmpRepository.GetAllVw(e =>
					!e.IsDeleted &&
					e.TransTypeId == 1 &&
					e.InsuranceType == 1 &&
					e.FacilityID == session.FacilityId &&
					(string.IsNullOrEmpty(filter.StatusList) || (e.StatusId.HasValue && statusList.Contains(e.StatusId.Value))) &&
					(string.IsNullOrEmpty(filter.EmpCode) || filter.EmpCode == e.EmpCode) &&
					(string.IsNullOrEmpty(filter.EmpName) ||
						(e.EmpName != null && e.EmpName.ToLower().Contains(filter.EmpName)) ||
						(e.Empname2 != null && e.Empname2.ToLower().Contains(filter.EmpName))) &&
					(filter.LocationId == 0 || filter.LocationId == e.Location) &&
					(filter.DepartmentId == 0 || filter.DepartmentId == e.DeptId) &&
					(filter.NationalityId == 0 || filter.NationalityId == e.NationalityID) &&
					(filter.JobCategory == 0 || filter.JobCategory == e.JobCatagoriesID) &&
					(filter.BranchId == 0 || filter.BranchId == e.BranchId) &&
					(filter.SalaryGroupId == 0 || filter.SalaryGroupId == e.SalaryGroupID)
				);

				// استبعاد الموظفين المسجلين مسبقًا في HR_Provisions_MedicalInsurance_Employee_Acc_VW
				var existingEmployees = await hrRepositoryManager.HrProvisionsMedicalInsuranceEmployeeAccVwRepository.GetAll(x =>
					x.YearlyOrMonthly == filter.YearlyOrMonthly &&
					x.FinYear == filter.FinYear &&
					x.MonthId == filter.MonthId
				);

				var filteredItems = items.Where(e => !existingEmployees.Any(x => x.EmpId == e.EmpId)).ToList();

				if (!filteredItems.Any())
					return await Result<List<HrProvisionsMedicalInsuranceEmployeeResultDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));

				// جلب الاستبعادات
				var excludedValues = await hrRepositoryManager.HrInsuranceEmpRepository.GetAllVw(v => v.TransTypeId == 2 && !v.IsDeleted);

				// حساب القيم لكل موظف
				foreach (var item in filteredItems)
				{
					decimal amount = item.Amount ?? 0;

					// المبالغ المستثناة لكل موظف
					decimal excludedValue = excludedValues
						.Where(v => v.EmpId == item.EmpId && v.PolicyId == item.PolicyId)
						.Sum(v => v.Amount) ?? 0;

					// مجموع التأمينات السابقة
					var allInsurances = await hrRepositoryManager.HrProvisionsMedicalInsuranceEmployeeRepository.GetAll();

					decimal totalPreInsuranceAmount = (decimal)allInsurances
						.Where(e => e.IsDeleted == false && e.EmpId == item.EmpId && e.PolicyId == item.PolicyId)
						.Sum(e => e.CurrentInsuranceAmount);

					//decimal totalPreInsuranceAmount = await hrRepositoryManager.HrProvisionsMedicalInsuranceEmployeeRepository
					//	.Sum(e => e.CurrentInsuranceAmount,
					//		e => !e.IsDeleted && e.EmpId == item.EmpId && e.PolicyId == item.PolicyId);

					// حساب المبلغ المتبقي من التأمين
					decimal remainingInsurance = amount - totalPreInsuranceAmount;

					// حساب مبلغ التأمين الحالي
					decimal currentInsuranceAmount = remainingInsurance - excludedValue;
					// احسب القيمة قبل إضافة الكائن
					decimal remainingInsuranceAmount = (amount / countMonth) - excludedValue - totalPreInsuranceAmount;
					resultList.Add(new HrProvisionsMedicalInsuranceEmployeeResultDto
					{
						Id = item.EmpId,
						EmpCode = item.EmpCode,
						PolicyCode = item.PolicyCode,
						EmpName = (session.Language == 1) ? item.EmpName : item.Empname2,
						DepName = (session.Language == 1) ? item.DepName : item.DepName, // يمكن تعديلها لعرض اسم القسم
						LocationName = (session.Language == 1) ? item.LocationName : item.LocationName2,
						InsuranceAmount = Math.Round(amount / countMonth, 2),
						TotalPreInsuranceAmount = Math.Round(totalPreInsuranceAmount, 2),
						CurrentInsuranceAmount = Math.Round(currentInsuranceAmount, 2),
						ExcludedValue = Math.Round(excludedValue, 2),
						RemainingInsuranceAmount = Math.Round(remainingInsuranceAmount, 2)
					});
				}

				return await Result<List<HrProvisionsMedicalInsuranceEmployeeResultDto>>.SuccessAsync(resultList);
			}
			catch (Exception ex)
			{
				return await Result<List<HrProvisionsMedicalInsuranceEmployeeResultDto>>.FailAsync($"Error occurred while processing the operation: {ex.Message}");
			}
		}



		#endregion
	}

}