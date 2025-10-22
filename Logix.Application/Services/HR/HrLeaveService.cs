using AutoMapper;
using Castle.MicroKernel.Registration;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Wordprocessing;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.Hr;
using Logix.Domain.HR;
using Logix.Domain.Main;
using Logix.Domain.PM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR.Protocol;
using Org.BouncyCastle.Utilities.Zlib;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Emit;
using IResult = Logix.Application.Wrapper.IResult;

namespace Logix.Application.Services.HR
{
    public class HrLeaveService : GenericQueryService<HrLeave, HrLeaveDto, HrLeaveVw>, IHrLeaveService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly IWorkflowHelper workflowHelper;

        public HrLeaveService(IQueryRepository<HrLeave> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization, IWorkflowHelper workflowHelper) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
            this.workflowHelper = workflowHelper;
        }

        public Task<IResult<HrLeaveDto>> Add(HrLeaveDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<bool>> AddNewLeave(HrLeaveAddDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<bool>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                int empId = 0;
                long? appId = 0;
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<bool>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                if (checkEmpExist.StatusId == 2) return await Result<bool>.FailAsync(localization.GetMessagesResource("EmployeeAlreadyTerminated"));
                var checkFixingEmployeeSalaryExist = await hrRepositoryManager.HrFixingEmployeeSalaryRepository.GetOneVw(X => X.EmpId == checkEmpExist.Id && X.IsDeleted == false && X.Status == 1);
                if (checkFixingEmployeeSalaryExist != null) return await Result<bool>.FailAsync(localization.GetMessagesResource("ActiveSalaryFixationExists"));

                empId = Convert.ToInt32(checkEmpExist.Id);
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);


                entity.AppTypeId ??= 0;


                //  ارسال الى سير العمل

                var GetApp_ID = await workflowHelper.Send(checkEmpExist.Id, 263, entity.AppTypeId);
                appId = GetApp_ID;
                var hrLeaveEntity = _mapper.Map<HrLeave>(entity);
                ///////////////
                hrLeaveEntity.AppId = appId;
                hrLeaveEntity.EmpId = checkEmpExist.Id;
                hrLeaveEntity.LeaveDate = entity.LeaveDate;
                hrLeaveEntity.WorkDays = entity.WorkDays;
                hrLeaveEntity.WorkMonth = entity.WorkMonth;
                hrLeaveEntity.WorkYear = entity.WorkYear;
                hrLeaveEntity.LeaveType = entity.LeaveType;
                hrLeaveEntity.LeaveType2 = entity.LeaveType2;
                hrLeaveEntity.BasicSalary = entity.Salary;
                hrLeaveEntity.Housing = entity.Housing;
                hrLeaveEntity.Allowances = entity.Allowances;
                hrLeaveEntity.Deduction = entity.Deduction;
                hrLeaveEntity.TotalSalary = entity.NetSalary;
                hrLeaveEntity.BankId = "0";
                hrLeaveEntity.Iban = entity.Iban;
                hrLeaveEntity.LastSalaryDate = entity.LastSalaryDate;
                hrLeaveEntity.VacationBalance = entity.VacationBalance;
                hrLeaveEntity.VacationBalanceAmount = entity.VacationBalanceAmount;
                hrLeaveEntity.SalaryC = entity.SalaryC;
                hrLeaveEntity.HousingC = entity.HousingC;
                hrLeaveEntity.AllowanceC = entity.AllowanceC;
                hrLeaveEntity.OtherAllowance = entity.OtherAllowance;
                hrLeaveEntity.OtherAllowanceNote = entity.OtherAllowanceNote;
                hrLeaveEntity.TickDueTotal = entity.TickDueTotal;
                hrLeaveEntity.TickDueCnt = Convert.ToInt32(entity.TickDueCnt);
                hrLeaveEntity.TickDueAmount = entity.TickDueAmount;
                hrLeaveEntity.TotalAllowance = entity.TotalAllowance;
                hrLeaveEntity.DedHousing = entity.DedHousing;
                hrLeaveEntity.Loan = entity.Loan;
                hrLeaveEntity.Gosi = entity.Gosi;
                hrLeaveEntity.GosiNote = entity.GosiNote;
                hrLeaveEntity.DedOhad = entity.DedOhad;
                hrLeaveEntity.DedOhadNote = entity.DedOhadNote;
                hrLeaveEntity.Delay = entity.Delay;
                hrLeaveEntity.DelayCnt = entity.DelayCnt;
                hrLeaveEntity.Absence = entity.Absence;
                hrLeaveEntity.AbsenceCnt = entity.AbsenceCnt;
                hrLeaveEntity.Penalties = entity.Penalties;
                hrLeaveEntity.TotalDeduction = entity.TotalDeduction;
                hrLeaveEntity.Net = entity.Net;
                hrLeaveEntity.Note = entity.Note;
                hrLeaveEntity.Bounce = entity.Bounce;
                hrLeaveEntity.EndServiceBenefits = entity.EndServiceBenefits;
                hrLeaveEntity.EndServiceIndemnity = entity.EndServiceIndemnity;
                hrLeaveEntity.EndServiceIndemnityNote = entity.EndServiceIndemnityNote;
                hrLeaveEntity.MdInsurance = entity.MdInsurance;
                hrLeaveEntity.MdInsuranceNote = entity.MdInsuranceNote;
                hrLeaveEntity.OtherDeduction = entity.OtherDeduction;
                hrLeaveEntity.OtherDeductionNote = entity.OtherDeductionNote;
                hrLeaveEntity.CreatedBy = session.UserId;
                hrLeaveEntity.HaveBankLoan = entity.HaveBankLoan ?? false;
                hrLeaveEntity.CountDayWork = entity.CountDayWork;
                hrLeaveEntity.LastWorkingDay = entity.LastWorkingDay;
                hrLeaveEntity.ProvEndServesAmount = entity.ProvEndServesAmount;
                hrLeaveEntity.NetProvision = entity.NetProvision;
                hrLeaveEntity.HaveCustody = entity.HaveCustody ?? false;
                hrLeaveEntity.IsDeleted = false;
                hrLeaveEntity.CreatedOn = DateTime.Now;
                hrLeaveEntity.DepId = checkEmpExist.DeptId;
                hrLeaveEntity.BranchId = checkEmpExist.BranchId;
                hrLeaveEntity.LocationId = checkEmpExist.Location;
                ///////////////
                decimal Amount = entity.AllowanceC ?? 0m;
                decimal Housing = entity.HousingC ?? 0m;
                int Housing_allowance = 0;
                string Housing_allowance_Value = "0.00";

                decimal NewAmount = entity.allowanceList.Sum(x => x.NewAmount) ?? 0;


                if (NewAmount - Housing != Amount)
                {
                    return await Result<bool>.WarningAsync(localization.GetMessagesResource("DifferenceAmounts"));
                }

                var hrSetting = await hrRepositoryManager.HrSettingRepository.GetAll(x => x.FacilityId == session.FacilityId);
                foreach (var setting in hrSetting)
                {
                    Housing_allowance = setting.HousingAllowance ?? 0;
                }

                foreach (var row in entity.allowanceList)
                {
                    if (row.AdId == Housing_allowance)
                    {
                        Housing_allowance_Value = row.NewAmount.ToString();
                    }
                }

                if (Housing_allowance_Value != entity.HousingC.ToString())
                {
                    return await Result<bool>.WarningAsync(localization.GetMessagesResource("HousingDifferenceAmounts"));
                }

                var newEntity = await hrRepositoryManager.HrLeaveRepository.AddAndReturn(hrLeaveEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                try
                {
                    var hrLeaveAllowanceDeductionList = new List<HrLeaveAllowanceDeduction>();
                    foreach (var row in entity.allowanceList)
                    {
                        var hrAllowance = new HrLeaveAllowanceDeduction
                        {
                            LeaveId = newEntity.Id,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            Rate = row.Rate,
                            Amount = row.Amount,
                            NewAmount = row.NewAmount,
                            TypeId = 1,
                            FixedOrTemporary = 1,
                            AdId = row.AdId,
                            IsDeleted = false
                        };
                        hrLeaveAllowanceDeductionList.Add(hrAllowance);
                    }
                    var hrLeaveAllowanceDeductionAdd = await hrRepositoryManager.HrLeaveAllowanceDeductionRepository.AddList(hrLeaveAllowanceDeductionList);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    return await Result<bool>.WarningAsync(localization.GetMessagesResource("SavingAllowanceError") + " : " + ex.Message);
                }

                // تغيير حالة الموظف
                checkEmpExist.StatusId = 2;
                checkEmpExist.ModifiedBy = session.UserId;
                checkEmpExist.ModifiedOn = DateTime.Now;
                mainRepositoryManager.InvestEmployeeRepository.Update(checkEmpExist);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, newEntity.Id, 56);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                //  تعطيل المستخدمين للموظف
                if (entity.CancelAccount)
                {
                    var disableUser = await mainRepositoryManager.SysUserRepository.DisableUserByEmpID(empId);
                    if (disableUser == false)
                    {
                        return await Result<bool>.FailAsync(localization.GetMessagesResource("DisableUserErrorMessage") + $" {entity.EmpCode} ");
                    }
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<bool>.SuccessAsync(true, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {

                return await Result<bool>.FailAsync(localization.GetResource1("AddError"));
            }
        }



        public async Task<decimal> HR_End_Service_Due(string Curr_Date, long ID_Emp, int LeaveTypeId = 0)
        {

            var result = await hrRepositoryManager.HrLeaveRepository.HR_End_Service_Due(Curr_Date, ID_Emp, LeaveTypeId);
            return result;
        }

        public async Task<IResult<Dictionary<string, string>>> PayrollTransfer(List<int> leaveIds, CancellationToken cancellationToken = default)
        {
            try
            {
                decimal Completion_Rate = 0;
                string Trans_ConvertPrev_To_Payroll = "";
                string Trans_ConvertNow_To_Payroll = "";
                var objDT = new List<HrLeavePayrollTransferDto>();

                var keyValuePairs = new Dictionary<string, string>();
                foreach (var id in leaveIds)
                {
                    if (!string.IsNullOrEmpty(id.ToString()))
                    {
                        bool b = false;
                        long PayrollId = 0;

                        //   للفحص هل هناك انهاء خدمة بهذا الرقم 
                        var HrLeaveItem = await hrRepositoryManager.HrLeaveRepository.GetOneVw(x => x.Id == Convert.ToInt64(id));
                        var Bank = HrLeaveItem.BankId;
                        if (HrLeaveItem == null)
                        {
                            return await Result<Dictionary<string, string>>.FailAsync(localization.GetMessagesResource("InvalidLeaveId"));

                        }
                        string Emp_Name = HrLeaveItem.EmpName.ToString();
                        string Emp_Code = HrLeaveItem.EmpCode.ToString();
                        decimal Net = HrLeaveItem.Net ?? 0;
                        if (!string.IsNullOrEmpty(HrLeaveItem.PayrollId.ToString()))
                        {
                            PayrollId = HrLeaveItem.PayrollId ?? 0;
                            var HrPayroll = await hrRepositoryManager.HrPayrollRepository.GetAll(x => x.MsId == PayrollId);
                            if (HrPayroll.Count() > 0)
                            {
                                Trans_ConvertPrev_To_Payroll += id + ",";
                                b = true;
                            }
                        }
                        if (!b)
                        {
                            int BankID = 0;
                            if (!string.IsNullOrEmpty(Bank))
                            {
                                BankID = Convert.ToInt32(Bank);
                            }
                            var objDR = new HrLeavePayrollTransferDto
                            {
                                EmpCode = HrLeaveItem.EmpCode.ToString(),
                                EmpName = HrLeaveItem.EmpName.ToString(),
                                BankId = BankID,
                                Net = HrLeaveItem.Net ?? 0,
                                SalaryC = HrLeaveItem.SalaryC ?? 0,
                                TotalAllowance = HrLeaveItem.TotalAllowance ?? 0,
                                TotalDeduction = HrLeaveItem.TotalDeduction ?? 0,
                                CountDayWork = HrLeaveItem.CountDayWork.ToString(),
                                Absence = HrLeaveItem.Absence ?? 0,
                                Delay = HrLeaveItem.Delay,
                                AccountNo = HrLeaveItem.AccountNo.ToString(),
                                Loan = HrLeaveItem.Loan,
                                BasicSalary = HrLeaveItem.BasicSalary,
                                Penalties = HrLeaveItem.Penalties,
                                HousingC = HrLeaveItem.HousingC,
                                VacationBalanceAmount = HrLeaveItem.VacationBalanceAmount,
                                TickDueTotal = HrLeaveItem.TickDueTotal,
                                EndServiceBenefits = HrLeaveItem.EndServiceBenefits.ToString(),
                                EndServiceIndemnity = HrLeaveItem.EndServiceIndemnity.ToString(),
                                AllowanceC = HrLeaveItem.AllowanceC,
                                Gosi = HrLeaveItem.Gosi,
                                DedHousing = HrLeaveItem.DedHousing,
                                Housing = HrLeaveItem.Housing,
                                DedOhad = HrLeaveItem.DedOhad.ToString(),
                                MdInsurance = HrLeaveItem.MdInsurance.ToString(),
                                OtherDeduction = HrLeaveItem.OtherDeduction.ToString(),
                                ReferenceNo = id.ToString()
                            };
                            objDT.Add(objDR);
                            Trans_ConvertNow_To_Payroll += id + ",";
                        }
                    }
                    else
                    {
                        return await Result<Dictionary<string, string>>.FailAsync(localization.GetMessagesResource("InvalidLeaveId"));
                    }
                }
                if (objDT.Count() == 0)
                {
                    keyValuePairs = new Dictionary<string, string>
                                {
                                    {"new" , Trans_ConvertNow_To_Payroll },
                                    {"exist" , Trans_ConvertPrev_To_Payroll },
                                };

                    return await Result<Dictionary<string, string>>.SuccessAsync(keyValuePairs);
                }

                var hrSetting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == session.FacilityId);

                if (hrSetting == null)
                {
                    return await Result<Dictionary<string, string>>.FailAsync(localization.GetMessagesResource("HRSettingsNotFound"));
                }

                if (checkHrSetting(hrSetting).Length > 0)
                {
                    return await Result<Dictionary<string, string>>.FailAsync(checkHrSetting(hrSetting));
                }

                var FinancelYear = Bahsas.HDateNow3(session).Substring(0, 4);

                var Month_Code = Bahsas.HDateNow3(session).Substring(5, 2);
                var monthName = await mainRepositoryManager.InvestMonthRepository.GetOne(x => x.MonthName, x => x.MonthCode == Month_Code);
                var objM = new HrPayroll
                {
                    CreatedBy = session.UserId,
                    MsDate = Bahsas.HDateNow3(session),
                    MsMonth = Month_Code,
                    MsMothTxt = monthName,
                    MsTitle = localization.GetHrResource("EndofServicePayroll"),
                    FinancelYear = Convert.ToInt32(FinancelYear),
                    State = 1,
                    PayrollTypeId = 6, //مسير نهاية خدمة فقط
                    FacilityId = Convert.ToInt32(session.FacilityId),
                    BranchId = session.BranchId,
                    AppId = 0
                };
                //'التشييك على السالب

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                //إضافة المسير
                var payrollResult = await hrRepositoryManager.HrPayrollRepository.AddPayroll(objM, cancellationToken);

                foreach (var objDR in objDT)
                {
                    long EmpId = 0;
                    var emp = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == objDR.EmpCode && x.IsDeleted == false && x.Isdel == false);
                    if (emp == null)
                        return await Result<Dictionary<string, string>>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                    else
                        EmpId = emp.Id;

                    decimal allowance = 0;
                    decimal SalaryC = 0;
                    if (!string.IsNullOrEmpty(Math.Round(objDR.SalaryC, 2).ToString()))
                        SalaryC = Math.Round(objDR.SalaryC, 2);
                    if (!string.IsNullOrEmpty(objDR.TotalAllowance.ToString()))
                        allowance = decimal.Parse(objDR.TotalAllowance.ToString()) - SalaryC - decimal.Parse(objDR.EndServiceIndemnity.ToString());
                    else
                        allowance = 0;

                    decimal Deduction = 0;
                    if (!string.IsNullOrEmpty(objDR.TotalDeduction.ToString()))
                    {
                        Deduction = decimal.Parse(objDR.TotalDeduction.ToString());
                    }
                    int Count_Day_Work = 0;
                    Count_Day_Work = int.Parse(objDR.CountDayWork);
                    decimal Absence = 0;
                    if (!string.IsNullOrEmpty(objDR.Absence.ToString()))
                    {
                        Absence = objDR.Absence ?? 0;
                        Deduction -= decimal.Parse(objDR.Absence.ToString());
                    }
                    else
                        Absence = 0;

                    decimal Delay = 0;
                    if (!string.IsNullOrEmpty(objDR.Delay.ToString()))
                    {
                        Delay = objDR.Delay ?? 0;
                        Deduction -= decimal.Parse(objDR.Delay.ToString());
                    }
                    else
                        Delay = 0;
                    decimal Loan = 0;
                    if (!string.IsNullOrEmpty(objDR.Loan.ToString()))
                    {
                        Loan = objDR.Loan ?? 0;
                        Deduction -= decimal.Parse(objDR.Loan.ToString());
                    }
                    else
                        Loan = 0;


                    decimal SalaryTotal = 0;
                    if (!string.IsNullOrEmpty(Math.Round(objDR.SalaryC, 2).ToString()))
                    {
                        SalaryTotal = Math.Round(objDR.SalaryC, 2);
                    }
                    else
                    {
                        SalaryTotal = 0;
                    }

                    if (!string.IsNullOrEmpty(objDR.EndServiceIndemnity.ToString()))
                    {
                        SalaryTotal += decimal.Parse(objDR.EndServiceIndemnity.ToString());
                    }

                    decimal penalties = 0;

                    if (!string.IsNullOrEmpty(objDR.Penalties.ToString()))
                    {
                        penalties = objDR.Penalties ?? 0;
                        Deduction -= decimal.Parse(objDR.Penalties.ToString());
                    }
                    else
                    {
                        penalties = 0;
                    }
                    var objD = new HRPayrollDStoredProcedureDto
                    {
                        Emp_ID = EmpId,
                        MS_ID = payrollResult.MsId,
                        Allowance = allowance,
                        Absence = Absence,
                        Delay = Delay,
                        BankId = objDR.BankId,
                        Loan = Loan,
                        Count_Day_Work = Count_Day_Work,
                        CreatedBy = session.UserId,
                        Emp_Account_No = objDR.AccountNo,
                        Salary = SalaryTotal,
                        Salary_Orignal = objDR.BasicSalary,
                        Commission = 0,
                        OverTime = 0,
                        Mandate = 0,
                        H_OverTime = 0,
                        Penalties = penalties,
                        Deduction = Deduction,
                        Net = objDR.Net,
                        CMDTYPE = 1,
                    };
                    //Dim PDID As Long = .InsertHR_Payroll_D(objD)

                    var PDID = await mainRepositoryManager.StoredProceduresRepository.HR_Payroll_D_SP(objD);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    if (PDID <= 0)
                        return await Result<Dictionary<string, string>>.FailAsync($"{localization.GetResource1("AddError")}");
                    ////  اضافة لعدم ارجاع البروسيجر لرقم المسير التفصيلي 
                    var getPDID = hrRepositoryManager.HrPayrollDRepository.Entities.Max(x => x.MsdId);
                    PDID = getPDID;

                    //'-- بدل السكن
                    var housingAllowance = await AddPayrollAllowanceDeductionAsync(
                        hrSetting.HousingAllowance,
                        objDR.HousingC,
                        true,
                        payrollResult.MsId,
                        PDID,
                        cancellationToken,
                        objDR.Housing
                    );
                    if (housingAllowance != null)
                    {
                        allowance -= decimal.Parse(Math.Round(objDR.HousingC ?? 0, 2).ToString() ?? "0");
                    }

                    // بدل اجازة مستحقة
                    var vacationAllowance = await AddPayrollAllowanceDeductionAsync(
                        hrSetting.VacationDueAllowance,
                        objDR.VacationBalanceAmount,
                        true,
                        payrollResult.MsId,
                        PDID,
                        cancellationToken
                    );
                    if (vacationAllowance != null)
                    {
                        allowance -= decimal.Parse(Math.Round(objDR.VacationBalanceAmount ?? 0, 2).ToString() ?? "0");
                    }

                    // بدل تذاكر مستحقة
                    var ticketAllowance = await AddPayrollAllowanceDeductionAsync(
                        hrSetting.TicketAllowance,
                        objDR.TickDueTotal,
                        true,
                        payrollResult.MsId,
                        PDID,
                        cancellationToken
                    );
                    if (ticketAllowance != null)
                        allowance -= decimal.Parse(Math.Round(objDR.TickDueTotal ?? 0, 2).ToString() ?? "0");


                    //'-- بدل مكافأة نهاية خدمة
                    decimal Leave_Benefits = 0;
                    if (!string.IsNullOrEmpty(objDR.EndServiceBenefits.ToString()))
                    {
                        Leave_Benefits += decimal.Parse(objDR.EndServiceBenefits.ToString());
                    }
                    if (!string.IsNullOrEmpty(objDR.EndServiceIndemnity.ToString()))
                    {
                        Leave_Benefits += decimal.Parse(objDR.EndServiceIndemnity.ToString());
                    }

                    if (Leave_Benefits > 0)
                    {
                        var leaveBenefitsAllowance = await AddPayrollAllowanceDeductionAsync(
                                hrSetting.LeaveBenefitsAllowance,
                                Leave_Benefits,
                                true,
                                payrollResult.MsId,
                                PDID,
                                cancellationToken
                            );
                        allowance -= decimal.Parse(Leave_Benefits.ToString() ?? "0");
                    }

                    //'--البدلات الأخرى
                    if (!string.IsNullOrEmpty(Math.Round(objDR.AllowanceC ?? 0, 2).ToString()))
                    {
                        var otherAllowanceDeduction = await AddPayrollAllowanceDeductionAsync(
                            hrSetting.BadalatAllowance,
                            objDR.AllowanceC,
                            true,
                            payrollResult.MsId,
                            PDID,
                            cancellationToken
                        );
                    }

                    //'--حسم التأمينات الإجتماعية
                    if (!string.IsNullOrEmpty(objDR.Gosi.ToString()))
                    {

                        var gosiAllowanceDeduction = await AddPayrollAllowanceDeductionAsync(
                            hrSetting.GosiDeduction,
                            objDR.Gosi,
                            false,
                            payrollResult.MsId,
                            PDID,
                            cancellationToken
                        );
                    }

                    //'--حسم بدل السكن المقدم
                    if (!string.IsNullOrEmpty(objDR.DedHousing.ToString()))
                    {

                        var dedHousingAllowanceDeduction = await AddPayrollAllowanceDeductionAsync(
                            hrSetting.HousingDeduction,
                            objDR.DedHousing,
                            false,
                            payrollResult.MsId,
                            PDID,
                            cancellationToken,
                            objDR.Housing
                        );
                    }

                    decimal amountOtherDeduction = 0;
                    if (!string.IsNullOrEmpty(objDR.DedOhad.ToString()))
                    {
                        amountOtherDeduction += decimal.Parse(objDR.DedOhad);
                    }

                    if (!string.IsNullOrEmpty(objDR.MdInsurance.ToString()))
                    {
                        amountOtherDeduction += decimal.Parse(objDR.MdInsurance);
                    }

                    if (!string.IsNullOrEmpty(objDR.OtherDeduction.ToString()))
                    {
                        amountOtherDeduction += decimal.Parse(objDR.OtherDeduction);
                    }

                    //'--حسم العهودات الأخرى
                    if (amountOtherDeduction > 0)
                    {

                        var payrollAllowanceDeduction = await AddPayrollAllowanceDeductionAsync(
                            hrSetting.OtherDeduction,
                            amountOtherDeduction,
                            false,
                            payrollResult.MsId,
                            PDID,
                            cancellationToken
                        );
                    }
                    var hrLeave = await hrRepositoryManager.HrLeaveRepository.GetOne(x => x.Id == Convert.ToInt64(objDR.ReferenceNo));
                    if (hrLeave != null)
                    {
                        hrLeave.PayrollId = payrollResult.MsId;
                        hrRepositoryManager.HrLeaveRepository.Update(hrLeave);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }
                // إضافة ملاحظة للمسير
                var hrPayrollNote = new HrPayrollNote
                {
                    MsId = payrollResult.MsId,
                    Note = "",
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    StateId = 1 // 1: Pending, 2: Approved, 3: Rejected, 4: Paid
                };

                await hrRepositoryManager.HrPayrollRepository.ChangeStatusPayrollTrans(hrPayrollNote, cancellationToken);
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<Dictionary<string, string>>.SuccessAsync(keyValuePairs);

            }
            catch (Exception ex)
            {
                return await Result<Dictionary<string, string>>.FailAsync(ex.InnerException.Message.ToString());
            }
        }
        private async Task<HrPayrollAllowanceDeduction?> AddPayrollAllowanceDeductionAsync(
            int? adId,
            decimal? amount,
            bool isCredit,
            long msId,
            long msdId,
            CancellationToken cancellationToken,
            decimal? adValueOriginal = 0)
        {
            decimal roundedAmount = Math.Round(amount ?? 0, 2);
            if (roundedAmount == 0)
                return null;

            var payrollAllowanceDeduction = new HrPayrollAllowanceDeduction
            {
                AdId = adId,
                MsId = msId,
                AdValue = roundedAmount,
                Debit = isCredit ? 0 : roundedAmount,
                Credit = isCredit ? roundedAmount : 0,
                AdValueOrignal = adValueOriginal == 0 ? roundedAmount : adValueOriginal,
                CreatedBy = session.UserId,
                MsdId = msdId,
                FixedOrTemporary = 1
            };

            var payrollAllowanceDeductionResult = await hrRepositoryManager.HrPayrollAllowanceDeductionRepository.AddAndReturn(payrollAllowanceDeduction);
            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

            return payrollAllowanceDeductionResult;
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrLeaveRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrLeaveDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrLeaveRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrLeaveDto>.SuccessAsync(_mapper.Map<HrLeaveDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrLeaveDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrLeaveRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrLeaveDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrLeaveRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrLeaveDto>.SuccessAsync(_mapper.Map<HrLeaveDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrLeaveDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
        private string checkHrSetting(HrSetting hrSetting)
        {
            string MSGALL = "";

            if (hrSetting.HousingAllowance == 0)
            {
                MSGALL += localization.GetMessagesResource("HousingAllowanceNotLinkedHR");
            }

            if (hrSetting.HousingDeduction == 0)
            {
                MSGALL += localization.GetMessagesResource("HousingAllowanceDeductionNotLinkedHR");
            }

            if (hrSetting.GosiDeduction == 0)
            {
                MSGALL += localization.GetMessagesResource("SocialInsuranceDeductionstAllowanceNotLinkedHR");
            }

            if (hrSetting.BadalatAllowance == 0)
            {
                MSGALL += localization.GetMessagesResource("OtherAllowanceNotLinkedHR");
            }

            if (hrSetting.OtherDeduction == 0)
            {
                MSGALL += localization.GetMessagesResource("OtherDeductionsNotLinkedHR");
            }

            if (hrSetting.TicketAllowance == 0)
            {
                MSGALL += localization.GetMessagesResource("TravelTicketAllowanceNotLinkedHR");
            }

            if (hrSetting.VacationDueAllowance == 0)
            {
                MSGALL += localization.GetMessagesResource("AccruedVacationSalaryAllowanceNotLinkedHR");
            }

            if (hrSetting.LeaveBenefitsAllowance == 0)
            {
                MSGALL += localization.GetMessagesResource("EndServiceGratuityAllowanceNotLinkedHR");
            }
            return MSGALL;
        }
        public async Task<IResult<HrLeaveEditDto>> PayrollTransferFromEdit(HrLeaveEditDto entity, CancellationToken cancellationToken = default)
        {

            if (entity == null) return await Result<HrLeaveEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));
            if (string.IsNullOrEmpty(entity.EmpCode)) return await Result<HrLeaveEditDto>.FailAsync(localization.GetMessagesResource("EmployeeNumberIsRequired"));
            try
            {

                //   للفحص هل هناك انهاء خدمة بهذا الرقم 
                var hrLeaveItem = await hrRepositoryManager.HrLeaveRepository.GetOneVw(x => x.Id == entity.Id);
                var Bank = hrLeaveItem.BankId;
                if (hrLeaveItem == null)
                {
                    return await Result<HrLeaveEditDto>.FailAsync(localization.GetMessagesResource("InvalidLeaveId"));

                }

                if (!string.IsNullOrEmpty(hrLeaveItem.PayrollId.ToString()))
                {
                    var HrPayroll = await hrRepositoryManager.HrPayrollRepository.GetAll(x => x.MsId == hrLeaveItem.PayrollId);
                    if (HrPayroll.Count() > 0)
                    {
                        return await Result<HrLeaveEditDto>.WarningAsync(localization.GetMessagesResource("EndOfServicePayrollAlreadyProcessed") + hrLeaveItem.PayrollId);
                    }
                }
                int BankID = 0;
                if (!string.IsNullOrEmpty(Bank))
                {
                    BankID = Convert.ToInt32(Bank);
                }

                long EmpId = 0;
                var emp = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (emp == null)
                    return await Result<HrLeaveEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                else
                    EmpId = emp.Id;

                var hrSetting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == session.FacilityId);

                if (hrSetting == null)
                {
                    return await Result<HrLeaveEditDto>.FailAsync(localization.GetMessagesResource("HRSettingsNotFound"));
                }

                if (checkHrSetting(hrSetting).Length > 0)
                {
                    return await Result<HrLeaveEditDto>.FailAsync(checkHrSetting(hrSetting));
                }


                //'ارسال الى سير العمل
                var subject = $"{localization.GetMessagesResource("EndOfServiceliquidationemployee")}  {hrLeaveItem.EmpName} {localization.GetHrResource("Amount")}  : {entity.Net}";
                var appId = await workflowHelper.Send(session.EmpId, 900, entity.AppTypeId, 0, subject);


                var FinancelYear = entity.LeaveDate.Substring(0, 4);

                var Month_Code = entity.LeaveDate.Substring(5, 2);
                var monthName = await mainRepositoryManager.InvestMonthRepository.GetOne(x => x.MonthName, x => x.MonthCode == Month_Code);
                var objM = new HrPayroll
                {
                    CreatedBy = session.UserId,
                    MsDate = entity.LeaveDate,
                    MsMonth = Month_Code,
                    MsMothTxt = monthName,
                    MsTitle = localization.GetHrResource("EndofServicePayroll"),
                    FinancelYear = Convert.ToInt32(FinancelYear),
                    State = 1,
                    PayrollTypeId = 6, //مسير نهاية خدمة فقط
                    FacilityId = Convert.ToInt32(session.FacilityId),
                    BranchId = session.BranchId,
                    AppId = appId
                };
                //'التشييك على السالب

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                //إضافة المسير
                var payrollResult = await hrRepositoryManager.HrPayrollRepository.AddPayroll(objM, cancellationToken);

                var allowances = await hrRepositoryManager.HrLeaveAllowanceDeductionRepository.GetAllVw(x => x.IsDeleted == false && x.LeaveId == hrLeaveItem.Id && x.EmpId == EmpId);

                decimal allowance = 0;
                decimal SalaryC = 0;
                if (!string.IsNullOrEmpty(entity.SalaryC.ToString()))
                    SalaryC = Math.Round(entity.SalaryC ?? 0, 2);
                if (!string.IsNullOrEmpty(entity.TotalAllowance.ToString()))
                    allowance = decimal.Parse(entity.TotalAllowance.ToString()) - SalaryC - decimal.Parse(entity.EndServiceIndemnity.ToString());
                else
                    allowance = 0;

                decimal Deduction = 0;
                if (!string.IsNullOrEmpty(entity.TotalDeduction.ToString()))
                {
                    Deduction = decimal.Parse(entity.TotalDeduction.ToString());
                }
                int Count_Day_Work = 0;
                Count_Day_Work = entity.CountDayWork ?? 0;
                decimal Absence = 0;
                if (!string.IsNullOrEmpty(entity.Absence.ToString()))
                {
                    Absence = entity.Absence ?? 0;
                    Deduction -= decimal.Parse(entity.Absence.ToString());
                }
                else
                    Absence = 0;

                decimal Delay = 0;
                if (!string.IsNullOrEmpty(entity.Delay.ToString()))
                {
                    Delay = entity.Delay ?? 0;
                    Deduction -= decimal.Parse(entity.Delay.ToString());
                }
                else
                    Delay = 0;
                decimal Loan = 0;
                if (!string.IsNullOrEmpty(entity.Loan.ToString()))
                {
                    Loan = entity.Loan ?? 0;
                    Deduction -= decimal.Parse(entity.Loan.ToString());
                }
                else
                    Loan = 0;


                decimal SalaryTotal = 0;
                if (!string.IsNullOrEmpty(entity.SalaryC.ToString()))
                {
                    SalaryTotal = entity.SalaryC ?? 0;
                }
                else
                {
                    SalaryTotal = 0;
                }

                if (!string.IsNullOrEmpty(entity.EndServiceIndemnity.ToString()))
                {
                    SalaryTotal += decimal.Parse(entity.EndServiceIndemnity.ToString());
                }

                decimal penalties = 0;

                if (!string.IsNullOrEmpty(entity.Penalties.ToString()))
                {
                    penalties = entity.Penalties ?? 0;
                    Deduction -= decimal.Parse(entity.Penalties.ToString());
                }
                else
                {
                    penalties = 0;
                }
                var objD = new HRPayrollDStoredProcedureDto
                {
                    Emp_ID = EmpId,
                    MS_ID = payrollResult.MsId,
                    Allowance = allowance,
                    Absence = Absence,
                    Delay = Delay,
                    BankId = BankID,
                    Loan = Loan,
                    Count_Day_Work = Count_Day_Work,
                    CreatedBy = session.UserId,
                    Emp_Account_No = hrLeaveItem.AccountNo,
                    Salary = SalaryTotal,
                    Salary_Orignal = entity.Salary,
                    Commission = 0,
                    OverTime = 0,
                    Mandate = 0,
                    H_OverTime = 0,
                    Penalties = penalties,
                    Deduction = Deduction,
                    Net = entity.Net,
                    CMDTYPE = 1,
                };
                //Dim PDID As Long = .InsertHR_Payroll_D(objD)

                var PDID = await mainRepositoryManager.StoredProceduresRepository.HR_Payroll_D_SP(objD);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                if (PDID <= 0)
                    return await Result<HrLeaveEditDto>.FailAsync($"{localization.GetMessagesResource("AddPayrollDetailsError")}");
                ////  اضافة لعدم ارجاع البروسيجر لرقم المسير التفصيلي 
                var getPDID = hrRepositoryManager.HrPayrollDRepository.Entities.Max(x => x.MsdId);
                PDID = getPDID;

                // بدل اجازة مستحقة
                var vacationAllowance = await AddPayrollAllowanceDeductionAsync(
                    hrSetting.VacationDueAllowance,
                    entity.VacationBalanceAmount,
                    true,
                    payrollResult.MsId,
                    PDID,
                    cancellationToken
                );
                if (vacationAllowance != null)
                {
                    allowance -= entity.VacationBalanceAmount ?? 0;
                }

                // بدل تذاكر مستحقة
                var ticketAllowance = await AddPayrollAllowanceDeductionAsync(
                    hrSetting.TicketAllowance,
                    entity.TickDueTotal,
                    true,
                    payrollResult.MsId,
                    PDID,
                    cancellationToken
                );
                if (ticketAllowance != null)
                    allowance -= entity.TickDueTotal ?? 0;


                //'-- بدل مكافأة نهاية خدمة
                decimal Leave_Benefits = 0;
                if (!string.IsNullOrEmpty(entity.EndServiceBenefits.ToString()))
                {
                    Leave_Benefits += decimal.Parse(entity.EndServiceBenefits.ToString());
                }

                if (Leave_Benefits > 0)
                {
                    var leaveBenefitsAllowance = await AddPayrollAllowanceDeductionAsync(
                            hrSetting.LeaveBenefitsAllowance,
                            Leave_Benefits,
                            true,
                            payrollResult.MsId,
                            PDID,
                            cancellationToken
                        );
                    allowance -= decimal.Parse(Leave_Benefits.ToString() ?? "0");
                }
                foreach (var item in allowances)
                {
                    //'--البدلات الأخرى
                    var otherAllowanceDeduction = await AddPayrollAllowanceDeductionAsync(
                        item.AdId,
                        item.NewAmount,
                        true,
                        payrollResult.MsId,
                        PDID,
                        cancellationToken,
                        item.Amount
                    );

                    allowance -= item.NewAmount ?? 0;
                }

                //'--حسم التأمينات الإجتماعية
                if (!string.IsNullOrEmpty(entity.Gosi.ToString()))
                {

                    var gosiAllowanceDeduction = await AddPayrollAllowanceDeductionAsync(
                        hrSetting.GosiDeduction,
                        entity.Gosi,
                        false,
                        payrollResult.MsId,
                        PDID,
                        cancellationToken
                    );
                }

                //'--حسم بدل السكن المقدم
                if (!string.IsNullOrEmpty(entity.DedHousing.ToString()))
                {

                    var dedHousingAllowanceDeduction = await AddPayrollAllowanceDeductionAsync(
                        hrSetting.HousingDeduction,
                        entity.DedHousing,
                        false,
                        payrollResult.MsId,
                        PDID,
                        cancellationToken,
                        entity.Housing
                    );
                }

                decimal amountOtherDeduction = 0;
                if (!string.IsNullOrEmpty(entity.DedOhad.ToString()))
                {
                    amountOtherDeduction += entity.DedOhad ?? 0;
                }

                if (!string.IsNullOrEmpty(entity.MdInsurance.ToString()))
                {
                    amountOtherDeduction += entity.MdInsurance ?? 0;
                }

                if (!string.IsNullOrEmpty(entity.OtherDeduction.ToString()))
                {
                    amountOtherDeduction += entity.OtherDeduction ?? 0;
                }

                //'--حسم العهودات الأخرى
                if (amountOtherDeduction > 0)
                {

                    var payrollAllowanceDeduction = await AddPayrollAllowanceDeductionAsync(
                        hrSetting.OtherDeduction,
                        amountOtherDeduction,
                        false,
                        payrollResult.MsId,
                        PDID,
                        cancellationToken
                    );
                }

                // إضافة ملاحظة للمسير
                var hrPayrollNote = new HrPayrollNote
                {
                    MsId = payrollResult.MsId,
                    Note = "",
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    StateId = 1 // 1: Pending, 2: Approved, 3: Rejected, 4: Paid
                };

                await hrRepositoryManager.HrPayrollRepository.ChangeStatusPayrollTrans(hrPayrollNote, cancellationToken);

                var hrLeave = await hrRepositoryManager.HrLeaveRepository.GetOne(x => x.Id == entity.Id);
                if (hrLeave != null)
                {
                    hrLeave.PayrollId = payrollResult.MsId;
                    hrLeave.ModifiedBy = session.UserId;
                    hrLeave.ModifiedOn = DateTime.Now;
                    hrRepositoryManager.HrLeaveRepository.Update(hrLeave);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                //save files
                var newFile = new SysFile
                {
                    CreatedBy = session.UserId,
                    ModifiedBy = session.UserId,
                    PrimaryKey = payrollResult.MsId,
                    FileName = $"{localization.GetMessagesResource("EndOfServiceSettlementForm")}",
                    TableId = 37,
                    FileType = 0,
                    FileUrl = "/Apps/HR/Crystalreport/Report_Viewer.aspx?Rep_ID=9&ID=" + entity.Id.ToString(),
                    FileDate = entity.LeaveDate,
                    IsDeleted = false
                };

                await mainRepositoryManager.SysFileRepository.Add(newFile);
                await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<HrLeaveEditDto>.SuccessAsync(_mapper.Map<HrLeaveEditDto>(hrLeaveItem), localization.GetMessagesResource("PayrollExtracted"));

            }
            catch (Exception ex)
            {
                return await Result<HrLeaveEditDto>.FailAsync(ex.InnerException.Message.ToString());
            }
        }
        public async Task<IResult<HrLeaveEditDto>> Update(HrLeaveEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                return await Result<HrLeaveEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            if (string.IsNullOrEmpty(entity.EmpCode))
                return await Result<HrLeaveEditDto>.FailAsync(localization.GetMessagesResource("EmployeeNumberIsRequired"));

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);


                var item = await hrRepositoryManager.HrLeaveRepository.GetById(entity.Id);
                if (item == null)
                    return await Result<HrLeaveEditDto>.FailAsync(localization.GetMessagesResource("RecordNotFound"));

                var employee = await mainRepositoryManager.InvestEmployeeRepository
                    .GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted ==false && x.Isdel == false);

                if (employee == null)
                    return await Result<HrLeaveEditDto>.FailAsync(localization.GetMessagesResource("EmployeeNotFound"));
                entity.BranchId = item.BranchId;
                _mapper.Map(entity, item);
                item.EmpId = employee.Id;
                item.IsDeleted = false;
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.TotalSalary = entity.NetSalary;
                item.BasicSalary = entity.Salary;

                decimal Amount = entity.AllowanceC ?? 0;
                decimal Housing = entity.HousingC ?? 0;
                decimal NewAmount = entity.allowanceList.Sum(x => x.NewAmount) ?? 0;
                decimal Housing_allowance_Value = 0;


                if ((NewAmount - Housing) != Amount)
                    return await Result<HrLeaveEditDto>.FailAsync(localization.GetMessagesResource("DifferenceAmounts"));

                var hrSetting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == session.FacilityId);
                
                foreach (var row in entity.allowanceList)
                    if (row.AdId == hrSetting.HousingAllowance)
                        Housing_allowance_Value = row.NewAmount ?? 0;

                if (Housing_allowance_Value != entity.HousingC)
                    return await Result<HrLeaveEditDto>.FailAsync(localization.GetMessagesResource("HousingDifferenceAmounts"));

                hrRepositoryManager.HrLeaveRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                if (entity.fileDtos != null && entity.fileDtos.Any())
                {
                    await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, item.Id, 56);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                //  update allowance
                if (entity.allowanceList != null && entity.allowanceList.Any())
                {
                    foreach (var row in entity.allowanceList)
                    {
                        var existingAllowance = await hrRepositoryManager.HrLeaveAllowanceDeductionRepository.GetOne(x => x.Id == row.Id && x.IsDeleted == false && x.EmpId == row.EmpId && x.AdId == row.AdId&& x.TypeId == row.TypeId);

                        //_mapper.Map<HrLeaveAllowanceDeduction>(existingAllowance,row);
                        existingAllowance.FixedOrTemporary = 1;
                        existingAllowance.TypeId = 1;
                        hrRepositoryManager.HrLeaveAllowanceDeductionRepository.Update(existingAllowance);
                    }
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrLeaveEditDto>.SuccessAsync(
                    _mapper.Map<HrLeaveEditDto>(item),
                    localization.GetResource1("SaveSuccess")
                );
            }
            catch (Exception ex)
            {
                await hrRepositoryManager.UnitOfWork.RollbackTransactionAsync(cancellationToken);
                Console.WriteLine($"EXP in Update at ({this.GetType()}), Message: {ex.Message}, Inner: {ex.InnerException?.Message}");
                return await Result<HrLeaveEditDto>.FailAsync(
                    $"{localization.GetMessagesResource("SaveError")} {ex.Message}"
                );
            }
        }

        public async Task<IResult<object>> GetEmployeeLeaveData(HrLeaveGetDataDto obj, CancellationToken cancellationToken = default)
        {
            try
            {
                var BranchesList = session.Branches.Split(',');
                var investEmployees = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == obj.EmpCode && e.IsDeleted == false && e.Isdel == false);
                if (investEmployees == null)
                {
                    return await Result<object>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                }
                if (investEmployees.StatusId == 2)
                {
                    return await Result<object>.WarningAsync(localization.GetMessagesResource("EmployeeAlreadyTerminated"));
                }
                var GetHREmpAllData = await hrRepositoryManager.HrEmployeeRepository.GetOneVw(x => x.Id == investEmployees.Id && x.IsDeleted == false && x.Isdel == false && BranchesList.Contains(x.BranchId.ToString()));
                if (GetHREmpAllData == null)
                {
                    return await Result<object>.FailAsync(localization.GetHrResource("UserNotHasPermissionToEmp"));
                }
                obj.EmpId = investEmployees.Id;
                var result = await mainRepositoryManager.StoredProceduresRepository.GetEmployeeLeaveData(obj);

                decimal Total_Allowance;
                decimal Total_Deduction;

                //decimal Net;
                Total_Allowance =
                   (result.Salary_C ?? 0) +
                    (result.Housing_C ?? 0) +
                    (result.Allowance_C ?? 0) +
                    (result.VacationBalanceAmount ?? 0) +
                    (result.Tick_Due_Total ?? 0) +
                    (result.Other_Allowance ?? 0) +
                    (result.Bounce ?? 0) +
                    (result.End_Service_Benefits ?? 0) +
                    (result.End_Service_Indemnity ?? 0);

                Total_Deduction =
                    result.Ded_Housing ?? 0 + result.Loan ?? 0 +
                    (result.Absence ?? 0) +
                    (result.Delay ?? 0) +
                    (result.DedOhad ?? 0) +
                    (result.Gosi ?? 0) +
                    (result.Penalties ?? 0) +
                    (result.MD_Insurance ?? 0) +
                    (result.Other_Deduction ?? 0);

                var TotalAllowance = Math.Round(Total_Allowance, 2);
                var TotalDeduction = Math.Round(Total_Deduction, 2);
                var Net = Total_Allowance - Total_Deduction;
                bool ChkHaveCustody = false;

                var GetHR_Ohad_Emp = await hrRepositoryManager.HrOhadDetailsVwRepository.GetByInvestEmployeesAsync(investEmployees.Id, 1);
                if (GetHR_Ohad_Emp != null && GetHR_Ohad_Emp.Count() > 0)
                {
                    ChkHaveCustody = true;
                }
                long DaysCnt;

                if (!long.TryParse(result.Count_Day_Work.ToString(), out DaysCnt))
                {
                    DaysCnt = 0;
                }

                var getAllowance = await hrRepositoryManager.HrAllowanceVwRepository.GetAll(x => x.EmpId == investEmployees.Id && x.IsDeleted == false && x.Status == true && x.FixedOrTemporary == 1 && x.TypeId == 1);
                decimal AmountLong;

                var listAllowance = new List<HrAllowanceVwDto>();
                foreach (var RowAll in getAllowance)
                {
                    if (!decimal.TryParse(RowAll.Amount.ToString(), out AmountLong))
                    {
                        AmountLong = 0;
                    }
                    var objDR = new HrAllowanceVwDto
                    {
                        Id = RowAll.Id,
                        TypeId = RowAll.TypeId,
                        AdId = RowAll.AdId,
                        Rate = RowAll.Rate,
                        Amount = RowAll.Amount,
                        Name = RowAll.Name,
                        NewAmount = Math.Round((AmountLong / 30) * DaysCnt, 2),
                        IsDeleted = false,
                        IsNew = false
                    };
                    listAllowance.Add(objDR);
                }
                var hrSetting = await hrRepositoryManager.HrSettingRepository.GetOne(x => x.FacilityId == session.FacilityId);
                string HousingId = "0";
                decimal HousingC = 0;
                decimal AllowanceC = 0;
                decimal totalAllowance = 0;
                HousingId = hrSetting.HousingAllowance.ToString();
                bool b = false;
                bool thereIsAllowance = false;
                foreach (var row in listAllowance)
                {
                    if (row.AdId.ToString() == HousingId)
                    {
                        HousingC = row.NewAmount ?? 0;
                        b = true;
                    }
                    totalAllowance += row.NewAmount ?? 0;
                }
                if (listAllowance.Count() > 0)
                {

                    decimal Housing = 0;

                    if (!decimal.TryParse(HousingC.ToString(), out Housing))
                    {
                        Housing = 0;
                    }
                    AllowanceC = totalAllowance - Housing;
                    thereIsAllowance = true;
                }

                var res = new
                {
                    EmpName = result.EmpName,
                    EmpId = result.EmpId,
                    DOAppointment = result.Doappointment,
                    Nationality = result.NationalityName,
                    JobCategory = result.CatName,
                    Location = result.LocationName,
                    Department = result.DepName,
                    Salary = result.Salary,
                    Housing = result.Housing,
                    Allowances = result.Allowance,
                    Deduction = (result.Deduction + result.Gosi),
                    TotalSalary = (result.Salary + result.Housing + result.Allowance),
                    NetSalary = (result.Salary + result.Housing + result.Allowance - result.Deduction - result.Gosi),
                    Iban = result.Iban,
                    BankName = result.BankName,
                    SalaryC = result.Salary_C ?? 0,
                    AllowanceC = thereIsAllowance ? AllowanceC : result.Allowance_C ?? 0,
                    HousingC = b ? HousingC : result.Housing_C ?? 0,
                    GosiC = result.Gosi_C,
                    Loan = result.Loan,
                    Delay = result.Delay,
                    DelayCnt = result.Delay_Cnt,
                    Absence = result.Absence,
                    AbsenceCnt = result.Absence_Cnt,
                    Penalties = result.Penalties,
                    DedHousing = result.Ded_Housing,
                    DedOhad = result.DedOhad,
                    LastSalaryDate = result.Last_Salary_Date,
                    VacationBalance = result.VacationBalance,
                    VacationBalanceAmount = result.VacationBalanceAmount ?? 0,
                    OtherAllowance = result.Other_Allowance ?? 0,
                    TickDueTotal = result.Tick_Due_Total ?? 0,
                    TickDueCnt = result.Tick_Due_Cnt,
                    TickDueAmount = result.Tick_Due_Amount,
                    WorkYear = result.WorkYear,
                    WorkMonth = result.WorkMonth,
                    WorkDays = result.WorkDays,
                    MDInsurance = result.MD_Insurance,
                    OtherDeduction = result.Other_Deduction,
                    Bounce = result.Bounce,
                    EndServiceBenefits = result.End_Service_Benefits,
                    EndServiceIndemnity = result.End_Service_Indemnity,
                    ChkHaveBankloan = result.HaveBankLoan,
                    CountDayWork = result.Count_Day_Work,
                    HDDayCost4Clearnce = result.DayCost4Clearnce,
                    HDDepId = result.DeptId,
                    HDlocationId = result.Location,
                    HDBranchId = result.BranchId,
                    Branch = result.BraName,
                    ProvEndServesAmount = result.ProvEndServes_Amount,
                    Netprovision = (decimal.Parse(result.End_Service_Benefits?.ToString() ?? "0") - decimal.Parse(result.ProvEndServes_Amount?.ToString() ?? "0")),
                    TotalAllowance = TotalAllowance,
                    TotalDeduction = TotalDeduction,
                    Net = Net,
                    ChkHaveCustody = ChkHaveCustody,
                    //OtherAllowanceNote = result.othe,
                    AllowanceList = listAllowance,
                };
                return await Result<object>.SuccessAsync(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IResult<List<HrLeaveFilterDto>>> Search(HrLeaveFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                var BranchesList = session.Branches.Split(',');
                List<HrLeaveFilterDto> resultList = new List<HrLeaveFilterDto>();
                var items = await hrRepositoryManager.HrLeaveRepository.GetAllVw(e => e.IsDeleted == false && e.FacilityId == session.FacilityId);
                if (items != null)
                {
                    if (items.Count() > 0)
                    {
                        var res = items.AsQueryable();
                        if (!string.IsNullOrEmpty(filter.EmpCode))
                        {
                            res = res.Where(c => c.EmpCode != null && c.EmpCode == filter.EmpCode);
                        }
                        if (!string.IsNullOrEmpty(filter.EmpName))
                        {
                            res = res.Where(c => (c.EmpName != null && c.EmpName.Contains(filter.EmpName)));
                        }
                        if (filter.LeaveType != null && filter.LeaveType != 0)
                        {
                            res = res.Where(c => c.LeaveType != null && c.LeaveType == filter.LeaveType);
                        }
                        if (!string.IsNullOrEmpty(filter.FromDate) && !string.IsNullOrEmpty(filter.ToDate))
                        {
                            res = res.Where(r => r.LeaveDate != null &&
                            (DateHelper.StringToDate(r.LeaveDate) >= DateHelper.StringToDate(filter.FromDate)) &&
                           (DateHelper.StringToDate(r.LeaveDate) <= DateHelper.StringToDate(filter.ToDate))
                           );
                        }
                        if (filter.DeptId != null && filter.DeptId != 0)
                        {
                            res = res.Where(c => c.DeptId != null && c.DeptId == filter.DeptId);
                        }
                        if (filter.BranchId != null && filter.BranchId != 0)
                        {
                            res = res.Where(c => c.BranchId != null && c.BranchId == filter.BranchId);
                        }
                        else
                        {
                            res = res.Where(c => BranchesList.Contains(c.BranchId.ToString()));
                        }
                        if (filter.Location != null && filter.Location != 0)
                        {
                            res = res.Where(c => c.Location != null && c.Location == filter.Location);
                        }


                        var lang = session.Language;
                        foreach (var item in res)
                        {
                            var newRecord = new HrLeaveFilterDto
                            {
                                Id = item.Id,
                                EmpCode = item.EmpCode,
                                EmpName = lang == 1 ? item.EmpName : item.EmpName2,
                                BranchName = lang == 1 ? item.BraName : item.BraName2,
                                DepName = lang == 1 ? item.DepName : item.DepName2,
                                LocationName = lang == 1 ? item.LocationName : item.LocationName2,
                                LeaveDate = item.LeaveDate,
                                TypeName = lang == 1 ? item.TypeName : item.TypeName2,
                                WorkYear = item.WorkYear,
                                LastWorkingDay = item.LastWorkingDay,
                                PayrollCode = item.PayrollCode
                            };
                            resultList.Add(newRecord);
                        }
                        if (resultList.Count() > 0)
                            return await Result<List<HrLeaveFilterDto>>.SuccessAsync(resultList, "");
                        return await Result<List<HrLeaveFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));
                    }
                    return await Result<List<HrLeaveFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));
                }
                return await Result<List<HrLeaveFilterDto>>.FailAsync("There are errors");
            }
            catch (Exception ex)
            {
                return await Result<List<HrLeaveFilterDto>>.FailAsync(ex.Message);
            }
        }
    }
}
