using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrOpeningBalanceService : GenericQueryService<HrOpeningBalance, HrOpeningBalanceDto, HrOpeningBalanceVw>, IHrOpeningBalanceService
    {
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;

        public HrOpeningBalanceService(IQueryRepository<HrOpeningBalance> queryRepository,
            IMainRepositoryManager mainRepositoryManager,
            ILocalizationService localization,
            IMapper mapper,
            IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrOpeningBalanceDto>> Add(HrOpeningBalanceDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrOpeningBalanceDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
            try
            {
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrOpeningBalanceDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                var newItem = new HrOpeningBalance
                {
                    IsDeleted = false,
                    EmpId = checkEmpExist.Id,
                    TypeId = entity.TypeId,
                    ObValue = entity.ObValue,
                    StartDate = entity.StartDate,
                };
                var newEntity = await hrRepositoryManager.HrOpeningBalanceRepository.AddAndReturn(newItem);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                return await Result<HrOpeningBalanceDto>.SuccessAsync(_mapper.Map<HrOpeningBalanceDto>(newEntity), localization.GetResource1("CreateSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<HrOpeningBalanceDto>.FailAsync($"{localization.GetResource1("ErrorOccurredDuring")} EXP in {this.GetType()}, Meesage: {exc.Message} ");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrOpeningBalanceRepository.GetById(Id);
                if (item == null) return Result<HrOpeningBalanceDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.IsDeleted = true;
                hrRepositoryManager.HrOpeningBalanceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrOpeningBalanceDto>.SuccessAsync(_mapper.Map<HrOpeningBalanceDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrOpeningBalanceDto>.FailAsync($"{localization.GetResource1("DeleteFail")}  {exp.Message}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrOpeningBalanceRepository.GetById(Id);
                if (item == null) return Result<HrOpeningBalanceDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.IsDeleted = true;
                hrRepositoryManager.HrOpeningBalanceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrOpeningBalanceDto>.SuccessAsync(_mapper.Map<HrOpeningBalanceDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrOpeningBalanceDto>.FailAsync($"{localization.GetResource1("DeleteFail")}  {exp.Message}");
            }
        }

        public async Task<IResult<HrOpeningBalanceEditDto>> Update(HrOpeningBalanceEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrOpeningBalanceEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");
            try
            {
                // check if Emp Is Exist
                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist == null) return await Result<HrOpeningBalanceEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                var item = await hrRepositoryManager.HrOpeningBalanceRepository.GetById(entity.Id);
                if (item == null) return await Result<HrOpeningBalanceEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");

                item.EmpId = checkEmpExist.Id;
                item.ObValue = entity.ObValue;
                item.TypeId = entity.TypeId;
                item.StartDate = entity.StartDate;
                hrRepositoryManager.HrOpeningBalanceRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                return await Result<HrOpeningBalanceEditDto>.SuccessAsync(_mapper.Map<HrOpeningBalanceEditDto>(item), localization.GetResource1("UpdateSuccess"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrOpeningBalanceEditDto>.FailAsync($"{localization.GetResource1("UpdateError")}");
            }
        }

        private async Task<IResult<List<OtherBalanceDto>>> HR_Other_balances_SP(string currDate, long? empId, int TypeID, int CMDTYPE)
        {
            try
            {
                double? DaysCount = 0;
                decimal? OpBalance = 0;
                decimal? BalanceInYear = 0;
                decimal? BalanceDays = 0;
                string? StartDate;
                decimal? BalanceUsed = 0;
                decimal Curbalance = 0;
                List<OtherBalanceDto> result = new();
                DateTime currDateTime = DateHelper.StringToDate(currDate);
                if (CMDTYPE == 1)
                {
                    var getFromHrEmployee = await hrRepositoryManager.HrEmployeeRepository.GetOne(x => x.IsDeleted == false && x.Isdel == false && x.Id == empId);
                    if (getFromHrEmployee == null)
                    {
                        return await Result<List<OtherBalanceDto>>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                    }
                    // التذاكر
                    if (TypeID == 1)
                    {
                        decimal? TicketCount = 0;
                        var getFromBalance = await hrRepositoryManager.HrOpeningBalanceRepository.GetAll(x => x.IsDeleted == false && x.TypeId == 1 && x.EmpId == empId);
                        if (getFromBalance.Any())
                        {
                            var orderedGetFromBalance = getFromBalance.OrderByDescending(x => DateHelper.StringToDate(x.StartDate)).FirstOrDefault();
                            OpBalance = orderedGetFromBalance.ObValue ?? 0m;
                            StartDate = orderedGetFromBalance.StartDate;
                        }
                        else
                        {
                            OpBalance = 0m;
                            StartDate = getFromHrEmployee.Doappointment;
                        }
                        if (string.IsNullOrEmpty(getFromHrEmployee.TicketNoDependent))
                        {
                            getFromHrEmployee.TicketNoDependent = "0";
                        }
                        if (getFromHrEmployee.TicketEntitlement == null || getFromHrEmployee.TicketEntitlement <= 0)
                        {
                            getFromHrEmployee.TicketEntitlement = 1;
                        }
                        BalanceInYear = Convert.ToInt32(getFromHrEmployee.TicketNoDependent) / getFromHrEmployee.TicketEntitlement;

                        DateTime startDateTime = DateHelper.StringToDate(StartDate);
                        // get from Ticket
                        var getFromTicket = await hrRepositoryManager.HrTicketRepository.GetAll(x => x.IsDeleted == false && x.EmpId == empId && x.TicketDate != null);
                        if (getFromTicket.Any())
                        {
                            var filteredData = getFromTicket.Where(x => DateHelper.StringToDate(x.TicketDate) >= startDateTime && DateHelper.StringToDate(x.TicketDate) <= currDateTime);
                            if (filteredData.Any())
                            {
                                TicketCount = filteredData.Sum(x => x.TicketCount);
                                BalanceUsed = TicketCount;
                            }
                        }

                        DaysCount = (currDateTime - startDateTime).TotalDays;
                        if (DaysCount > 0)
                        {
                            BalanceDays = (BalanceInYear / 364) * (decimal)DaysCount;
                        }

                        Curbalance = (OpBalance ?? 0) + (BalanceDays ?? 0) - (TicketCount ?? 0);
                        OtherBalanceDto newItem = new OtherBalanceDto();
                        newItem.EmpId = getFromHrEmployee.Id;
                        newItem.EmpCode = getFromHrEmployee.EmpId;
                        newItem.EmpName = getFromHrEmployee.EmpName;
                        newItem.EmpName2 = getFromHrEmployee.EmpName2;
                        newItem.CurBalance = Math.Round(Curbalance, 3);
                        newItem.StartDate = StartDate;
                        newItem.BalanceDays = Math.Round(BalanceDays ?? 0, 3);
                        newItem.BalanceInYear = BalanceInYear;
                        newItem.BalanceUsed = BalanceUsed;
                        newItem.CurrDate = currDate;
                        newItem.OpBalance = OpBalance;
                        result.Add(newItem);
                    }
                    //الخروج والعودة
                    else if (TypeID == 2)
                    {
                        var getFromBalance = await hrRepositoryManager.HrOpeningBalanceRepository.GetAll(x => x.IsDeleted == false && x.TypeId == 2 && x.EmpId == empId);
                        if (getFromBalance.Any())
                        {
                            var orderedGetFromBalance = getFromBalance.OrderByDescending(x => DateHelper.StringToDate(x.StartDate)).FirstOrDefault();
                            OpBalance = orderedGetFromBalance.ObValue ?? 0m;
                            StartDate = orderedGetFromBalance.StartDate;
                        }
                        else
                        {
                            OpBalance = 0m;
                            StartDate = getFromHrEmployee.Doappointment;
                        }
                        DateTime startDateTime = DateHelper.StringToDate(StartDate);
                        // مجموع االتذاكر من بداية اعتبار التذاكر حتى تاريخ اليوم
                        var getFromVisa = await hrRepositoryManager.HrVisaRepository.GetAll(x => x.IsDeleted == false && x.EmpId == empId && x.VisaDate != null && x.IsBillable == 0);
                        decimal VisaCount = 0;
                        if (getFromVisa.Any())
                        {
                            var filteredData = getFromVisa.Where(x => DateHelper.StringToDate(x.VisaDate) >= startDateTime && DateHelper.StringToDate(x.VisaDate) <= currDateTime);
                            VisaCount = filteredData.Count();
                        }

                        DaysCount = (currDateTime - startDateTime).TotalDays;
                        if (DaysCount > 0)
                        {
                            BalanceDays = (BalanceInYear / 364) * (decimal)DaysCount;
                        }

                        Curbalance = (OpBalance ?? 0) + (BalanceDays ?? 0) - (VisaCount);

                        OtherBalanceDto newItem = new OtherBalanceDto();
                        newItem.EmpId = getFromHrEmployee.Id;
                        newItem.EmpCode = getFromHrEmployee.EmpId;
                        newItem.EmpName = getFromHrEmployee.EmpName;
                        newItem.EmpName2 = getFromHrEmployee.EmpName2;
                        newItem.CurBalance = Math.Round(Curbalance, 3);
                        newItem.StartDate = StartDate;
                        newItem.BalanceDays = Math.Round(BalanceDays ?? 0, 3);
                        newItem.BalanceInYear = BalanceInYear;
                        newItem.BalanceUsed = VisaCount;
                        newItem.CurrDate = currDate;
                        newItem.OpBalance = OpBalance;

                        result.Add(newItem);
                    }

                    // بدل السكن
                    else if (TypeID == 3) // بدل السكن
                    {
                        decimal? HousingAmountPaid = 0;
                        // الرصيد الافتتاحي
                        var openingBalance = await hrRepositoryManager.HrOpeningBalanceRepository
                            .GetAll(x => x.IsDeleted == false && x.TypeId == 3 && x.EmpId == empId);

                        if (openingBalance.Any())
                        {
                            var orderedOpeningBalance = openingBalance
                                .OrderByDescending(x => DateHelper.StringToDate(x.StartDate)).FirstOrDefault();

                            OpBalance = orderedOpeningBalance?.ObValue ?? 0m;
                            StartDate = orderedOpeningBalance?.StartDate;
                        }
                        else
                        {
                            OpBalance = 0m;
                            StartDate = getFromHrEmployee.Doappointment;
                        }

                        // البدل السكن في السنة
                        var allowanceDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository
                            .GetAll(x => x.EmpId == empId && x.TypeId == 1 && x.IsDeleted == false
                                && x.AdId == 1 && x.FixedOrTemporary == 1);
                        if (allowanceDeduction.Any())
                        {
                            BalanceInYear = allowanceDeduction.Sum(x => x.Amount * 12 ?? 0m);
                        }
                        DateTime startDateTime = DateHelper.StringToDate(StartDate);
                        // مجموع بدل السكن المدفوع منذ بداية الفترة حتى الآن
                        var payrollAllowance = await hrRepositoryManager.HrPayrollAllowanceVwRepository
                            .GetAll(x => x.IsDeleted == false && x.EmpId == empId && x.AdId == 1);
                        if (payrollAllowance.Any())
                        {
                            var filteredData = payrollAllowance.Where(x => DateHelper.StringToDate($"{x.FinancelYear}/{x.MsMonth}/01") >= startDateTime && DateHelper.StringToDate($"{x.FinancelYear}/{x.MsMonth}/01") <= currDateTime);
                            HousingAmountPaid = filteredData.Sum(x => x.Amount);
                        }

                        BalanceUsed = HousingAmountPaid;

                        // نسبة الأيام من تاريخ البداية حتى اليوم
                        DaysCount = (currDateTime - startDateTime).TotalDays;
                        if (DaysCount > 0)
                        {
                            BalanceDays = BalanceInYear / 364 * (decimal)DaysCount;
                        }

                        Curbalance = (OpBalance ?? 0) + (BalanceDays ?? 0) - (HousingAmountPaid ?? 0);
                        OtherBalanceDto newItem = new OtherBalanceDto();
                        newItem.EmpId = getFromHrEmployee.Id;
                        newItem.EmpCode = getFromHrEmployee.EmpId;
                        newItem.EmpName = getFromHrEmployee.EmpName;
                        newItem.EmpName2 = getFromHrEmployee.EmpName2;
                        newItem.CurBalance = Math.Round(Curbalance, 3);
                        newItem.StartDate = StartDate;
                        newItem.BalanceDays = Math.Round(BalanceDays ?? 0, 3);
                        newItem.BalanceUsed = BalanceUsed;
                        newItem.BalanceInYear = BalanceInYear;
                        newItem.CurrDate = currDate;
                        newItem.OpBalance = OpBalance;

                        result.Add(newItem);
                    }
                    else
                    {
                        if (getFromHrEmployee != null)
                        {
                            OtherBalanceDto newItem = new OtherBalanceDto();
                            newItem.EmpId = getFromHrEmployee.Id;
                            newItem.EmpCode = getFromHrEmployee.EmpId;
                            newItem.EmpName = getFromHrEmployee.EmpName;
                            newItem.EmpName2 = getFromHrEmployee.EmpName2;
                            newItem.CurBalance = 0;
                            newItem.StartDate = "";
                            newItem.BalanceDays = 0;
                            newItem.BalanceUsed = 0;
                            newItem.BalanceInYear = 0;
                            newItem.CurrDate = currDate;
                            newItem.OpBalance = 0;

                            result.Add(newItem);
                        }
                    }
                }

                //   يستخدم في شاشة الأرصدة الحالية لجميع الموظفين
                if (CMDTYPE == 2)
                {
                    var getFromHrEmployeeForCmdtype2 = await hrRepositoryManager.HrEmployeeRepository.GetAllVw(x => x.IsDeleted == false && x.StatusId == 1 && x.Isdel == false && (empId == 0 || x.Id == empId));
                    if (getFromHrEmployeeForCmdtype2.Count() < 1 && empId != 0)
                    {
                        return await Result<List<OtherBalanceDto>>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                    }
                    if (getFromHrEmployeeForCmdtype2.Count() < 1 && empId == 0)
                    {
                        return await Result<List<OtherBalanceDto>>.FailAsync(("ليس هناك أي موظف "));
                    }
                    // التذاكر
                    if (TypeID == 1)
                    {
                        decimal? TicketCount = 0;
                        var getFromBalances = await hrRepositoryManager.HrOpeningBalanceRepository.GetAll(x => x.IsDeleted == false && x.TypeId == 1);
                        var getFromTickets = await hrRepositoryManager.HrTicketRepository.GetAll(x => x.IsDeleted == false && x.TicketDate != null);

                        foreach (var newTicketitem in getFromHrEmployeeForCmdtype2)
                        {
                            try
                            {
                                BalanceInYear = 0; BalanceDays = 0; TicketCount = 0;
                                var getFromBalance = getFromBalances.Where(x => x.EmpId == newTicketitem.Id).ToList();
                                if (getFromBalance.Any())
                                {
                                    var orderedGetFromBalance = getFromBalance.OrderByDescending(x => DateHelper.StringToDate(x.StartDate)).FirstOrDefault();
                                    OpBalance = orderedGetFromBalance.ObValue ?? 0m;
                                    StartDate = orderedGetFromBalance.StartDate;
                                }
                                else
                                {
                                    OpBalance = 0m;
                                    StartDate = newTicketitem.Doappointment;
                                }
                                if (string.IsNullOrEmpty(newTicketitem.TicketNoDependent))
                                {
                                    newTicketitem.TicketNoDependent = "0";
                                }
                                if (newTicketitem.TicketEntitlement == null || newTicketitem.TicketEntitlement <= 0)
                                {
                                    newTicketitem.TicketEntitlement = 1;
                                }
                                BalanceInYear = Convert.ToInt32(newTicketitem.TicketNoDependent) / newTicketitem.TicketEntitlement;

                                DateTime startDateTime = DateHelper.StringToDate(StartDate);
                                // get from Ticket
                                var getFromTicket = getFromTickets.Where(x => x.EmpId == newTicketitem.Id).ToList();
                                if (getFromTicket.Any())
                                {
                                    var filteredData = getFromTicket.Where(x => DateHelper.StringToDate(x.TicketDate) >= startDateTime && DateHelper.StringToDate(x.TicketDate) <= currDateTime);
                                    if (filteredData.Any())
                                    {
                                        TicketCount = filteredData.Sum(x => x.TicketCount);
                                    }
                                }

                                DaysCount = (currDateTime - startDateTime).TotalDays;
                                if (DaysCount > 0 && BalanceInYear > 0)
                                {
                                    BalanceDays = (BalanceInYear / 364) * (decimal)DaysCount;
                                }

                                Curbalance = (OpBalance ?? 0) + (BalanceDays ?? 0) - (TicketCount ?? 0);
                                OtherBalanceDto newItem = new OtherBalanceDto();
                                newItem.EmpId = newTicketitem.Id;
                                newItem.EmpCode = newTicketitem.EmpId;
                                newItem.EmpName = newTicketitem.EmpName;
                                newItem.EmpName2 = newTicketitem.EmpName2;
                                newItem.CurBalance = Math.Round(Curbalance, 3);
                                newItem.StartDate = StartDate;
                                newItem.BalanceDays = Math.Round(BalanceDays ?? 0, 3);
                                newItem.BalanceUsed = BalanceUsed;
                                newItem.BalanceInYear = BalanceInYear;
                                newItem.CurrDate = currDate;
                                result.Add(newItem);
                            }
                            catch { continue; }
                        }
                    }
                    //الخروج والعودة
                    else if (TypeID == 2)
                    {
                        if (getFromHrEmployeeForCmdtype2.Count() > 0)
                        {
                            var empData = getFromHrEmployeeForCmdtype2.ToList();
                            var getFromBalances = await hrRepositoryManager.HrOpeningBalanceRepository.GetAll(x => x.IsDeleted == false && x.TypeId == 2);
                            var getFromVisas = await hrRepositoryManager.HrVisaRepository.GetAll(x => x.IsDeleted == false && x.VisaDate != null && x.IsBillable == 2);

                            foreach (var newitem in empData)
                            {
                                try
                                {
                                    BalanceInYear = 1; BalanceDays = 0; StartDate = string.Empty;
                                    var getFromBalance = getFromBalances.Where(x => x.EmpId == newitem.Id).ToList();
                                    if (getFromBalance.Count() > 0)
                                    {
                                        var orderedGetFromBalance = getFromBalance.OrderByDescending(x => DateHelper.StringToDate(x.StartDate)).FirstOrDefault();
                                        if (orderedGetFromBalance != null)
                                        {
                                            OpBalance = orderedGetFromBalance.ObValue ?? 0m;
                                            StartDate = orderedGetFromBalance.StartDate;
                                        }
                                    }
                                    else
                                    {
                                        OpBalance = 0m;
                                        StartDate = newitem.Doappointment;
                                    }
                                    DateTime startDateTime = DateHelper.StringToDate(StartDate);
                                    // مجموع االتذاكر من بداية اعتبار التذاكر حتى تاريخ اليوم
                                    var getFromVisa = getFromVisas.Where(x => x.EmpId == newitem.Id).ToList();
                                    decimal VisaCount = 0;
                                    if (getFromVisa.Count() > 0)
                                    {
                                        if (!string.IsNullOrEmpty(StartDate))
                                        {
                                            var filteredData = getFromVisa.Where(x => DateHelper.StringToDate(x.VisaDate) >= startDateTime && DateHelper.StringToDate(x.VisaDate) <= currDateTime);
                                            VisaCount = filteredData.Count();
                                        }
                                    }

                                    DaysCount = (currDateTime - startDateTime).TotalDays;
                                    if (DaysCount > 0)
                                    {
                                        BalanceDays = (BalanceInYear / 364) * (decimal)DaysCount;
                                    }

                                    Curbalance = (OpBalance ?? 0) + (BalanceDays ?? 0) - (VisaCount);

                                    OtherBalanceDto newItem = new OtherBalanceDto();
                                    newItem.EmpId = newitem.Id;
                                    newItem.EmpCode = newitem.EmpId;
                                    newItem.EmpName = newitem.EmpName;
                                    newItem.EmpName2 = newitem.EmpName2 ?? string.Empty;
                                    newItem.CurBalance = Math.Round(Curbalance, 3);
                                    newItem.StartDate = StartDate;
                                    newItem.BalanceDays = Math.Round(BalanceDays ?? 0, 3);
                                    newItem.BalanceUsed = VisaCount;
                                    newItem.BalanceInYear = BalanceInYear;
                                    newItem.CurrDate = currDate;
                                    result.Add(newItem);
                                }
                                catch { continue; }
                            }
                        }
                    }
                    // بدل السكن
                    else if (TypeID == 3) // بدل السكن
                    {
                        double? DaysCnt = 0; decimal? HousingAmountPaid = 0; decimal? BalanceInYearForHousing = 0;
                        var openingBalances = await hrRepositoryManager.HrOpeningBalanceRepository.GetAll(x => x.IsDeleted == false && x.TypeId == 3);
                        var allowanceDeductions = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(x => x.TypeId == 1 && x.IsDeleted == false && x.AdId == 1 && x.FixedOrTemporary == 1);
                        var payrollAllowances = await hrRepositoryManager.HrPayrollAllowanceVwRepository.GetAll(x => x.IsDeleted == false && x.AdId == 1);

                        // الرصيد الافتتاحي
                        foreach (var newHousingitem in getFromHrEmployeeForCmdtype2)
                        {
                            try
                            {
                                BalanceInYearForHousing = 0; DaysCnt = 0; BalanceUsed = 0; BalanceDays = 0; HousingAmountPaid = 0;
                                var openingBalance = openingBalances.Where(x => x.EmpId == newHousingitem.Id).ToList();
                                if (openingBalance.Any())
                                {
                                    var orderedOpeningBalance = openingBalance
                                        .OrderByDescending(x => DateHelper.StringToDate(x.StartDate)).FirstOrDefault();

                                    OpBalance = orderedOpeningBalance?.ObValue ?? 0m;
                                    StartDate = orderedOpeningBalance?.StartDate;
                                }
                                else
                                {
                                    OpBalance = 0m;
                                    StartDate = newHousingitem.Doappointment;
                                }

                                // البدل السكن في السنة
                                var allowanceDeduction = allowanceDeductions.Where(x => x.EmpId == newHousingitem.Id).ToList();
                                if (allowanceDeduction.Any())
                                {
                                    BalanceInYearForHousing = allowanceDeduction.Sum(x => x.Amount * 12 ?? 0m);
                                }
                                DateTime startDateTime = DateHelper.StringToDate(StartDate);

                                // مجموع بدل السكن المدفوع منذ بداية الفترة حتى الآن
                                var payrollAllowance = payrollAllowances.Where(x => x.EmpId == newHousingitem.Id).ToList();
                                if (payrollAllowance.Any())
                                {
                                    var filteredData = payrollAllowance.Where(x => DateHelper.StringToDate($"{x.FinancelYear}/{x.MsMonth}/01") >= startDateTime && DateHelper.StringToDate($"{x.FinancelYear}/{x.MsMonth}/01") <= currDateTime);
                                    HousingAmountPaid = filteredData.Sum(x => x.Amount);
                                }

                                BalanceUsed = HousingAmountPaid;

                                // نسبة الأيام من تاريخ البداية حتى اليوم
                                DaysCnt = (currDateTime - startDateTime).TotalDays;
                                if (DaysCnt > 0 && BalanceInYearForHousing > 0)
                                {
                                    BalanceDays = BalanceInYearForHousing / 364 * (decimal)DaysCnt;
                                }

                                Curbalance = (OpBalance ?? 0) + (BalanceDays ?? 0) - (HousingAmountPaid ?? 0);
                                OtherBalanceDto newItem = new OtherBalanceDto();
                                newItem.EmpId = newHousingitem.Id;
                                newItem.EmpCode = newHousingitem.EmpId;
                                newItem.EmpName = newHousingitem.EmpName;
                                newItem.EmpName2 = newHousingitem.EmpName2;
                                newItem.CurBalance = Math.Round(Curbalance, 3);
                                newItem.StartDate = StartDate;
                                newItem.BalanceDays = Math.Round(BalanceDays ?? 0, 3);
                                newItem.BalanceUsed = BalanceUsed;
                                newItem.BalanceInYear = BalanceInYearForHousing;
                                newItem.CurrDate = currDate;
                                result.Add(newItem);
                            }
                            catch { continue; }
                        }
                    }
                    else
                    {
                        if (getFromHrEmployeeForCmdtype2 != null)
                        {
                            foreach (var item in getFromHrEmployeeForCmdtype2)
                            {
                                OtherBalanceDto newItem = new OtherBalanceDto();
                                newItem.EmpId = item.Id;
                                newItem.EmpCode = item.EmpId;
                                newItem.EmpName = item.EmpName;
                                newItem.EmpName2 = item.EmpName2;
                                newItem.CurBalance = 0;
                                newItem.StartDate = "";
                                newItem.BalanceDays = 0;
                                newItem.BalanceUsed = 0;
                                newItem.BalanceInYear = 0;
                                newItem.OpBalance = 0;
                                newItem.CurrDate = currDate;
                                result.Add(newItem);
                            }
                        }
                    }
                }
                return await Result<List<OtherBalanceDto>>.SuccessAsync(result, "Item Retrieved");
            }
            catch (Exception ex)
            {
                return await Result<List<OtherBalanceDto>>.FailAsync(ex.Message);
            }
        }

        public async Task<IResult<List<HrOpeningBalanceFilterDto>>> Search(HrOpeningBalanceFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                List<HrOpeningBalanceFilterDto> result = new();
                filter.TypeId ??= 0; filter.BranchId ??= 0;

                var items = await hrRepositoryManager.HrOpeningBalanceRepository.GetAllVw(x => x.IsDeleted == false
                && (filter.TypeId == 0 || filter.TypeId == x.TypeId)
                && (string.IsNullOrEmpty(filter.EmpCode) || x.EmpCode == filter.EmpCode)
                && (filter.BranchId == 0 || filter.BranchId == x.BranchId));

                foreach (var item in items)
                {
                    var newItem = new HrOpeningBalanceFilterDto
                    {
                        Id = item.Id,
                        TypeId = item.TypeId,
                        EmpCode = item.EmpCode,
                        EmpName = item.EmpName,
                        EmpName2 = item.EmpName2,
                        StartDate = item.StartDate,
                        ObValue = item.ObValue,
                        TypeName = item.TypeName,
                        TypeName2 = item.TypeName2
                    };
                    result.Add(newItem);
                }
                return await Result<List<HrOpeningBalanceFilterDto>>.SuccessAsync(result, "");
            }
            catch (Exception ex)
            {
                return await Result<List<HrOpeningBalanceFilterDto>>.FailAsync(ex.Message);
            }
        }

        public async Task<IResult<List<OtherBalanceDto>>> CurrBalanceSearch(CurrentBalanceFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.EmpCode) || string.IsNullOrEmpty(filter.CurrDate) || filter.TypeId <= 0)
                    return await Result<List<OtherBalanceDto>>.FailAsync(localization.GetMessagesResource("dataRequire"));

                var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == filter.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmpExist != null)
                {
                    var result = await HR_Other_balances_SP(filter.CurrDate, checkEmpExist.Id, filter.TypeId, 1);
                    return result;
                }
                else
                {
                    return await Result<List<OtherBalanceDto>>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                }
            }
            catch (Exception ex)
            {
                return await Result<List<OtherBalanceDto>>.FailAsync(ex.Message);
            }
        }

        public async Task<IResult<List<OtherBalanceDto>>> CurrBalanceAllSearch(CurrentBalanceFilterDto filter, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrEmpty(filter.CurrDate) || filter.TypeId <= 0)
                    return await Result<List<OtherBalanceDto>>.FailAsync(localization.GetMessagesResource("dataRequire"));

                long Id = 0;
                if (!string.IsNullOrEmpty(filter.EmpCode))
                {
                    var checkEmpExist = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == filter.EmpCode && x.IsDeleted == false && x.Isdel == false);
                    if (checkEmpExist != null)
                        Id = checkEmpExist.Id;
                    else
                        return await Result<List<OtherBalanceDto>>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                }

                var result = await HR_Other_balances_SP(filter.CurrDate, Id, filter.TypeId, 2);
                return result;
            }
            catch (Exception ex)
            {
                return await Result<List<OtherBalanceDto>>.FailAsync(ex.Message);
            }
        }
    }
}
