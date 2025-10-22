
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Logix.Application.DTOs.HR
{

    //------------------Begin Of StoredProcedure [dbo].[HR_Attendance_Report_SP] Dtos-------------------------------//
    [Keyless]
    public class HRAttendanceReportDto
    {
        public string? Color_Value { get; set; }
        public string? MapLocation { get; set; }
        public string? Attendance_Type_Name { get; set; }
        public DateTime? Def_Time_Out { get; set; }
        public DateTime? Def_Time_In { get; set; }
        public string? Day_Name { get; set; }
        public string? Day_Date_Gregorian { get; set; }
        public string? Emp_Code { get; set; }
        public string? Emp_Name { get; set; }
        public string? TimeTable_Name { get; set; }
        public DateTime? Time_In { get; set; }
        public DateTime? Time_Out { get; set; }
        public DateTime? AllowTime_In { get; set; }
        public DateTime? AllowTime_Out { get; set; }
        public int? Delay { get; set; }
        public int? LeaveEarly { get; set; }
        public int? Late_Time_M { get; set; }
        public int? Leave_Early_Time_M { get; set; }
        public string? Day_Name2 { get; set; }
        public string? Emp_Name2 { get; set; }
        public string? Attendance_Type_Name2 { get; set; }
    }

    public class HRAttendanceReportFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public long? BranchId { get; set; }
        public string? DayDateGregorian { get; set; }
        public string? DayDateGregorian2 { get; set; }
        public long? TimeTableId { get; set; }
        public long? ManagerId { get; set; }
        public int? StatusId { get; set; }
        public long? Location { get; set; }
        public long? DeptId { get; set; }
        public long? ShitId { get; set; }
        public int? AttendanceType { get; set; }
        public string? BranchsId { get; set; }
        public int? SponsorsId { get; set; }
    }
    //--------------------------End Of StoredProcedure [dbo].[HR_Attendance_Report_SP] Dtos-------------------------------//

    //--------------------------Begin Of StoredProcedure [dbo].[HR_Attendance_Report4_SP] Dtos-------------------------------//
    [Keyless]

    public class HRAttendanceReport4Dto
    {
        public string? Color_Value { get; set; }
        public string? MapLocation { get; set; }
        public string? Attendance_Type_Name { get; set; }
        public DateTime? Def_Time_Out { get; set; }
        public DateTime? Def_Time_In { get; set; }
        public string? Day_Name { get; set; }
        public string? Day_Date_Gregorian { get; set; }
        public string? Emp_Code { get; set; }
        public string? Emp_name { get; set; }
        public string? TimeTable_Name { get; set; }
        public DateTime? Time_In { get; set; }
        public DateTime? Time_Out { get; set; }
        public bool? AllowTime_In { get; set; }
        public bool? AllowTime_Out { get; set; }
        public string? Location_Name { get; set; }
        public string? BRA_NAME { get; set; }
        public string? Day_Name2 { get; set; }
        public string? Location_Name2 { get; set; }
        public string? Emp_name2 { get; set; }
        public string? Dep_Name { get; set; }
        public string? Dep_name2 { get; set; }
        public long? TimeTable_ID { get; set; }
        public string? BRA_NAME2 { get; set; }

        public string? PermissionTime { get; set; }

        public int? Delay { get; set; }
        public int? LeaveEarly { get; set; }
        public int? Late_Time_M { get; set; }
        public int? Leave_Early_Time_M { get; set; }
        public int? Permission { get; set; }
        public int? Trng { get; set; }
        public int? Task { get; set; }
        public int? addTime { get; set; }
    }


    public class HRAttendanceReport4FilterDto
    {
        public long? BranchID { get; set; }
        public string? BranchsID { get; set; }
        public string? EmpName { get; set; }
        public long? TimeTableID { get; set; }
        [Required]
        public string DayDateGregorian { get; set; } = null!;
        [Required]
        public string DayDateGregorian2 { get; set; } = null!;
        public string? EmpCode { get; set; }
        public long? ManagerID { get; set; }
        public long? ShitID { get; set; }
        public int? StatusID { get; set; }
        public long? Location { get; set; }
        public long? DeptID { get; set; }
        public int? AttendanceType { get; set; }
        public int? SponsorsID { get; set; }
    }

    //--------------------------End Of StoredProcedure [dbo].[HR_Attendance_Report4_SP] Dtos-------------------------------//


    //--------------------------Begin Of StoredProcedure [dbo].[HR_Attendance_Report5_SP] Dtos-------------------------------//
    [Keyless]
    public class HRAttendanceReport5Dto
    {
        public string? Att_Date { get; set; }
        public string? Day_Name { get; set; }
        public string? Day_Name2 { get; set; }
        public string? Time_In { get; set; }
        public DateTime? Def_Time_In { get; set; }
        public string? Time_Out { get; set; }
        public DateTime? Def_Time_Out { get; set; }
        public long? Delay_in { get; set; }
        public long? Early_out { get; set; }
        public long? Over_Time { get; set; }
        public long? Work_hours { get; set; }
        public long? Def_Work_hours { get; set; }
        public string? Note { get; set; }
        public string? Shift_Name { get; set; }
        public int? Type_ID { get; set; }
        public string? Emp_Code { get; set; }
        public string? Emp_Name { get; set; }
        public string? Emp_Name2 { get; set; }
        public string? Dept_Name { get; set; }
        public string? Dept_Name2 { get; set; }
        public string? Position { get; set; }
    }
    public class HRAttendanceReport5FilterDto
    {
        public string? EmpName { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public int? CalendarType { get; set; }
        public int? Language { get; set; }

    }
    //--------------------------End Of StoredProcedure [dbo].[HR_Attendance_Report5_SP] Dtos-------------------------------//


    // تقرير بانتهاء جوازات سفر الموظفين
    public class RPPassportFilterDto
    {
        public string? From { get; set; }
        public string? To { get; set; }
        public string? empCode { get; set; }
        public string? EmpName { get; set; }

        public int? BranchId { get; set; }

        public int? Location { get; set; }
        public int? RemainingDays { get; set; }
        public string? PassExpireDate { get; set; }


    }

    // تقرير بانتهاء عقود الموظفين
    public class HrRPContractFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }
        public int? DepartmentId { get; set; }
        public int? LocationId { get; set; }
        public int? ContractTypeID { get; set; }
        public int? JobCategory { get; set; }
        public int? NationalityId { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? DOAppointment { get; set; }
        public string? CurrentData { get; set; }
        /////////////////////////////////////////////
        public decimal? Salary { get; set; }
        public decimal? NetSalary { get; set; }
        public string? BranchName { get; set; }
        public string? DepartmentName { get; set; }
        public string? LocationName { get; set; }
        public string? NationalityName { get; set; }
        public string? IdNo { get; set; }
        public int? RemainingDays { get; set; }
        public string? ContractExpiryDate { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Deduction { get; set; }

    }
    // تقرير بانتهاء التأمينات الطبية
    public class RPMedicalInsuranceFilterDto
    {
        public string? From { get; set; }
        public string? To { get; set; }
        public string? empCode { get; set; }
        public string? EmpName { get; set; }

        public int? BranchId { get; set; }

        public int? Location { get; set; }
        public int? RemainingDays { get; set; }
        public string? InuranceExpireDate { get; set; }


    }

    //  تقرير بتاريخ تعيين الموظفين
    public class DOAppointmentFilterDto
    {
        public string? From { get; set; }
        public string? To { get; set; }
        public string? empCode { get; set; }
        public string? EmpName { get; set; }

        public int? BranchId { get; set; }

        public int? Location { get; set; }
        public int? dept { get; set; }
        public string? DoAppointment { get; set; }
        public string? JobName { get; set; }
        public string? Nationality { get; set; }
        public int? NationalityId { get; set; }
        public string? DeptName { get; set; }
        public string? BranchName { get; set; }
        public string? LocationName { get; set; }


    }

    //  تقرير بالموظفين المستبعدين من التحضير
    public class RPAttendFilterDto
    {
        public string? empCode { get; set; }
        public string? EmpName { get; set; }
        public long? Id { get; set; }
    }
    //  تقرير بالموظفين حسب البنك
    public class RPBankFilterDto
    {
        public string? empCode { get; set; }
        public string? EmpName { get; set; }
        public int? Bank { get; set; }
        public int? Branch { get; set; }
        public string? IBan { get; set; }
        public string? BankName { get; set; }
    }

    // تقرير بانتهاء عقود الموظفين
    public class HrRPResignationFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }
        public int? LocationId { get; set; }
        public int? JobCategory { get; set; }
        public int? NationalityId { get; set; }
        public int? FacilityId { get; set; }
        public int? LeaveReason { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        /////////////////////////////////////////////

        public string? BranchName { get; set; }
        public string? DepartmentName { get; set; }
        public string? LocationName { get; set; }
        public string? NationalityName { get; set; }
        public string? FacilityName { get; set; }
        public string? CatName { get; set; }
        public string? Note { get; set; }
        public string? EndOfServiceReason { get; set; }
        public string? EndOfServiceDate { get; set; }
        public string? DOAppointment { get; set; }



    }


    // تقرير برواتب الموظفين والبدلات والحسميات
    public class HrStaffSalariesAllowancesDeductionsFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }
        public int? JobCategory { get; set; }
        public int? JobType { get; set; }
        public int? Status { get; set; }

        public int? NationalityId { get; set; }
        public int? DepartmentId { get; set; }
        public string? IdNo { get; set; }
        public string? PassId { get; set; }
        public string? EntryNo { get; set; }
        public int? LocationId { get; set; }
        public int? SalaryGroup { get; set; }

        /////////////////////////////////////////////
        public decimal? Salary { get; set; }
        public decimal? NetSalary { get; set; }
        public string? BranchName { get; set; }
        public string? DepartmentName { get; set; }
        public string? LocationName { get; set; }
        public string? NationalityName { get; set; }
        public string? CatName { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Deduction { get; set; }

    }



    // تقرير بالمواقع والورديات
    public class HrEmpWorkLocationFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }

        public int? ShitId { get; set; }
        public int? Location { get; set; }


        /////////////////////////////////////////////
        public decimal? NetSalary { get; set; }
        public string? BranchName { get; set; }
        public string? DepartmentName { get; set; }
        public string? LocationName { get; set; }
        public string? BankName { get; set; }
        public string? GroupName { get; set; }
        public string? AppointmentDate { get; set; }
        public decimal? GOSIDeduction { get; set; }


    }



    //استحقاق التذاكر
    public class HRTicketDueFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }

        public int? DepartmentId { get; set; }

        // غير موجود في النظام القديم
        //  public string? ToDate { get; set; }
        public int? NationalityId { get; set; }

        public int? Location { get; set; }
        public int? JobCategory { get; set; }

        /////////////////////////////////////////////
        public decimal? NetSalary { get; set; }
        public decimal? Salary { get; set; }
        public string? DoAppointment { get; set; }
        public decimal? TicketAmount { get; set; }
        public string? NoOfTickets { get; set; }
        public decimal? ValueTicket { get; set; }
        public decimal? PaidTicket { get; set; }
        public decimal? Balance { get; set; }
        //  الاستحقاقات
        public decimal? Due { get; set; }
    }

    //  تقرير بمراكز تكلفة الموظفين
    public class RPCostCenterEmployeeFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? StatusId { get; set; }


        /////////////////////////////////////////////

        public string? LocationName { get; set; }
        public string? IdNo { get; set; }
        public string? CostCenterCode { get; set; }
        public string? CostCenterName { get; set; }
        public string? SalaryGroupName { get; set; }
    }

    // إستحقاق الإجازات
    public class HrVacationDueFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }
        public int? DepartmentId { get; set; }
        public int? LocationId { get; set; }
        public int? JobCategory { get; set; }
        public int? NationalityId { get; set; }
        public string? CurrentDate { get; set; }
        /////////////////////////////////////////////
        public decimal? Salary { get; set; }
        public decimal? NetSalary { get; set; }
        public decimal? VacationBalance { get; set; }
        public decimal? Due { get; set; }
        public int? VacationDaysYear { get; set; }
        public string? DOAppointment { get; set; }


    }


    //  استحقاق نهاية الخدمة
    public class HREndOfServiceDueFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }
        public string? BranchName { get; set; }
        public int? DepartmentId { get; set; }
        public int? LocationId { get; set; }
        public int? JobCategory { get; set; }
        public int? LeaveType { get; set; }
        public int? NationalityId { get; set; }
        public string? CurrentDate { get; set; }
        /////////////////////////////////////////////
        public decimal? TotalSalary { get; set; }
        public decimal? Salary { get; set; }
        public decimal? NetSalary { get; set; }
        public decimal? EndServiceDue { get; set; }
        public decimal? previousDue { get; set; }
        public decimal? TotalAllowanceDeduction { get; set; }
        public string? DOAppointment { get; set; }
        public string? PDate { get; set; }
        public decimal? ForDays { get; set; }
        public decimal? ForMonths { get; set; }
        public decimal? RemainingYears { get; set; }
        public decimal? FirstFiveYears { get; set; }
        public int? MonthsCount { get; set; }
        public int? DaysCount { get; set; }
        public int? YearsCount { get; set; }


    }



    //  تقرير الأرشيف
    public class HRRPArchiveFilterDto
    {
        public int? FacilityId { get; set; }
        public int? TransactionType { get; set; }
        public string? fileName { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        /////////////////////////////////////////////
        public long? Id { get; set; }
        public string? FileDescription { get; set; }
        public string? FileDate { get; set; }
        public string? FileUrl { get; set; }


    }

    //  تقرير بالقرارات
    public class HrRPDecisionsFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }
        public int? DepartmentId { get; set; }
        public int? LocationId { get; set; }
        public int? DecisionTytpe { get; set; }
        public long? DecisionCode { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

        /////////////////////////////////////////////
        public string? DecisionTytpeName { get; set; }
        public string? DecisionDate { get; set; }
        public string? BranchName { get; set; }
        public string? DepartmentName { get; set; }
        public string? LocationName { get; set; }

    }

    // تقرير بإنتهاء رخص الموظفين
    public class HRRPlicenseFilterDto
    {
        public string? From { get; set; }
        public string? To { get; set; }
        public string? empCode { get; set; }
        public string? EmpName { get; set; }

        public int? BranchId { get; set; }
        public int? LicenseType { get; set; }
        /// <summary>
        /// ///////////////////
        /// </summary>
        public int? RemainingDays { get; set; }
        public long? Id { get; set; }
        public string? LicenseTypeName { get; set; }
        public string? ExpiryDate { get; set; }


    }

    //تقرير بالموظفين
    public class HRRPEmployeeFilterDto
    {

        public string? EmpName { get; set; }
        public string? EmpCode { get; set; }
        public int? JobType { get; set; }
        public int? JobCatagoriesId { get; set; }
        public int? Status { get; set; }
        public int? NationalityId { get; set; }
        public int? DeptId { get; set; }
        public string? IdNo { get; set; }
        public string? PassId { get; set; }
        public string? EntryNo { get; set; }
        public int? Location { get; set; }
        public int? SponsorsID { get; set; }
        public int? FacilityId { get; set; }
        public int? Level { get; set; }
        public int? Degree { get; set; }
        public int? ContractType { get; set; }
        public int? Protection { get; set; }
        public string? EmpCode2 { get; set; }
        public int? BranchId { get; set; }

        //////////////////////////////
        ///
        public string? EmpPhoto { get; set; }
        public string? EmpName2 { get; set; }
        public string? BranchName { get; set; }
        public string? DeptName { get; set; }
        public string? LocationName { get; set; }
        public string? Catname { get; set; }
        public string? DOAppointment { get; set; }
        public string? ContractexpiryDate { get; set; }
        public string? LeaveDate { get; set; }
        public string? StatusName { get; set; }



    }

    //   تقرير بالانتداب
    public class HRRPMandateFilterDto
    {

        public string? EmpName { get; set; }
        public string? EmpCode { get; set; }
        public int? JobCatagoriesId { get; set; }
        public int? TypeId { get; set; }
        public int? DeptId { get; set; }
        public int? BranchId { get; set; }
        public int? Location { get; set; }
        public int? FromLocation { get; set; }
        public int? ToLocation { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

        //////////////////////////////
        public long? Id { get; set; }
        public int? NumberOfNights { get; set; }
        public string? FromLocationName { get; set; }
        public string? ToLocationName { get; set; }
        public decimal? ActualExpenses { get; set; }

    }

    // تقرير بالموقوفين رواتبهم
    public class HRRPOFSalarieFilterDto
    {

        public string? EmpName { get; set; }
        public string? EmpCode { get; set; }
        public int? BranchId { get; set; }

        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

        //////////////////////////////
        public string? StopSalaryName { get; set; }
        public string? StopSalaryDate { get; set; }

    }

    //  تقرير المخالفات والجزاءت 
    public class HRRPDisciplinaryCaseActionFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }
        public int? DepartmentId { get; set; }
        public int? LocationId { get; set; }
        public int? DisciplinaryCaseID { get; set; }
        public int? ActionType { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        /////////////////////////////////////////////
        public string? DueDate { get; set; }
        public string? CaseName { get; set; }
        public string? ActionName { get; set; }
        public int? NoOfRepeat { get; set; }
        public decimal? DeductionRate { get; set; }
        public decimal? DeductionAmount { get; set; }


    }


    //  تقرير تقييمات الأداء
    public class HRRepKPIFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }
        public int? DepartmentId { get; set; }
        public int? LocationId { get; set; }
        public int? Status { get; set; }
        public int? Month { get; set; }
        public string? Achievements { get; set; }
        public string? Recommendations { get; set; }
        public string? SuggestedTraining { get; set; }
        public string? StrengthsPoints { get; set; }
        public string? WeaknessesPoints { get; set; }
        public int? Type { get; set; }

        /////////////////////////////////////////////
        public string? EvaDate { get; set; }
        public string? TemName { get; set; }
        public decimal? DegreeTotal { get; set; }



    }

    //  تقرير بالإستئذان
    public class HRRPPermissionsFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? Type { get; set; }
        public int? Reason { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        /////////////////////////////////////////////
        public string? PermissionType { get; set; }
        public string? PermissionDate { get; set; }
        public string? ExitTime { get; set; }
        public string? PerDetails { get; set; }
        public string? ReturnTime { get; set; }
        public string? ReasonName { get; set; }

        public string? ContactNumber { get; set; }
        public string? DetailsReason { get; set; }



    }


    // تقرير بالرواتب المعتمدة
    public class HRRPPayrollApprovedFilterDto
    {
        public int? FinancialYear { get; set; }
        public int? Month { get; set; }
        public int? PayrollType { get; set; }
        public string? PayrollNo { get; set; }
        public long? AppCode { get; set; }
        public long? MSCode { get; set; }

        /////////////////////////////////////////////
        public string? JCode { get; set; }
        public string? MSMonth { get; set; }
        public string? MSDate { get; set; }
        public string? StatusName { get; set; }
        public string? MSTitle { get; set; }
        public string? TypeName { get; set; }
        public decimal? TotalPayroll { get; set; }



    }

    //  تقرير بتغيرات حالة الموظف

    public class HRRPEmpStatusHistoryFilterDto
    {
        public string? empCode { get; set; }
        public string? EmpName { get; set; }

        public int? StatusId { get; set; }
        /////////////////////////////////////////////

        public string? Tdate { get; set; }
        public string? OldStatus { get; set; }
        public string? NewStatus { get; set; }
        public string? Reason { get; set; }
        public string? UserName { get; set; }

    }

    //  تقرير بالسلف المخصومة من الراتب
    public class HRReportLoanPaymentPayrollFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }
        public int? LocationId { get; set; }
        public int? MsMonth { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

        /////////////////////////////////////////////
        public string? BranchName { get; set; }
        public string? DepartmentName { get; set; }
        public string? LocationName { get; set; }
        public long? MsCode { get; set; }
        public string? MsDate { get; set; }
        public int? FinancialYear { get; set; }
        public decimal? AmountPaid { get; set; }

    }

    //  تقرير انتهاء الهوية للموظفين
    public class HREmpIDExpireReportFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }
        public int? LocationId { get; set; }
        public int? NationalityId { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

        /////////////////////////////////////////////
        public string? IdNo { get; set; }
        public string? IDExpireDate { get; set; }
        public int? RemainingDays { get; set; }


    }
    public class HREmpIDExpireUpdateDto
    {
        public List<string> EmpCode { get; set; } = null!;
        public int Duration { get; set; }

    }
    public class HRLoanInstallmentReportFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }
        public int? IsPaid { get; set; }

        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

        /////////////////////////////////////////////

        public long? LoanID { get; set; }
        public string? LoanDate { get; set; }
        public decimal? Amount { get; set; }
        public int? InstallmentNo { get; set; }
        public string? DueDate { get; set; }
        public decimal? Installment { get; set; }
        public bool? PaymentStatus { get; set; }



    }

    // تقرير إجمالي بالموظفين حسب الموقع
    public class HRRPByLocationFilterDto
    {

        public int? JobCatagoriesId { get; set; }
        public int? Status { get; set; }
        public int? NationalityId { get; set; }


        public int? Location { get; set; }

        public int? BranchId { get; set; }

        //////////////////////////////
        ///
        public int? EmployeeCount { get; set; }

        public string? LocationName { get; set; }




    }

    //--------------------------Begin Of StoredProcedure [dbo].[HR_RPEmployee_Sp] Dtos-------------------------------//
    [Keyless]
    public class EmployeeGosiDto
    {
        public string? ID_NO { get; set; }
        public decimal? Gosi_Salary { get; set; }
        public string? Gosi_Date { get; set; }
        public string? Gosi_No { get; set; }
        public string? Gois_Subscription_Expiry_Date { get; set; }
        public decimal? Gosi_Rate_Facility { get; set; }
        public long? Id { get; set; }             // maps to "id"
        public long? EmployeePrimaryId { get; set; }
        public string? Emp_ID { get; set; }
        public string? Emp_name { get; set; }
        public string? Emp_name2 { get; set; }
        public string? Location_Name { get; set; }
        public string? Location_Name2 { get; set; }
        public string? CostCenter_Code { get; set; }
        public string? CostCenter_Name { get; set; }
        public long? CC_ID { get; set; }
        public decimal? Gosi_Bisc_Salary { get; set; }
        public decimal? Gosi_House_Allowance { get; set; }
        public decimal? Gosi_Allowance_Commission { get; set; }
        public decimal? Gosi_Other_Allowances { get; set; }
        public string? Gosi_Name { get; set; }
        public string? Gosi_Name2 { get; set; }
        public decimal? Gosi_Salary_Facility { get; set; }
        public decimal? Gosi_Salary_Emp { get; set; }
    }



    //--------------------------End Of StoredProcedure [dbo].[HR_RPEmployee_Sp] Dtos-------------------------------//

    // تقرير الحضور والإنصراف الشهري
    public class HRAttendanceMonthly2FilterDto
    {
        public string? From { get; set; }
        public string? To { get; set; }
        public string? empCode { get; set; }
        public string? EmpName { get; set; }

        public int? BranchId { get; set; }
        public int? Location { get; set; }
        /// <summary>
        /// ///////////////////
        /// </summary>

        public List<AttendanceInfo>? Info { get; set; }
        public string? JobName { get; set; }


    }
    public class AttendanceInfo
    {
        public string? DayName { get; set; }
        public string? IsPreesent { get; set; }
    }

    //--------------------------Begin Of StoredProcedure [dbo].[HR_Attendance_TotalReportNew_SP] Dtos-------------------------------//
    [Keyless]
    public class HRAttendanceTotalReportNewSPDto
    {
        public long? ID { get; set; }
        public string? Emp_Name { get; set; }
        public string? Emp_Name2 { get; set; }
        public string? Location_Name { get; set; }
        public string? Location_Name2 { get; set; }
        public string? Dep_Name { get; set; }
        public string? Dep_Name2 { get; set; }
        public string? BRA_NAME { get; set; }
        public string? BRA_NAME2 { get; set; }
        public string? Emp_ID { get; set; }
        // Other properties from HR_Employee_VW

        public int? Attendances { get; set; }
        public int? Remote_Work_Days { get; set; }
        public int? Assignments_Days { get; set; }
        public int? BusinessTrip_Days { get; set; }
        public int? BusinessTripsDaysWithNoShift { get; set; }
        public int? Absence { get; set; }
        public int? WorkingDays { get; set; }
        public int? Vacation_days { get; set; }
        public int? Permissions { get; set; }
        public int? CheckInOut { get; set; }
        public string? Delay_in { get; set; }
        public string? Early_out { get; set; }
        public int? Compensation_in { get; set; }
        public string? Compensation { get; set; }
        public string? Work_hours { get; set; }
        public string? Shift_Hours { get; set; }
        public string? Contract_Type_Name { get; set; }

    }


    //--------------------------End Of StoredProcedure [dbo].[HR_Attendance_TotalReportNew_SP] Dtos-------------------------------//

    //--------------------------Begin Of StoredProcedure [dbo].[HR_Attendance_Report6_SP] Dtos-------------------------------//
    [Keyless]
    public class HRAttendanceReport6SP
    {
        public int Id { get; set; }
        public string? Att_Date { get; set; }
        public string? Day_Name { get; set; }
        public string? Day_Name2 { get; set; }
        public string? Time_In { get; set; }
        public DateTime? Def_Time_In { get; set; }
        public string? Time_Out { get; set; }
        public DateTime? Def_Time_Out { get; set; }
        public string? Attendance_Status { get; set; }
        public long? Delay_In { get; set; }
        public string? Delay_in_formatted { get; set; }
        public long? Early_Out { get; set; }
        public string? Early_out_formatted { get; set; }
        public string? Delay_in_Early_out_formatted { get; set; }
        //public string? ContractTypeName { get; set; }
        //public string? ContractTypeName2 { get; set; }
        public long? Over_Time { get; set; }
        public long? Work_Hours { get; set; }
        public string? Work_hoursFormatted { get; set; }
        public string? Note { get; set; }
        public string? Shift_Name { get; set; }
        public long? Shift_Hours { get; set; }
        public string? Shift_HoursFormatted { get; set; }
        public int? @Type_ID { get; set; }
        public string? Emp_Code { get; set; }
        public string? Emp_Name { get; set; }
        public string? Emp_Name2 { get; set; }
        public string? Dept_Name { get; set; }
        public string? Branch_Name { get; set; }
        public string? Location_Name { get; set; }
        public string? Location_Name2 { get; set; }
        public string? Position { get; set; }
        public string? Check_Attendance { get; set; }
        public string? @Check_Permissions { get; set; }
        public long? Compensation_In { get; set; }
        public long? Compensation_Out { get; set; }
        public string? CompensationFormatted { get; set; }
        public string? Extra_Hours { get; set; }
        public string? Facility_Name { get; set; }
        public string? Facility_Name2 { get; set; }
        public string? DOAppointment { get; set; }
        public string? MapLocation { get; set; }
        public string? Attendance_Type_Name { get; set; }
        public string? Attendance_Type_Name2 { get; set; }
        public string? TimeTable_Name { get; set; }
    }

    public class HRAttendanceReport6FilterSP
    {
        public int? Location { get; set; }

        public int? BranchId { get; set; }
        public string? BranchsId { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? EmpName { get; set; }
        public string? EmpCode { get; set; }
        public int? DeptId { get; set; }
        public int? AttendanceType { get; set; }
        public int? Status { get; set; }
        public int? Sponsors { get; set; }
        public int? Workinghours { get; set; }
        public int? Facility { get; set; }
        public int? ShitId { get; set; }
    }

    //--------------------------End Of StoredProcedure [dbo].[HR_Attendance_Report6_SP] Dtos-------------------------------//

    //---Begin--------------------------تقرير رواتب الموظفين تفصيلي---------------------------
    public class HrRepStaffSalariesDetailedFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }
        public int? FacilityId { get; set; }
        public int? NationalityId { get; set; }
        public int? Location { get; set; }
        public int? DeptId { get; set; }
        public int? StatusId { get; set; }
        public int? ContractTypeID { get; set; }

        public int? WagesProtection { get; set; }

        public decimal? Salary { get; set; }
        public decimal? TotalSalary { get; set; }
        public string? BranchName { get; set; }
        public string? DepartmentName { get; set; }
        public string? LocationName { get; set; }
        public string? NationalityName { get; set; }
        public string? FacilityName { get; set; }
        public string? DoAppointment { get; set; }

        // Declare Allowance and Deduction as lists
        public List<AllowanceItem>? Allowances { get; set; } = new List<AllowanceItem>();
        public List<DeductionItem>? Deductions { get; set; } = new List<DeductionItem>();
    }

    // Allowance item
    public class AllowanceItem
    {
        public string? Name { get; set; }
        public decimal? Amount { get; set; }
    }

    // Deduction item
    public class DeductionItem
    {
        public string? Name { get; set; }
        public decimal? Amount { get; set; }
    }

    //---End--------------------------تقرير رواتب الموظفين تفصيلي---------------------------
    //---Begin--------------------------تقرير اجمالي بالتقييمات---------------------------

    public class HRKpiTotalFilterDto
    {

        public string? EmpName { get; set; }
        public string? EmpCode { get; set; }
        public int? DeptId { get; set; }
        public int? BranchId { get; set; }
        public int? Location { get; set; }
        public int? PerformanceId { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }


    //---End--------------------------تقرير اجمالي بالتقييمات---------------------------


    //---Begin--------------------------استعلام عن تقييم الأداء---------------------------

    public class HRKpiQueryFilterDto
    {

        public string? EmpName { get; set; }
        public string? EmpCode { get; set; }
        public int? DeptId { get; set; }
        public int? BranchId { get; set; }
        public int? Location { get; set; }
        //  طلب التقييم
        public int? PerformanceId { get; set; }

        public string? FinancialYear { get; set; }
        public int? Month { get; set; }
        //  حالة التقييم
        public int? EvaluationStatus { get; set; }
        //  الحالة
        public int? Status { get; set; }
        public int? ExcludingProbationaryEmployees { get; set; }
    }


    //---End--------------------------استعلام عن تقييم الأداء---------------------------


    public class HrRPDefinitionSalaryFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? Sample { get; set; }
        public int? SendTo { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        /////////////////////////////////////////////

        public string? DepartmentName { get; set; }
        public string? LocationName { get; set; }
        public string? Date { get; set; }
        public string? SampleName { get; set; }
        public string? FileURL { get; set; }
        public string? SendToName { get; set; }

    }

    //--------------------------Begin Of StoredProcedure [dbo].[HR_Emp_Clearance_Sp] Dtos-------------------------------//
    [Keyless]
    public class HREmpClearanceSpDto
    {
        public long? ID { get; set; } // id of employee
        public string? Emp_name { get; set; }
        public string? Doappointment { get; set; }
        public string? IBAN { get; set; }
        public decimal? Salary { get; set; }
        public string? ID_No { get; set; }
        public string? Nationality_Name { get; set; }
        public string? BRA_NAME { get; set; }
        public string? Bank_Name { get; set; }
        public int? Catagories_ID { get; set; }
        public string? Location_Name { get; set; }
        public string? Dep_Name { get; set; }
        public int? Job_Catagories_ID { get; set; }

        public string? Emp_ID { get; set; }
        public string? Cat_name2 { get; set; }
        public string? Cat_name { get; set; }
        public int? BRANCH_ID { get; set; }
        public int? Location { get; set; }
        public int? Vacation_Days_Year { get; set; }
        public int? Dept_ID { get; set; }
        public string? Account_No { get; set; }

        //////////////////////////////////////////////////
        public decimal? VacationBalance { get; set; }
        public int? Vacation_Type_Id { get; set; } = 0;
        public string? Last_Vacation_Date { get; set; }
        public string? Last_Vacation_EDate { get; set; }
        public string? Vacation_Account_Day { get; set; }
        public string? Vacation_Type_Name { get; set; }
        public string? Last_Salary_Date { get; set; }
        public decimal? Salary_C { get; set; }
        public decimal? Housing_C { get; set; }
        public decimal? Allowance_C { get; set; }
        public decimal? Loan { get; set; }
        public decimal? DayCost4Clearnce { get; set; }
        public decimal? VacationBalanceAmount { get; set; }
        public int? Ded_Housing { get; set; } = 0;
        public int? DedOhad { get; set; } = 0;
        public decimal? Delay { get; set; }
        public decimal? Delay_Cnt { get; set; }
        public decimal? Absence { get; set; }
        public decimal? Absence_Cnt { get; set; }
        public decimal? Penalties { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Housing { get; set; }
        public decimal? Deduction { get; set; }
        public decimal? Deduction_tmp { get; set; }
        public decimal? Gosi { get; set; }
        public decimal? Tick_Due_Cnt { get; set; }
        public decimal? Tick_Due_Amount { get; set; }
        public decimal? Tick_Due_Total { get; set; }
        public int? Count_Day_Work { get; set; }
        public decimal? Daily_Working_hours { get; set; }
    }



    //--------------------------End Of StoredProcedure [dbo].[HR_Emp_Clearance_Sp] Dtos-------------------------------//

    //ملف الموظف
    public class HrEmployeeFileFilterDto
    {
        public int? BranchId { get; set; }
        public string? BranchName { get; set; }
        public string? EmpName { get; set; }
        public string? EmpCode { get; set; }
        public int? JobType { get; set; }
        public int? JobCatagoriesId { get; set; }
        public int? Status { get; set; }
        public int? NationalityId { get; set; }
        public int? DeptId { get; set; }
        public string? IdNo { get; set; }
        public string? PassId { get; set; }
        public string? EntryNo { get; set; }
        public int? Location { get; set; }
        public string? LocationName { get; set; }
        public string? Endofcontract { get; set; }
        public int? SponsorsID { get; set; }
        public long? Id { get; set; }
        /////////////////////////
        ///
        public string? DeptName { get; set; }
        public string? EmpName2 { get; set; }
        public string? CatName { get; set; }
        public string? StatusName { get; set; }

    }
    //تقرير تجمعي بالتاخرات خلال فترة 
    public class HrDelayTotalFilterDto
    {
        public int? DeptId { get; set; }
        public int? Location { get; set; }
        public int? BranchId { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
    }
    //--------------------------  بداية تقرير حضور وانصراف يومي-------------------------------//
    public class AttendanceSummaryFilter
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }
        public string? DayDateGregorian { get; set; }
        public string? DayDateGregorian2 { get; set; }
        public int? TimeTableId { get; set; }
        public int? ManagerId { get; set; }
        public int? StatusId { get; set; }
        public int? Location { get; set; }
        public int? DeptId { get; set; }
        public int? AttendanceType { get; set; }
        public int? FingerprintType { get; set; }
        public int HoursReadings { get; set; }
        public int AttendanceHours { get; set; }
        public int DelayMethod { get; set; }
    }
    public class AttendanceSummaryDto
    {
        public double TotalTime { get; set; }
        public double TotalTimeWorkShift { get; set; }
        public string? DayName { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? DayDateGregorian { get; set; }
        public string? AttendanceTypeName { get; set; }
        public string? TimeTableName { get; set; }
        public string? LocationName { get; set; }
        public string? BranchName { get; set; }
        public string? TimeIn { get; set; }
        public string? TimeOut { get; set; }
        public string? DefTimeIn { get; set; }
        public string? DefTimeOut { get; set; }
        //  الإجمالي
        public string? TotalTimeFormatted { get; set; }
        // ساعات العمل
        public string? WorkHoursFormatted { get; set; }

        //  التقصير الغير مسجل
        public string? UnregisteredDefault { get; set; }
        public decimal? DailyWorkingHours { get; set; }
        // 	التأخرات السابقة
        public string? TotalDelay { get; set; }
        // 	الاستئذانات
        public string? TotalPermission { get; set; }
    }

    // اضافة اعتماد

    public class AddDelayDto
    {
        public string? EmpCode { get; set; }
        public string? DayDateGregorian { get; set; }
        public string? UnregisteredDefault { get; set; }

    }

    //--------------------------  نهاية تقرير حضور وانصراف يومي -------------------------------//
    public class HrDashboardDto
    {
        public decimal? Cnt { get; set; }
        public string? Name { get; set; }
        public string? Name2 { get; set; }
        public string? Color { get; set; }
        public string? Icon { get; set; }
        public string? Url { get; set; }
        public int? BranchId { get; set; }
        public int? LocationId { get; set; }
        public int? DeptId { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? FinancelYear { get; set; }
    }
    public class HrDashboard2ResultDto
    {
        public List<HrDashboardDto>? locationResults { get; set; }
        public List<HrDashboardDto>? monthResults { get; set; }
        public List<HrDashboardDto>? allowanceData { get; set; }
        public List<HrDashboardDto>? DeductionData { get; set; }

    }

    // تقرير بعهدة موظف
    public class HRRPOhadFilterDto
    {
        public long? OhadId { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? Branch { get; set; }
        public string? OhdaDate { get; set; }
        public long? ItemNo { get; set; }
        public string? ItemName { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? StatusName { get; set; }
        public string? Note { get; set; }
    }

    public class HRAttendanceTotalReportSPFilterDto
    {
        public long? facilityId { get; set; }
        public string? facilityName { get; set; }
        public long? BranchID { get; set; }
        public string? BranchsId { get; set; }
        public long? DeptID { get; set; }
        public long? Location { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }
    [Keyless]
    public class HRAttendanceTotalReportSPDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? DeptName { get; set; }
        public string? LocationName { get; set; }
        public long? Absence { get; set; }
        public string? Delay2 { get; set; }
        public string? H_OverTime { get; set; }
    }


    public class HRApprovalAbsencesReportFilterDto
    {
        public long? facilityId { get; set; }
        public string? facilityName { get; set; }
        public long? BranchID { get; set; }
        public string? BranchsId { get; set; }
        public long? DeptID { get; set; }
        public long? Location { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public long? NationalityType { get; set; }
        public long? GroupShiftId { get; set; }
    }
    public class HRApprovalAbsencesReportDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? DeptName { get; set; }
        public string? LocationName { get; set; }
        public long? Absence { get; set; }
        public string? CatName { get; set; }
        public string? NationalityName { get; set; }
        public string? MonthDay { get; set; }
        public long? Attendances { get; set; }
        public long? OffDays { get; set; }
        public long? VacationDays { get; set; }
        public long? AbsenceNotRecorded { get; set; }
    }
}
