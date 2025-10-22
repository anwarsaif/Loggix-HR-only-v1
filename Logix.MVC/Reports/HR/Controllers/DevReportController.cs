using DevExpress.XtraReports.UI;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IServices;
using Logix.Domain.HR;
using Logix.Infrastructure.Repositories;
using Logix.MVC.Helpers;
using Logix.MVC.Reports.HR.DevReports;
using Logix.MVC.Reports.HR.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Reports.HR.Controllers
{
    [Area("HR")]
    public class DevReportController : Controller
    {
        private readonly IDevReportHelper devReportHelper;
        private readonly ICurrentData session;
        private readonly IHrServiceManager hrServiceManager;
        private readonly IAccServiceManager accServiceManager;
        private readonly IMainServiceManager mainServiceManager;

        public DevReportController(IDevReportHelper devReportHelper,
            ICurrentData session,
            IHrServiceManager hrServiceManager,
            IAccServiceManager accServiceManager,
            IMainServiceManager mainServiceManager)
        {
            this.devReportHelper = devReportHelper;
            this.session = session;
            this.hrServiceManager = hrServiceManager;
            this.accServiceManager = accServiceManager;
            this.mainServiceManager = mainServiceManager;
        }
        [HttpGet]
        public async Task<IActionResult> DevReport_Viewer(long Rep_ID, long Cus_RP_ID, long T_ID, string Filters, string ChkIds)
        {

            try
            {
                devReportHelper.InitSession();
                var setRptPath = await devReportHelper.SetCustomReportPath("Accounting", Rep_ID, Cus_RP_ID);
                if (!setRptPath.Succeeded)
                {
                    ViewData["RptException"] = setRptPath.Status.message;
                    return View("~/Views/_ReportViewerError.cshtml");
                }
                ViewData["rptCustomReportAddDto"] = setRptPath.Data;

                var filtersDictionary = devReportHelper.DecodeFilterToDictionary(Filters);

                var basicData = new ReportBasicDataDto();
                XtraReport myReport = new(); object myDataSource = new(); bool IsAutoDirection = true;
                switch (Rep_ID)
                {
                    case 1: //   
                        basicData = await devReportHelper.GetBasicData("تقرير الموظفيين", "Employee Report");
                        var filter = HrReportsFilters.GetHrEmployee(filtersDictionary);
                        var getEmployee = await hrServiceManager.HrEmployeeService.Search(filter);
                        myReport = new RptHrEmployee();
                        myDataSource = new HrEmployeeDS() { BasicData = basicData, Filter = filter, Details = getEmployee.Data.ToList() };
                        break;

                    case 2: // 
                        basicData = await devReportHelper.GetBasicData("تقرير الموظفيين", "Employee Bending Report");
                        var filter2 = HrReportsFilters.GetHrEmployeeBending(filtersDictionary);
                        var getEmployeeBending = await hrServiceManager.HrEmployeeService.EmployeeBendingSearch(filter2);
                        myReport = new RptHrEmployeeBending();
                        myDataSource = new HrEmployeeBendingDS() { BasicData = basicData, Filter = filter2, Details = getEmployeeBending.Data.ToList() };
                        break;

                    case 3: // 
                        basicData = await devReportHelper.GetBasicData("تقرير انهاء الخدمة", "End Of Service Report");
                        var filter3 = HrReportsFilters.GetHrEndOfService(filtersDictionary);
                        var getEndOfService = await hrServiceManager.HrLeaveService.Search(filter3);
                        myReport = new RptHrEndOfService();
                        myDataSource = new HrEndOfServiceDS() { BasicData = basicData, Filter = filter3, Details = getEndOfService.Data.ToList() };
                        break;

                    case 4: // 
                        basicData = await devReportHelper.GetBasicData("تقرير التنقلات", "Transfer Report");
                        var filter4 = HrReportsFilters.GetHrTransfer(filtersDictionary);
                        var getTransfer = await hrServiceManager.HrTransferService.Search(filter4);
                        myReport = new RptHrTransfer();
                        myDataSource = new HrTransferDS() { BasicData = basicData, Filter = filter4, Details = getTransfer.Data.ToList() };
                        break;

                    case 5: //   
                        basicData = await devReportHelper.GetBasicData("تقرير بالسلف", "Loans Report");
                        var filter5 = HrReportsFilters.GetHrLoan(filtersDictionary);
                        var getFromLoan = await hrServiceManager.HrLoanService.Search(filter5);
                        myReport = new RptHrLoan();
                        myDataSource = new HrLoanDS() { BasicData = basicData, Filter = filter5, Details = getFromLoan.Data.ToList() };
                        break;

                    case 6: //    
                        basicData = await devReportHelper.GetBasicData("تقرير الحسميات والبدلات الاخرى", "Allowance Deduction Report");
                        var filter6 = HrReportsFilters.GetHrAllowanceDeduction(filtersDictionary);
                        var items = await hrServiceManager.HrAllowanceDeductionService.Search(filter6);
                        myReport = new RptHrAllowanceDeduction();
                        myDataSource = new HrAllowanceDeductionDS() { BasicData = basicData, Filter = filter6, Details = items.Data.ToList() };
                        break;

                    case 7: //    
                        basicData = await devReportHelper.GetBasicData("تقرير العلاوات والزيادات", "Bonus And Increments Report");
                        var filter7 = HrReportsFilters.GetHrIncrement(filtersDictionary);
                        var itemsIncrement = await hrServiceManager.HrIncrementService.Search(filter7);
                        myReport = new RptHrIncrement();
                        myDataSource = new HrIncrementDS() { BasicData = basicData, Filter = filter7, Details = itemsIncrement.Data.ToList() };
                        break;

                    case 8: //     
                        basicData = await devReportHelper.GetBasicData("تقرير تغيير حالة الموظفيين", "Employee Status Report");
                        var filter8 = HrReportsFilters.GetHrEmpStatus(filtersDictionary);
                        var employees = await hrServiceManager.HrEmpStatusHistoryService.Search(filter8);
                        myReport = new RptEmpStatus();
                        myDataSource = new HrEmpStatusDS() { BasicData = basicData, Filter = filter8, Details = employees.Data.ToList() };
                        break;

                    case 9: //     
                        basicData = await devReportHelper.GetBasicData("تقرير مباشرة عمل", "Join Work Report");
                        var filter9 = HrReportsFilters.GetHrJoinWork(filtersDictionary);
                        var getJoinWork = await hrServiceManager.HrDirectJobService.Search(filter9);
                        myReport = new RptJoinWork();
                        myDataSource = new HrJoinWorkDS() { BasicData = basicData, Filter = filter9, Details = getJoinWork.Data.ToList() };
                        break;
                    case 10: //     
                        basicData = await devReportHelper.GetBasicData("تقرير بالتحضير", "Attendance Report");
                        var filter10 = HrReportsFilters.GetHrHrAttendance(filtersDictionary);
                        var searchResult = await hrServiceManager.HrAttendanceService.AttendanceSearch(filter10);
                        myReport = new RptAttendance();
                        myDataSource = new HrAttendanceDS() { BasicData = basicData, Filter = filter10, Details = searchResult.Data.ToList() };
                        break;
                    case 11: //     
                        basicData = await devReportHelper.GetBasicData("تقرير بالتحضير التفصيلي", "Attendance Data Report");
                        var filter11 = HrReportsFilters.GetHrHrAttendanceData(filtersDictionary);
                        var getData = await hrServiceManager.HrAttendanceReport3Service.GetAttendanceData(filter11);
                        myReport = new RptAttendanceData();
                        myDataSource = new HrAttendanceDataDS() { BasicData = basicData, Filter = filter11, Details = getData.Data.ToList() };
                        break;
                    case 12: //     
                        basicData = await devReportHelper.GetBasicData("تقرير بالدخول والخروج للموظف", "In / Out Employee Report");
                        var filter12 = HrReportsFilters.GetHrActualAttendance(filtersDictionary);
                        var result = await hrServiceManager.HrActualAttendanceService.Search(filter12);
                        myReport = new RptActualAttendance();
                        myDataSource = new HrActualAttendanceDS() { BasicData = basicData, Filter = filter12, Details = result.Data.ToList() };
                        break;
                    case 13: //     
                        basicData = await devReportHelper.GetBasicData("تقرير بالحضور والانصراف", "Attendance and departure report");
                        var filter13 = HrReportsFilters.GetHrAttendanceReport(filtersDictionary);
                        var getAttendanceReport = await hrServiceManager.HrAttendanceService.getHR_Attendance_Report_SP(filter13);
                        myReport = new RptAttendanceReport();
                        myDataSource = new HrAttendanceReportDS() { BasicData = basicData, Filter = filter13, Details = getAttendanceReport.Data.ToList() };
                        break;
                    case 14: //     
                        basicData = await devReportHelper.GetBasicData("تقرير بإجمالي الحضور والتأخير والإضافي للموظفين", "Report on total attendance and absences");
                        var filter14 = HrReportsFilters.GetHrAttendanceTotalReport(filtersDictionary);
                        var getAttendanceTotalReport = await hrServiceManager.HrAttendanceService.HR_Attendance_TotalReport_SP(filter14);
                        myReport = new RptAttendanceTotalReport();
                        myDataSource = new HrAttendanceTotalReportDS() { BasicData = basicData, Filter = filter14, Details = getAttendanceTotalReport.Data.ToList() };
                        break;
                    case 15: //     
                        basicData = await devReportHelper.GetBasicData("تقرير الغيابات", "Absences Report");
                        var filter15 = HrReportsFilters.GetHrAbsence(filtersDictionary);
                        var getHrAbsence = await hrServiceManager.HrAbsenceService.Search(filter15);
                        myReport = new RptAbsence();
                        myDataSource = new HrAbsenceDS() { BasicData = basicData, Filter = filter15, Details = getHrAbsence.Data.ToList() };
                        break;
                    case 16: //     
                        basicData = await devReportHelper.GetBasicData("تقرير الاوقات الاضافية", "OverTime Report");
                        var filter16 = HrReportsFilters.GetHrOverTimeM(filtersDictionary);
                        var getOverTimeM = await hrServiceManager.HrOverTimeMService.Search(filter16);
                        myReport = new RptOverTimeM();
                        myDataSource = new HrOverTimeMDS() { BasicData = basicData, Filter = filter16, Details = getOverTimeM.Data.ToList() };
                        break;
                    case 17: //     
                        basicData = await devReportHelper.GetBasicData("تقرير التاخير", "Delay Report");
                        var filter17 = HrReportsFilters.GetHrDelay(filtersDictionary);
                        var getDelay = await hrServiceManager.HrDelayService.Search(filter17);
                        myReport = new RptDelay();
                        myDataSource = new HrDelayDS() { BasicData = basicData, Filter = filter17, Details = getDelay.Data.ToList() };
                        break;
                    case 18: //     
                        basicData = await devReportHelper.GetBasicData("تقرير اعتماد الغيابات الغير مسجله للموظفيين", "Approval Absence Report");
                        var filter18 = HrReportsFilters.GetHrApprovalAbsence(filtersDictionary);
                        var getApprovalAbsence = await hrServiceManager.HrAbsenceService.HRApprovalAbsencesReport(filter18);
                        myReport = new RptApprovalAbsence();
                        myDataSource = new HrApprovalAbsenceDS() { BasicData = basicData, Filter = filter18, Details = getApprovalAbsence.Data.ToList() };
                        break;
                    case 19: //     
                        basicData = await devReportHelper.GetBasicData("تقرير الاستئذان", "Permission Report");
                        var filter19 = HrReportsFilters.GetHrPermission(filtersDictionary);
                        var getPermission = await hrServiceManager.HrPermissionService.Search(filter19);
                        myReport = new RptPermission();
                        myDataSource = new HrPermissionDS() { BasicData = basicData, Filter = filter19, Details = getPermission.Data.ToList() };
                        break;
                    case 20: //     
                        basicData = await devReportHelper.GetBasicData("تقرير بالحضور والإنصراف حسب الموظف", "Attendance Report For Employee");
                        var filter20 = HrReportsFilters.GetHrAttendanceReportForEmp(filtersDictionary);
                        var getHrAttendanceReportForEmp = await hrServiceManager.HrAttendanceService.Search(filter20);
                        myReport = new RptAttendanceReportForEmp();
                        myDataSource = new HrAttendanceReportForEmpDS() { BasicData = basicData, Filter = filter20, Details = getHrAttendanceReportForEmp.Data.ToList() };
                        break;
                    case 21: //     
                        basicData = await devReportHelper.GetBasicData("البصمات الغير معروفة", "Attendance Unknown Report");
                        var filter21 = HrReportsFilters.GetHrAttendanceUnknown(filtersDictionary);
                        var getAttendanceUnknown = await hrServiceManager.HrCheckInOutService.Search(filter21);
                        myReport = new RptAttendanceUnknown();
                        myDataSource = new HrAttendanceUnknownDS() { BasicData = basicData, Filter = filter21, Details = getAttendanceUnknown.Data.ToList() };
                        break;
                    case 22: //     
                        basicData = await devReportHelper.GetBasicData(" تقرير الحضور والانصراف اليومي", "Attendance Report Days");
                        var filter22 = HrReportsFilters.GetHrAttendanceReportDays(filtersDictionary);
                        var getHrAttendanceReportDays = await hrServiceManager.HrAttendanceService.HR_Attendance_Report6_SP(filter22);
                        myReport = new RptAttendanceReportDays();
                        myDataSource = new HrAttendanceReportDaysDS() { BasicData = basicData, Filter = filter22, Details = getHrAttendanceReportDays.Data.ToList() };
                        break;
                    case 23: //     
                        basicData = await devReportHelper.GetBasicData("  ملخص الحضور والإنصراف خلال فترة", "Attendance Total Report");
                        var filter23 = HrReportsFilters.GetHrAttendanceTotalFromTo(filtersDictionary);
                        var getAttendanceTotalFromTo = await hrServiceManager.HrAttendanceService.HR_Attendance_TotalReportNew_SP(filter23);
                        myReport = new RptAttendanceTotalFromTo();
                        myDataSource = new HrAttendanceTotalFromToDS() { BasicData = basicData, Filter = filter23, Details = getAttendanceTotalFromTo.Data.ToList() };
                        break;
                    case 24: //     
                        basicData = await devReportHelper.GetBasicData("رفع بيانات الحضور والانصراف", "Checking Staff Report");
                        var filter24 = HrReportsFilters.GetHrCheckingStaff(filtersDictionary);
                        var getCheckingStaff = await hrServiceManager.HrAttendanceService.GetEmployeesForUploadAttendances(filter24);
                        myReport = new RptCheckingStaff();
                        myDataSource = new HrCheckingStaffDS() { BasicData = basicData, Filter = filter24, Details = getCheckingStaff.Data.ToList() };
                        break;
                    case 25: //     الاجازات
                        basicData = await devReportHelper.GetBasicData(" الاجازات", "Vacations Report");
                        var filter25 = HrReportsFilters.GetHRVacations(filtersDictionary);
                        var getHRVacations = await hrServiceManager.HrVacationsService.Search(filter25);
                        myReport = new RptVacations();
                        myDataSource = new HRVacationsDS() { BasicData = basicData, Filter = filter25, Details = getHRVacations.Data.ToList() };
                        break;
                    case 26: //     
                        basicData = await devReportHelper.GetBasicData(" تقرير بأرصدة الاجازات", "Vacation Balance ALL Report");
                        var filter26 = HrReportsFilters.GetHRVacationBalanceALL(filtersDictionary);
                        var getVacationBalanceALL = await hrServiceManager.HrVacationBalanceService.VacationBalanceALL(filter26);
                        myReport = new RptVacationBalanceALL();
                        myDataSource = new HrVacationBalanceALLDS() { BasicData = basicData, Filter = filter26, Details = getVacationBalanceALL.Data.ToList() };
                        break;
                    case 27: //     
                        basicData = await devReportHelper.GetBasicData("  الرصيد الافتتاحي للإجازات", "Vacation Balance Report");
                        var filter27 = HrReportsFilters.GetHRVacationBalance(filtersDictionary);
                        var getVacationBalance = await hrServiceManager.HrVacationBalanceService.Search(filter27);
                        myReport = new RptVacationBalance();
                        myDataSource = new HrVacationBalanceDS() { BasicData = basicData, Filter = filter27, Details = getVacationBalance.Data.ToList() };
                        break;
                    case 28: //     
                        basicData = await devReportHelper.GetBasicData("رصيد إجازة موظف", "Vacation Employee Balance Report");
                        var filter28 = HrReportsFilters.GetHRVacationEmpBalance(filtersDictionary);
                        var getVacationEmpBalance = await hrServiceManager.HrVacationBalanceService.VacationEmpBalanceSearch(filter28);
                        myReport = new RptVacationEmpBalance();
                        myDataSource = new HrVacationEmpBalanceDS() { BasicData = basicData, Filter = filter28, Details = getVacationEmpBalance.Data.ToList() };
                        break;
                    case 29: //     
                        basicData = await devReportHelper.GetBasicData("   تقرير بالإجازات", "Vacations Report");
                        var filter29 = HrReportsFilters.GetHRVacationReport(filtersDictionary);
                        var res = await hrServiceManager.HrVacationsService.VacationReportSearch(filter29);
                        myReport = new RptVacationsReport();
                        myDataSource = new HrVacationsReportDS() { BasicData = basicData, Filter = filter29, Details = res.Data.ToList() };
                        break;
                    case 30: //     
                        basicData = await devReportHelper.GetBasicData("تقرير بالموظفين المجازين", "Vacations Employees Report");
                        var filter30 = HrReportsFilters.GetHRRPVacationEmployee(filtersDictionary);
                        var getHRRPVacationEmployee = await hrServiceManager.HrVacationsService.HRRVacationEmployeeSearch(filter30);
                        myReport = new HRRPVacationEmployee();
                        myDataSource = new HRRPVacationEmployeeDS() { BasicData = basicData, Filter = filter30, Details = getHRRPVacationEmployee.Data.ToList() };
                        break;
                    case 31: //     
                        basicData = await devReportHelper.GetBasicData("الارصدة الإفتتاحية الأخرى", "Balance Report");
                        var filter31 = HrReportsFilters.GetHRBalance(filtersDictionary);
                        var getBalance = await hrServiceManager.HrOpeningBalanceService.Search(filter31);
                        myReport = new RptBalance();
                        myDataSource = new HrBalanceDS() { BasicData = basicData, Filter = filter31, Details = getBalance.Data.ToList() };
                        break;
                    case 32: //     
                        basicData = await devReportHelper.GetBasicData("تقرير المرشحين", "Recruitment Candidate Report");
                        var filter32 = HrReportsFilters.GetHRRecruitmentCandidate(filtersDictionary);
                        var getRecruitmentCandidate = await hrServiceManager.HrRecruitmentCandidateService.Search(filter32);
                        myReport = new RptRecruitmentCandidate();
                        myDataSource = new HrRecruitmentCandidateDS() { BasicData = basicData, Filter = filter32, Details = getRecruitmentCandidate.Data.ToList() };
                        break;
                    case 33: //     
                        basicData = await devReportHelper.GetBasicData("تقرير تقييمات الأداء", "RepKPI Report");
                        var filter33 = HrReportsFilters.GetHrRepKPI(filtersDictionary);
                        var getRepKPI = await hrServiceManager.HrKpiService.Search(filter33);
                        myReport = new RepKPI();
                        myDataSource = new RepKPIDS() { BasicData = basicData, Filter = filter33, Details = getRepKPI.Data.ToList() };
                        break;
                    case 34: //     
                        basicData = await devReportHelper.GetBasicData(" تقرير بالأرصدة الأخرى لموظف", "Current Balance Report");
                        var filter34 = HrReportsFilters.GetHrCurrBalance(filtersDictionary);
                        var getCurrBalance = await hrServiceManager.HrOpeningBalanceService.CurrBalanceSearch(filter34);
                        myReport = new RptCurrBalance();
                        myDataSource = new HrCurrBalanceDS() { BasicData = basicData, Filter = filter34, Details = getCurrBalance.Data.ToList() };
                        break;
                    case 35: //     
                        basicData = await devReportHelper.GetBasicData(" تقرير بالأرصدة الأخرى لجميع الموظفين", "Current Balance All Report");
                        var filter35 = HrReportsFilters.GetHrCurrBalanceAll(filtersDictionary);
                        var getCurrBalanceAll = await hrServiceManager.HrOpeningBalanceService.CurrBalanceAllSearch(filter35);
                        myReport = new RptCurrentBalanceAll();
                        myDataSource = new HrCurrBalanceAllDS() { BasicData = basicData, Filter = filter35, Details = getCurrBalanceAll.Data.ToList() };
                        break;
                    case 36: // تقرير توزيع الرواتب حسب الفرع    
                        basicData = await devReportHelper.GetBasicData("تقرير توزيع الرواتب حسب الفرع", "Payroll By Branch Report");
                        var filter36 = HrReportsFilters.GetHrPayrollByBranch(filtersDictionary);
                        var getPayrollByBranch = await hrServiceManager.HrPayrollDService.Search(filter36);
                        myReport = new RptPayrollByBranch();
                        myDataSource = new HrPayrollByBranchDS() { BasicData = basicData, Filter = filter36, Details = getPayrollByBranch.Data.ToList() };
                        break;
                    case 37: // استعلام عن راتب موظف    
                        basicData = await devReportHelper.GetBasicData("استعلام عن راتب موظف", "Payroll Query Report");
                        var filter38 = HrReportsFilters.GetHrPayrollQuery(filtersDictionary);
                        var getPayrollQuery = await hrServiceManager.HrPayrollDService.SearchPayrollQuery(filter38);
                        myReport = new RptHRPayrollQuery();
                        myDataSource = new HRPayrollQueryDS() { BasicData = basicData, Filter = filter38, Details = getPayrollQuery.Data.ToList() };
                        break;
                    case 38: // استعلام عن راتب موظف    
                        basicData = await devReportHelper.GetBasicData("استعلام عن راتب موظف", "Payroll Query By Id Report");
                        myReport = new RptHRPayrollQuery();
                        myDataSource = new HRPayrollQueryDS() { BasicData = basicData, Details = await GetPayrollCheckB(T_ID) };
                        break;
                    case 39: //الموظفين الذين لم يصدر لهم رواتب    
                        basicData = await devReportHelper.GetBasicData("الموظفين الذين لم يصدر لهم رواتب", "Unpaid Employees Report");
                        var filter39 = HrReportsFilters.GetHrUnpaidEmployees(filtersDictionary);
                        var getUnpaidEmployees = await hrServiceManager.HrEmployeeService.UnpaidEmployeesSearch(filter39);
                        myReport = new RptUnpaidEmployees();
                        myDataSource = new HrUnpaidEmployeesDS() { BasicData = basicData, Filter = filter39, Details = getUnpaidEmployees.Data.ToList() };
                        break;
                    case 40: //الموظفين الذين لم يصدر لهم رواتب    
                        basicData = await devReportHelper.GetBasicData("الموظفين", "Edit Employee Data Report");
                        myReport = new RptEditEmployeeData();
                        myDataSource = new EditEmployeeDataDS() { BasicData = basicData, Details = await GetEmployeeData(T_ID) };
                        break;
                    case 41: //تقرير توزيع الرواتب حسب الموقع    
                        basicData = await devReportHelper.GetBasicData("تقرير توزيع الرواتب حسب الموقع", "Payroll By Locations");
                        var filter41 = HrReportsFilters.GetHrPayrollByLocation(filtersDictionary);
                        var getPayrollByLocation = await hrServiceManager.HrPayrollDService.PayrollByLocationSearch(filter41);
                        myReport = new RptPayrollByLocation();
                        myDataSource = new HrPayrollByLocationDS() { BasicData = basicData, Filter = filter41, Details = getPayrollByLocation.Data.ToList() };
                        break;
                    case 42: //تقرير توزيع الرواتب حسب الإدارات    
                        basicData = await devReportHelper.GetBasicData("تقرير توزيع الرواتب حسب الإدارات", "Payroll By Departments");
                        var filter42 = HrReportsFilters.GetHrPayrollByDep(filtersDictionary);
                        var getPayrollByDep = await hrServiceManager.HrPayrollDService.PayrollByDeptSearch(filter42);
                        myReport = new RptHrPayrolByDepartment();
                        myDataSource = new HrPayrollByDepDS() { BasicData = basicData, Filter = filter42, Details = getPayrollByDep.Data.ToList() };
                        break;
                    case 43: //مقارنة  الرواتب  حسب الفرع  
                        basicData = await devReportHelper.GetBasicData("مقارنة  الرواتب  حسب الفرع", "Comper By Branch");
                        var filter43 = await hrServiceManager.HrAllReportsService.GetHrCompareByBranch(filtersDictionary);
                        var getComperByBranch = await hrServiceManager.HrPayrollDService.SearchComperByBranch(filter43);
                        myReport = new RptCompareByBranch();
                        myDataSource = new HrCompareByBranchDS() { BasicData = basicData, Filter = filter43, Details = getComperByBranch.Data.ToList() };
                        break;
                    case 44: //الجزاءت   
                        basicData = await devReportHelper.GetBasicData("الجزاءت", "Disciplinary Case Action Report");
                        myReport = new RptDisciplinaryCaseActionEdit();
                        myDataSource = new HrDisciplinaryCaseActionEditDS() { BasicData = basicData, Details = await GetDisciplinaryCaseAction(T_ID) };
                        break;
                    case 45: //المؤهلات والخبرات
                        basicData = await devReportHelper.GetBasicData("تقرير المؤهلات والخبرات", "Qualifications And Experiences Report");
                        var filter45 = HrReportsFilters.GetHRQualifications(filtersDictionary);
                        var getHRQualifications = await hrServiceManager.HrEmployeeService.HRQualificationsSearch(filter45);
                        myReport = new RptHRQualifications();
                        myDataSource = new HRQualificationsDS() { BasicData = basicData, Filter = filter45, Details = getHRQualifications.Data.ToList() };
                        break;
					case 46: //مخصص تامين طبي للموظف   
            basicData = await devReportHelper.GetBasicData("مخصص تامين طبي للموظف", "Provisions MedicalInsurance Employee Report");
						myReport = new RP_Provisions_MedicalInsurance_Employee();
						myDataSource = new RP_Provisions_MedicalInsurance_EmployeeDS() { BasicData = basicData, Details = await GetProvisionsMedicalInsuranceEmployee(T_ID) };
						break;
          case 47: // تقرير بعهد الموظفين
            basicData = await devReportHelper.GetBasicData("تقرير بعهد الموظفين", "Ohad Employees Report");
            var filter47 = HrReportsFilters.GetHRReportOhad(filtersDictionary);
            var GetData = await hrServiceManager.HrOhadService.RPOhadSerach(filter47);
            myReport = new ReportOhad();
            myDataSource = new ReportOhadDS() { BasicData = basicData, Filter = filter47, Details = GetData.Data.ToList() };
            break;
          case 48: //مخصص نهاية خدمه   
            basicData = await devReportHelper.GetBasicData("مخصص نهاية خدمه", "Provisions End Of Service Report");
            myReport = new HRProvisionsAllReport();
            myDataSource = new HRProvisionsEmployeeDS() { BasicData = basicData, Details = await GetProvisionsEmployee(T_ID) };
            break;
          case 49: //مخصص تذاكر   
            basicData = await devReportHelper.GetBasicData("مخصص تذاكر", "Provisions Ticket Report");
            myReport = new HRProvisionsAllReport();
            myDataSource = new HRProvisionsEmployeeDS() { BasicData = basicData, Details = await GetProvisionsEmployee(T_ID) };
            break;
          case 50: //مخصص اجازات   
            basicData = await devReportHelper.GetBasicData("مخصص اجازات", "Provisions Vacation Report");
            myReport = new HRProvisionsAllReport();
            myDataSource = new HRProvisionsEmployeeDS() { BasicData = basicData, Details = await GetProvisionsEmployee(T_ID) };
            break;
          case 51: // تقرير بتكاليف الموظفين
            basicData = await devReportHelper.GetBasicData("تقرير بتكاليف الموظفين", "Employee Cost Report");
            var filter51 = HrReportsFilters.GetHrEmployeeCost(filtersDictionary);
            var getEmployeeCost = await hrServiceManager.HrEmployeeCostService.Search(filter51);
            myReport = new RptHrEmployeeCost();
            myDataSource = new HrEmployeeCostDS() { BasicData = basicData, Filter = filter51, Details = getEmployeeCost.Data.ToList() };
            break;
          case 52: // تقرير ملفات الموظفين
            basicData = await devReportHelper.GetBasicData("تقرير ملفات الموظفين", "Employee File Report");
            var filter52 = HrReportsFilters.GetHrEmployeeFile(filtersDictionary);
            var getEmployeeFile = await hrServiceManager.HrEmployeeService.SearchEmployeeFile(filter52);
            myReport = new RptHrEmployeeFile();
            myDataSource = new HrEmployeeFileDS() { BasicData = basicData, Filter = filter52, Details = getEmployeeFile.Data.ToList() };
            break;
          case 53: // تقرير انتهاء الهوية للموظفين
            basicData = await devReportHelper.GetBasicData("تقرير انتهاء الهوية للموظفين", "Employee IDExpire Report");
            var filter53 = HrReportsFilters.GetHREmpIDExpireReport(filtersDictionary);
            var getEmpIDExpireReport = await hrServiceManager.HrEmployeeService.SearchEmpIDExpireReport(filter53);
            myReport = new HREmpIDExpireReport();
            myDataSource = new HREmpIDExpireReportDS() { BasicData = basicData, Filter = filter53, Details = getEmpIDExpireReport.Data.ToList() };
            break;
          case 54: //   تقرير بالسلف
            basicData = await devReportHelper.GetBasicData("تقرير بالسلف", "Loans Report");
            var filter54 = HrReportsFilters.GetHrLoan(filtersDictionary);
            var getLoanReport = await hrServiceManager.HrLoanService.Search(filter54);
            myReport = new HRLoanReport();
            myDataSource = new HRLoanReportDS() { BasicData = basicData, Filter = filter54, Details = getLoanReport.Data.ToList() };
            break;
          case 55: //   تقرير بانتهاء جوازات السفر للموظفين
            basicData = await devReportHelper.GetBasicData("تقرير بانتهاء جوازات سفر الموظفين", "RPPassport Report");
            var filter55 = HrReportsFilters.GetHRRPPassport(filtersDictionary);
            var getRPPassport = await hrServiceManager.HrEmployeeService.SearchRPPassport(filter55);
            myReport = new RPPassport();
            myDataSource = new RPPassportDS() { BasicData = basicData, Filter = filter55, Details = getRPPassport.Data.ToList() };
            break;
          case 56: //   تقرير بانتهاء عقود الموظفين
            basicData = await devReportHelper.GetBasicData(" تقرير بانتهاء عقود الموظفين", "RPContract Report");
            var filter56 = HrReportsFilters.GetRPContract(filtersDictionary);
            var getRPContract = await hrServiceManager.HrEmployeeService.SearchRPContract(filter56);
            myReport = new RPContract();
            myDataSource = new RPContractDS() { BasicData = basicData, Filter = filter56, Details = getRPContract.Data.ToList() };
            break;
          case 57: //  تقرير بانتهاء التأمينات الطبية
            basicData = await devReportHelper.GetBasicData(" تقرير بانتهاء التأمينات الطبية", "RPMedicalInsurance Report");
            var filter57 = HrReportsFilters.GetRPMedicalInsurance(filtersDictionary);
            var getMedicalInsurance = await hrServiceManager.HrEmployeeService.SearchRPMedicalInsurance(filter57);
            myReport = new RPMedicalInsurance();
            myDataSource = new RPMedicalInsuranceDS() { BasicData = basicData, Filter = filter57, Details = getMedicalInsurance.Data.ToList() };
            break;
          case 58: //  تقرير بتاريخ تعيين الموظفين 
            basicData = await devReportHelper.GetBasicData(" تقرير بتاريخ تعيين الموظفين ", "DOAppointment Report");
            var filter58 = HrReportsFilters.GetDOAppointment(filtersDictionary);
            var getDOAppointment = await hrServiceManager.HrEmployeeService.SearchRPDOAppointement(filter58);
            myReport = new DOAppointment();
            myDataSource = new DOAppointmentDS() { BasicData = basicData, Filter = filter58, Details = getDOAppointment.Data.ToList() };
            break;
          case 59: //  تقرير بالموظفين المستبعدين من التحضير 
            basicData = await devReportHelper.GetBasicData(" تقرير بالموظفين المستبعدين من التحضير ", "RPAttend Report");
            var filter59 = HrReportsFilters.GetRPAttend(filtersDictionary);
            var getRPAttend = await hrServiceManager.HrEmployeeService.SearchRPAttend(filter59);
            myReport = new RPAttend();
            myDataSource = new RPAttendDS() { BasicData = basicData, Filter = filter59, Details = getRPAttend.Data.ToList() };
            break;
          case 60: //  تقرير بالموظفين حسب البنك 
            basicData = await devReportHelper.GetBasicData(" تقرير بالموظفين حسب البنك", "RPBank Report");
            var filter60 = HrReportsFilters.GetRPBank(filtersDictionary);
            var getRPBank = await hrServiceManager.HrEmployeeService.SearchRPBank(filter60);
            myReport = new RPBank();
            myDataSource = new RPBankDS() { BasicData = basicData, Filter = filter60, Details = getRPBank.Data.ToList() };
            break;
          case 61: // تقرير بعهد الموظفين
            basicData = await devReportHelper.GetBasicData("تقرير بعهد الموظفين", "Ohad Employees Report");
            var filter61 = HrReportsFilters.GetHRReportOhad(filtersDictionary);
            var GetOhadData = await hrServiceManager.HrOhadService.RPOhadSerach(filter61);
            myReport = new RPAllOhad();
            myDataSource = new ReportOhadDS() { BasicData = basicData, Filter = filter61, Details = GetOhadData.Data.ToList() };
            break;
          case 62: // تقرير برواتب الموظفين والبدلات والحسميات
            basicData = await devReportHelper.GetBasicData("تقرير برواتب الموظفين والبدلات والحسميات", "Staff Salaries Allowances Deductions Report");
            var filter62 = HrReportsFilters.GetHrStaffSalariesAllowancesDeductions(filtersDictionary);
            var GetHrStaffSalariesAllowancesDeductions = await hrServiceManager.HrEmployeeService.SearchHrStaffSalariesAllowancesDeductions(filter62);
            myReport = new HrStaffSalariesAllowancesDeductions();
            myDataSource = new HrStaffSalariesAllowancesDeductionsDS() { BasicData = basicData, Filter = filter62, Details = GetHrStaffSalariesAllowancesDeductions.Data.ToList() };
            break;
          case 63: // كشف حساب موظف
            basicData = await devReportHelper.GetBasicData("كشف حساب موظف", "Account Transactions Report");
            var filter63 = HrReportsFilters.GetHRAccountTransactions(filtersDictionary);
            var getHRAccountTransactions = await accServiceManager.AccAccountService.AccountTransactionsSearch(filter63);
            myReport = new RptHRAccountTransactions();
            myDataSource = new HRAccountTransactionsDS() { BasicData = basicData, Filter = filter63, Details = getHRAccountTransactions.Data.ToList() };
            break;
          default:
                        ViewData["RptException"] = "Invalid reportId";
                        return View("~/Views/_ReportViewerError.cshtml");
                }

                var openReport = await devReportHelper.OpenReport(myReport, myDataSource, IsAutoDirection);
                if (openReport.Succeeded)
                {
                    ViewData["ReportName"] = openReport.Data;
                    return View("~/Views/_ReportViewer.cshtml");
                }
                else
                {
                    ViewData["RptException"] = openReport.Status.message;
                    return View("~/Views/_ReportViewerError.cshtml");
                }
            }
            catch (Exception exp)
            {
                ViewData["RptException"] = exp.Message;
                return View("~/Views/_ReportViewerError.cshtml");
            }
        }

    #region==================================================HR===============================
    public async Task<List<HrDisciplinaryCaseActionVw>> GetDisciplinaryCaseAction(long empId)
    {
      try
      {
        var items = await hrServiceManager.HrDisciplinaryCaseActionService.GetOneVW(
            x => x.Id == empId && x.IsDeleted == false
        );              // التحقق من أن النتيجة ناجحة وأن بها بيانات
        if (items.Data != null)
        {
          return new List<HrDisciplinaryCaseActionVw> { items.Data };
        }

        return new List<HrDisciplinaryCaseActionVw>();
      }
      catch
      {
        return new List<HrDisciplinaryCaseActionVw>();
      }
    }
    //[NonAction]
    //private EmployeeFilterDto GetHrEmployee(Dictionary<string, string> dictionary)
    //{
    //    return new EmployeeFilterDto()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //        //JDateGregorian = dictionary["JDateGregorian"],
    //        //JDateGregorian2 = dictionary["JDateGregorian2"],
    //    };
    //}
    //[NonAction]
    //private Application.DTOs.Main.HrEmployeeBendingFilterVM GetHrEmployeeBending(Dictionary<string, string> dictionary)
    //{
    //    return new Application.DTOs.Main.HrEmployeeBendingFilterVM()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //        //JDateGregorian = dictionary["JDateGregorian"],
    //    };
    //}
    //[NonAction]
    //private HrLeaveFilterDto GetHrEndOfService(Dictionary<string, string> dictionary)
    //{
    //    return new HrLeaveFilterDto()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //    };
    //}
    //[NonAction]
    //private HrTransferFilterDto GetHrTransfer(Dictionary<string, string> dictionary)
    //{
    //    string GetValue(string key, string defaultValue = "")
    //    {
    //        return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
    //    }

    //    return new HrTransferFilterDto()
    //    {
    //        EmpId = GetValue("EmpId", ""),
    //        EmpName = GetValue("EmpName", ""),
    //        DeptId = Convert.ToInt32(GetValue("DeptId", "0")),
    //        BranchId = Convert.ToInt32(GetValue("BranchId", "0")),
    //        BranchToId = Convert.ToInt64(GetValue("BranchToId", "0")),
    //        LocationId = Convert.ToInt32(GetValue("LocationId", "0")),
    //        LocationFromId = Convert.ToInt64(GetValue("LocationFromId", "0")),
    //        LocationToId = Convert.ToInt64(GetValue("LocationToId", "0")),
    //        TransDepartmentFrom = Convert.ToInt64(GetValue("TransDepartmentFrom", "0")),
    //        TransDepartmentTo = Convert.ToInt64(GetValue("TransDepartmentTo", "0")),
    //        FromDate = GetValue("FromDate", ""),
    //        ToDate = GetValue("ToDate", "")
    //    };
    //}

    //[NonAction]
    //private HrLoanFilterDto GetHrLoan(Dictionary<string, string> dictionary)
    //{
    //    return new HrLoanFilterDto()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //    };
    //}
    //[NonAction]
    //private HrAllowanceDeductionOtherFilterDto GetHrAllowanceDeduction(Dictionary<string, string> dictionary)
    //{
    //    return new HrAllowanceDeductionOtherFilterDto()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //    };
    //}
    //[NonAction]
    //private HrIncrementFilterDto GetHrIncrement(Dictionary<string, string> dictionary)
    //{
    //    return new HrIncrementFilterDto()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //    };
    //}
    //[NonAction]
    //private HRRPEmpStatusHistoryFilterDto GetHrEmpStatus(Dictionary<string, string> dictionary)
    //{
    //    return new HRRPEmpStatusHistoryFilterDto()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //    };
    //}
    //[NonAction]
    //private HrDirectJobFilterDto GetHrJoinWork(Dictionary<string, string> dictionary)
    //{
    //    string GetValue(string key, string defaultValue = "")
    //    {
    //        return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
    //    }

    //    return new HrDirectJobFilterDto()
    //    {
    //        EmpId = GetValue("EmpId", ""),
    //        EmpName = GetValue("EmpName", ""),
    //        DeptId = Convert.ToInt32(GetValue("DeptId", "0")),
    //        BranchId = Convert.ToInt32(GetValue("BranchId", "0")),
    //        LocationId = Convert.ToInt32(GetValue("LocationId", "0")),
    //        From = GetValue("From", ""),
    //        To = GetValue("To", "")
    //    };
    //}
    //[NonAction]
    //private HrAttendancesFilterDto GetHrHrAttendance(Dictionary<string, string> dictionary)
    //{
    //    return new HrAttendancesFilterDto()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //    };
    //}
    //[NonAction]
    //private HRAttendanceReport4FilterDto GetHrHrAttendanceData(Dictionary<string, string> dictionary)
    //{
    //    return new HRAttendanceReport4FilterDto()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //    };
    //}
    //[NonAction]
    //private HrCheckInOutFilterDto GetHrActualAttendance(Dictionary<string, string> dictionary)
    //{
    //    return new HrCheckInOutFilterDto()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //    };
    //}
    //[NonAction]
    //private HRAttendanceReportFilterDto GetHrAttendanceReport(Dictionary<string, string> dictionary)
    //{
    //    return new HRAttendanceReportFilterDto()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //    };
    //}
    //[NonAction]
    //private HRAttendanceTotalReportSPFilterDto GetHrAttendanceTotalReport(Dictionary<string, string> dictionary)
    //{
    //    return new HRAttendanceTotalReportSPFilterDto()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //    };
    //}
    //[NonAction]
    //private HrAbsenceFilterDto GetHrAbsence(Dictionary<string, string> dictionary)
    //{
    //    return new HrAbsenceFilterDto()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //    };
    //}
    //[NonAction]
    //private HrOverTimeMFilterDto GetHrOverTimeM(Dictionary<string, string> dictionary)
    //{
    //    return new HrOverTimeMFilterDto()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //    };
    //}
    //[NonAction]
    //private HrDelayFilterDto GetHrDelay(Dictionary<string, string> dictionary)
    //{
    //    return new HrDelayFilterDto()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //    };
    //}
    //[NonAction]
    //private HRApprovalAbsencesReportFilterDto GetHrApprovalAbsence(Dictionary<string, string> dictionary)
    //{
    //    return new HRApprovalAbsencesReportFilterDto()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //    };
    //}
    //[NonAction]
    //private HrPermissionFilterDto GetHrPermission(Dictionary<string, string> dictionary)
    //{
    //    return new HrPermissionFilterDto()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //    };
    //}
    //[NonAction]
    //private HRAttendanceReport5FilterDto GetHrAttendanceReportForEmp(Dictionary<string, string> dictionary)
    //{
    //    return new HRAttendanceReport5FilterDto()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //    };
    //}
    //[NonAction]
    //private HrAttendanceUnknownFilterDto GetHrAttendanceUnknown(Dictionary<string, string> dictionary)
    //{
    //    return new HrAttendanceUnknownFilterDto()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //    };
    //}
    //[NonAction]
    //private HRAttendanceReport6FilterSP GetHrAttendanceReportDays(Dictionary<string, string> dictionary)
    //{
    //    return new HRAttendanceReport6FilterSP()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //    };
    //}
    //[NonAction]
    //private HRAttendanceTotalReportFilterDto GetHrAttendanceTotalFromTo(Dictionary<string, string> dictionary)
    //{
    //    return new HRAttendanceTotalReportFilterDto()
    //    {
    //        //PeriodId = Convert.ToInt64(dictionary["PeriodId"]),
    //        //JCode = dictionary["JCode"],
    //        //JCode2 = dictionary["JCode2"],
    //    };
    //}
    //[NonAction]
    //private HRAttendanceCheckingStaffFilterDto GetHrCheckingStaff(Dictionary<string, string> dictionary)
    //{
    //    return new HRAttendanceCheckingStaffFilterDto()
    //    {
    //        empCode = dictionary["empCode"],
    //        Date = dictionary["Date"],
    //        DeptId = Convert.ToInt32(dictionary["DeptId"]),
    //    };
    //}
    //[NonAction]
    //private HrVacationsFilterDto GetHRVacations(Dictionary<string, string> dictionary)
    //{
    //    return new HrVacationsFilterDto()
    //    {
    //        EmpCode = dictionary["EmpCode"],
    //        EmpName = dictionary["EmpName"],
    //        BranchId = Convert.ToInt32(dictionary["BranchId"]),
    //        ClearnceId = Convert.ToInt32(dictionary["ClearnceId"]),
    //        StartDate = dictionary["StartDate"],
    //        EndDate = dictionary["EndDate"],
    //        DeptId = Convert.ToInt32(dictionary["DeptId"]),
    //        VacationTypeId = Convert.ToInt32(dictionary["VacationTypeId"]),
    //        LocationId = Convert.ToInt32(dictionary["LocationId"]),
    //        TransTypeId = Convert.ToInt32(dictionary["TransTypeId"])
    //    };
    //}

    //[NonAction]
    //private HrVacationBalanceALLSendFilterDto GetHRVacationBalanceALL(Dictionary<string, string> dictionary)
    //{
    //    return new HrVacationBalanceALLSendFilterDto()
    //    {
    //        BranchId = Convert.ToInt64(dictionary["BranchId"]),
    //        FacilityId = Convert.ToInt32(dictionary["FacilityId"]),
    //        DeptId = Convert.ToInt32(dictionary["DeptId"]),
    //        JobCatagoriesId = Convert.ToInt32(dictionary["JobCatagoriesId"]),
    //        Location = Convert.ToInt32(dictionary["Location"]),
    //        StatusId = Convert.ToInt32(dictionary["StatusId"]),
    //        NationalityId = Convert.ToInt32(dictionary["NationalityId"]),
    //        VacationTypeId = Convert.ToInt32(dictionary["VacationTypeId"]),
    //        EmpId = dictionary["EmpId"].ToString(),
    //        EmpName = dictionary["EmpName"],
    //        CurrentDate = dictionary["CurrentDate"]
    //    };
    //}

    //[NonAction]
    //private HrVacationBalanceFilterDto GetHRVacationBalance(Dictionary<string, string> dictionary)
    //{
    //    return new HrVacationBalanceFilterDto()
    //    {
    //        //BranchId = Convert.ToInt64(dictionary["BranchId"]),
    //        //FacilityId = Convert.ToInt32(dictionary["FacilityId"]),
    //        //DeptId = Convert.ToInt32(dictionary["DeptId"]),
    //    };
    //}
    //[NonAction]
    //private HrVacationEmpBalanceDto GetHRVacationEmpBalance(Dictionary<string, string> dictionary)
    //{
    //    return new HrVacationEmpBalanceDto()
    //    {
    //        Emp_Code = dictionary["Emp_Code"],
    //        Currentdate = dictionary["Currentdate"]
    //    };
    //}
    //[NonAction]
    //private HrVacationsFilterDto GetHRVacationReport(Dictionary<string, string> dictionary)
    //{
    //    return new HrVacationsFilterDto()
    //    {
    //        BranchId = Convert.ToInt32(dictionary["BranchId"]),
    //        DeptId = Convert.ToInt32(dictionary["DeptId"]),
    //        EmpCode = dictionary["EmpCode"],
    //        LocationId = Convert.ToInt32(dictionary["LocationId"]),
    //        StartDate = dictionary["StartDate"],
    //        EndDate = dictionary["EndDate"],
    //        VacationTypeId = Convert.ToInt32(dictionary["VacationTypeId"]),
    //        ChkGroupByEmpAndVacation = Convert.ToBoolean(dictionary["ChkGroupByEmpAndVacation"])
    //    };
    //}
    //[NonAction]
    //private HrRPVacationEmployeeFilterDto GetHRRPVacationEmployee(Dictionary<string, string> dictionary)
    //{
    //    return new HrRPVacationEmployeeFilterDto()
    //    {
    //        BranchId = Convert.ToInt32(dictionary["BranchId"]),
    //        LocationId = Convert.ToInt32(dictionary["LocationId"]),
    //        DeptId = Convert.ToInt32(dictionary["DeptId"]),
    //        EmpCode = dictionary["EmpCode"],
    //        StartDate = dictionary["StartDate"],
    //        EndDate = dictionary["EndDate"],
    //        VacationTypeId = Convert.ToInt32(dictionary["VacationTypeId"])
    //    };
    //}
    //[NonAction]
    //private HrOpeningBalanceFilterDto GetHRBalance(Dictionary<string, string> dictionary)
    //{
    //    return new HrOpeningBalanceFilterDto()
    //    {
    //        TypeId = Convert.ToInt64(dictionary["TypeId"]),
    //        BranchId = Convert.ToInt32(dictionary["BranchId"]),
    //        EmpCode = dictionary["EmpCode"]
    //    };
    //}
    //[NonAction]
    //private HrRecruitmentCandidateFilterDto GetHRRecruitmentCandidate(Dictionary<string, string> dictionary)
    //{
    //    return new HrRecruitmentCandidateFilterDto()
    //    {
    //        //BranchId = Convert.ToInt64(dictionary["BranchId"]),
    //        //FacilityId = Convert.ToInt32(dictionary["FacilityId"]),
    //        //DeptId = Convert.ToInt32(dictionary["DeptId"]),
    //    };
    //}
    //[NonAction]
    //private HRRepKPIFilterDto GetHrRepKPI(Dictionary<string, string> dictionary)
    //{
    //    return new HRRepKPIFilterDto()
    //    {
    //        //BranchId = Convert.ToInt64(dictionary["BranchId"]),
    //        //FacilityId = Convert.ToInt32(dictionary["FacilityId"]),
    //        //DeptId = Convert.ToInt32(dictionary["DeptId"]),
    //    };
    //}
    //[NonAction]
    //private CurrentBalanceFilterDto GetHrCurrBalance(Dictionary<string, string> dictionary)
    //{
    //    return new CurrentBalanceFilterDto()
    //    {
    //        EmpCode = dictionary["EmpCode"],
    //        TypeId = Convert.ToInt32(dictionary["TypeId"]),
    //        CurrDate = dictionary["CurrDate"],
    //    };
    //}
    //[NonAction]
    //      private CurrentBalanceFilterDto GetHrCurrBalanceAll(Dictionary<string, string> dictionary)
    //      {
    //          return new CurrentBalanceFilterDto()
    //          {
    //              EmpCode = dictionary["EmpCode"],
    //              TypeId = Convert.ToInt32(dictionary["TypeId"]),
    //              CurrDate = dictionary["CurrDate"],
    //          };
    //      }

    //      [NonAction]
    //      private HrPayrollFilterDto GetHrPayrollByBranch(Dictionary<string, string> dictionary)
    //      {
    //          return new HrPayrollFilterDto()
    //          {
    //              FinancelYear = Convert.ToInt32(dictionary["FinancelYear"]),
    //              BranchId = Convert.ToInt64(dictionary["BranchId"]),
    //              //MsMonth = dictionary["MsMonth"],
    //              PayrollTypeId = Convert.ToInt32(dictionary["PayrollTypeId"]),
    //              FacilityId = Convert.ToInt64(dictionary["FacilityId"]),
    //              //MsId = Convert.ToInt64(dictionary["MsId"]),
    //              //MsCode = Convert.ToInt64(dictionary["MsCode"]),
    //              //MsDate = dictionary["MsDate"],
    //              //MsTitle = dictionary["MsTitle"],
    //              //MsMonthName = dictionary["MsMonthName"],
    //              //StatusName = dictionary["StatusName"],
    //              //TypeName = dictionary["TypeName"],
    //              //TypeName2 = dictionary["TypeName2"],
    //              //StatusName2 = dictionary["StatusName2"],
    //              //ApplicationCode = Convert.ToInt64(dictionary["ApplicationCode"]),
    //              //BranchsId = dictionary["BranchsId"],
    //              //Status = Convert.ToInt32(dictionary["Status"]),
    //          };
    //      }
    //      [NonAction]
    //      private HrPayrollQueryFilterDto GetHrPayrollQuery(Dictionary<string, string> dictionary)
    //      {
    //          return new HrPayrollQueryFilterDto()
    //          {
    //              //FinancelYear = Convert.ToInt32(dictionary["FinancelYear"]),
    //              //BranchId = Convert.ToInt64(dictionary["BranchId"]),
    //              ////MsMonth = dictionary["MsMonth"],
    //              //PayrollTypeId = Convert.ToInt32(dictionary["PayrollTypeId"]),
    //              //FacilityId = Convert.ToInt64(dictionary["FacilityId"]),
    //              ////MsId = Convert.ToInt64(dictionary["MsId"]),
    //              ////MsCode = Convert.ToInt64(dictionary["MsCode"]),
    //              ////MsDate = dictionary["MsDate"],
    //              ////MsTitle = dictionary["MsTitle"],
    //              ////MsMonthName = dictionary["MsMonthName"],
    //              ////StatusName = dictionary["StatusName"],
    //              ////TypeName = dictionary["TypeName"],
    //              ////TypeName2 = dictionary["TypeName2"],
    //              ////StatusName2 = dictionary["StatusName2"],
    //              ////ApplicationCode = Convert.ToInt64(dictionary["ApplicationCode"]),
    //              ////BranchsId = dictionary["BranchsId"],
    //              ////Status = Convert.ToInt32(dictionary["Status"]),
    //          };
    //      }

    [NonAction]
    private async Task<List<HrPayrollDVw>> GetPayrollCheckB(long payrIds)
    {
      try
      {
        var payrollResult = await hrServiceManager.HrPayrollDService.GetOneVW(x => x.MsdId == payrIds && x.IsDeleted == false);

        // التحقق من أن النتيجة ناجحة وأن بها بيانات
        if (payrollResult.Succeeded && payrollResult.Data != null)
        {
          return new List<HrPayrollDVw> { payrollResult.Data };
        }

        return new List<HrPayrollDVw>();
      }
      catch
      {
        return new List<HrPayrollDVw>();
      }
    }

    //      [NonAction]
    //      private HrUnpaidEmployeesFilter GetHrUnpaidEmployees(Dictionary<string, string> dictionary)
    //      {
    //          return new HrUnpaidEmployeesFilter()
    //          {
    //              FinancelYear = Convert.ToInt32(dictionary["FinancelYear"]),
    //              BranchId = Convert.ToInt32(dictionary["BranchId"]),
    //              MsMonth = dictionary["MsMonth"],
    //              EmpName = dictionary["EmpName"],
    //              JobCatagoriesId = Convert.ToInt32(dictionary["JobCatagoriesId"]),
    //              StatusId = Convert.ToInt32(dictionary["StatusId"]),
    //              NationalityId = Convert.ToInt32(dictionary["NationalityId"]),
    //              DeptId = Convert.ToInt32(dictionary["DeptId"]),
    //          };
    //      }


    [NonAction]
    private async Task<List<HrEmployeeVw>> GetEmployeeData(long empId)
    {
      try
      {
        var getEmpData = await hrServiceManager.HrEmployeeService.GetOneVW(
            x => x.Id == empId && x.IsDeleted == false
        );              // التحقق من أن النتيجة ناجحة وأن بها بيانات
        if (getEmpData.Succeeded && getEmpData.Data != null)
        {
          return new List<HrEmployeeVw> { getEmpData.Data };
        }

        return new List<HrEmployeeVw>();
      }
      catch
      {
        return new List<HrEmployeeVw>();
      }
    }

    //      [NonAction]
    //      private HrPayrollFilterDto GetHrPayrollByLocation(Dictionary<string, string> dictionary)
    //      {
    //          return new HrPayrollFilterDto()
    //          {
    //              FinancelYear = Convert.ToInt32(dictionary["FinancelYear"]),
    //              BranchId = Convert.ToInt32(dictionary["BranchId"]),
    //              PayrollTypeId = Convert.ToInt32(dictionary["PayrollTypeId"]),
    //              FacilityId = Convert.ToInt64(dictionary["FacilityId"]),
    //          };
    //      }
    //      [NonAction]
    //      private HrPayrollFilterDto GetHrPayrollByDep(Dictionary<string, string> dictionary)
    //      {
    //          return new HrPayrollFilterDto()
    //          {
    //              FinancelYear = Convert.ToInt32(dictionary["FinancelYear"]),
    //              BranchId = Convert.ToInt32(dictionary["BranchId"]),
    //              PayrollTypeId = Convert.ToInt32(dictionary["PayrollTypeId"]),
    //              FacilityId = Convert.ToInt64(dictionary["FacilityId"]),
    //          };
    //      }
    //      [NonAction]
    //      private async Task<List<HrDisciplinaryCaseActionVw>> GetDisciplinaryCaseAction(long empId)
    //      {
    //          try
    //          {
    //              var items = await hrServiceManager.HrDisciplinaryCaseActionService.GetOneVW(
    //                  x => x.Id == empId && x.IsDeleted == false
    //              );              // التحقق من أن النتيجة ناجحة وأن بها بيانات
    //              if (items.Succeeded && items.Data != null)
    //              {
    //                  return new List<HrDisciplinaryCaseActionVw> { items.Data };
    //              }

    //              return new List<HrDisciplinaryCaseActionVw>();
    //          }
    //          catch
    //          {
    //              return new List<HrDisciplinaryCaseActionVw>();
    //          }
    //      }

    //      [NonAction]
    //      private HrQualificationsFilterDto GetHRQualifications(Dictionary<string, string> dictionary)
    //      {
    //          return new HrQualificationsFilterDto()
    //          {
    //              BranchId = Convert.ToInt32(dictionary["BranchId"]),
    //              JobType = Convert.ToInt32(dictionary["JobType"]),
    //              DeptId = Convert.ToInt32(dictionary["DeptId"]),
    //              NationalityId = Convert.ToInt32(dictionary["NationalityId"]),
    //              JobCatagoriesId = Convert.ToInt32(dictionary["JobCatagoriesId"]),
    //              Status = Convert.ToInt32(dictionary["Status"]),
    //              EmpCode = dictionary["EmpCode"],
    //              EmpName = dictionary["EmpName"],
    //              IdNo = dictionary["IdNo"],
    //              PassId = dictionary["PassId"],
    //              EntryNo = dictionary["EntryNo"],
    //          };
    //      }

    [NonAction]
    private async Task<List<HrProvisionsMedicalInsuranceEmployeeVw>> GetProvisionsMedicalInsuranceEmployee(long empId)
    {
      try
      {
        var items = await hrServiceManager.HrProvisionsMedicalInsuranceEmployeeService.GetOneVW(
          x => x.PId == empId && x.IsDeleted == false
        );              // التحقق من أن النتيجة ناجحة وأن بها بيانات
        if (items.Succeeded && items.Data != null)
        {
          return new List<HrProvisionsMedicalInsuranceEmployeeVw> { items.Data };
        }

        return new List<HrProvisionsMedicalInsuranceEmployeeVw>();
      }
      catch
      {
        return new List<HrProvisionsMedicalInsuranceEmployeeVw>();
      }
    }

    [NonAction]
    private async Task<List<HrProvisionsEmployeeVw>> GetProvisionsEmployee(long empId)
    {
      try
      {
        var items = await hrServiceManager.HrProvisionsEmployeeService.GetOneVW(
          x => x.PId == empId && x.IsDeleted == false
        );              // التحقق من أن النتيجة ناجحة وأن بها بيانات
        if (items.Succeeded && items.Data != null)
        {
          return new List<HrProvisionsEmployeeVw> { items.Data };
        }

        return new List<HrProvisionsEmployeeVw>();
      }
      catch
      {
        return new List<HrProvisionsEmployeeVw>();
      }
    }

    //  [NonAction]
    //  private HRRPOhadFilterDto GetHRReportOhad(Dictionary<string, string> dictionary)
    //  {
    //    return new HRRPOhadFilterDto()
    //    {
    //      //BranchId = Convert.ToInt32(dictionary["BranchId"]),
    //      //JobType = Convert.ToInt32(dictionary["JobType"]),
    //      //DeptId = Convert.ToInt32(dictionary["DeptId"]),
    //    };
    //  }
    //  [NonAction]
    //  private HrEmployeeCostFilterDto GetHrEmployeeCost(Dictionary<string, string> dictionary)
    //  {
    //    return new HrEmployeeCostFilterDto()
    //    {
    //      //BranchId = Convert.ToInt32(dictionary["BranchId"]),
    //      //JobType = Convert.ToInt32(dictionary["JobType"]),
    //      //DeptId = Convert.ToInt32(dictionary["DeptId"]),
    //    };
    //  }
    //  [NonAction]
    //  private HrEmployeeFileFilterDto GetHrEmployeeFile(Dictionary<string, string> dictionary)
    //  {
    //    return new HrEmployeeFileFilterDto()
    //    {
    //      //BranchId = Convert.ToInt32(dictionary["BranchId"]),
    //      //JobType = Convert.ToInt32(dictionary["JobType"]),
    //      //DeptId = Convert.ToInt32(dictionary["DeptId"]),
    //    };
    //  }

    #endregion
  }
}
