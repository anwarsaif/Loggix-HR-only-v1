
using System.ComponentModel.DataAnnotations;
using Logix.Application.DTOs.Main;
using Logix.Domain.Hr;
using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;

namespace Logix.Application.DTOs.HR
{
    public class HrPayrollDto
    {
        public long MsId { get; set; }
        public long? MsCode { get; set; }
        public string? MsDate { get; set; }
        public string? MsTitle { get; set; }
        public string? MsMonth { get; set; }
        public string? MsMothTxt { get; set; }
        public int? FinancelYear { get; set; }
        public int? State { get; set; }
        public string? AuditBy { get; set; }
        public DateTime? AuditOn { get; set; }
        public string? ApproveBy { get; set; }
        public DateTime? ApproveOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public int? FacilityId { get; set; }
        public int? PayrollTypeId { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? PaymentDate { get; set; }
        public string? DueDate { get; set; }
        public long? AppId { get; set; }
        public long? BranchId { get; set; }
        public bool? Posted { get; set; }
    }

    public class HrPayrollEditDto
    {
        public long MsId { get; set; }
        public string? MsTitle { get; set; }
        public string? PaymentDate { get; set; }
        public string? DueDate { get; set; }
        public int? FacilityId { get; set; }
        public int? FinancelYear { get; set; }
        public string? MsMonth { get; set; }
        public long? ApplicationCode { get; set; }
        public string? ApplicationDate { get; set; }

        public List<SaveFileDto>? fileDtos { get; set; }
        public List<HrPayrollDVw>? payrollDVw { get; set; }
        public List<HrPayrollNoteVw>? notes { get; set; }
    }



    public partial class HrPayrollFilterDto
    {
        public long MsId { get; set; }
        public long? MsCode { get; set; }
        public string? MsDate { get; set; }
        public string? MsTitle { get; set; }
        public string? MsMonth { get; set; }
        public string? MsMonthName { get; set; }
        public string? StatusName { get; set; }
        public int? FinancelYear { get; set; }
        public int? PayrollTypeId { get; set; }
        public string? TypeName { get; set; }
        public string? TypeName2 { get; set; }
        public string? StatusName2 { get; set; }
        public long? ApplicationCode { get; set; }
        public long? FacilityId { get; set; }
        public long? BranchId { get; set; }
        public string? BranchsId { get; set; }
        public int? Status { get; set; }

    }

    public partial class HrPayrollFilter2Dto
    {
        //[Required]
        public long? MsId { get; set; }
        public long? MsCode { get; set; }
        public string? MsDate { get; set; }
        public string? MsTitle { get; set; }
        public string? MsMonth { get; set; }
        public string? CostCenterCode { get; set; }
        public string? StatusName { get; set; }
        public int? FinancelYear { get; set; }
        public int? PayrollTypeId { get; set; }
        public string? TypeName { get; set; }
        public string? TypeName2 { get; set; }
        public string? StatusName2 { get; set; }
        public long? AppId { get; set; }
        public long? Id { get; set; }

        public int? PaymentTypeId { get; set; }
        public long? SalaryGroupId { get; set; }
        public long? BranchId { get; set; }
        public long? Location { get; set; }
        public long? FacilityId { get; set; }
        public long? DeptId { get; set; }
        public int? NationalityId { get; set; }
        public int? SponsorsId { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? WagesProtection { get; set; }





    }

    public partial class HrPayrollFilter2ResultDto
    {
        public long? ID { get; set; }

        public int? Attendance { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Deduction { get; set; }
        public decimal? TotalDeductions { get; set; }
        public decimal? Absence { get; set; }
        public decimal? Delay { get; set; }
        public decimal? Loan { get; set; }
        public decimal? Net { get; set; }

        public string? Emp_Name { get; set; }
        public string? EmpCode { get; set; }
        public string? Emp_ID { get; set; }
        public decimal? Commission { get; set; }
        public decimal? OverTime { get; set; }
        public decimal? Mandate { get; set; }
        public decimal? H_OverTime { get; set; }
        public decimal? Penalties { get; set; }
        public decimal? Salary { get; set; }
        public decimal? BasicSalary { get; set; }
        public int? WagesProtection { get; set; }
        public string? WagesProtectionName { get; set; }

        public string? Dep_Name { get; set; }
        public string? Location_Name { get; set; }
        public string? BraName { get; set; }
        public string? CostCenterCode { get; set; }
        public decimal? Total { get; set; }
        public decimal? AllowanceOrignal { get; set; }
        public decimal? GosiDeduction { get; set; }
        public string? Account_No { get; set; }
        public string? Bank_Name { get; set; }
        public int? Bank_ID { get; set; }

        public int? Cnt_Absence { get; set; }

        public string? Note { get; set; }
    }

    [Keyless]
    public class HRPayrollCreate2SpDto
    {
        public long ID { get; set; }
        public string? Emp_ID { get; set; }
        //public string? Emp_Code { get; set; }
        public string? Emp_Name { get; set; }
        public int? Bank_ID { get; set; }
        public string? Bank_Name { get; set; }
        public string? Account_No { get; set; }
        public decimal? BasicSalary { get; set; }
        public int? Attendance { get; set; }  // Count_Day_Work
        public int? Cnt_Absence { get; set; } // Day_Absence
        public decimal? Absence { get; set; }
        public decimal? Delay { get; set; }
        public decimal? Commission { get; set; }
        public decimal? Penalties { get; set; }
        public decimal? OverTime { get; set; } // Extra_time
        public int? Mandate { get; set; }  // Always 0 in this query
        public decimal? H_OverTime { get; set; } // H_Extra_time
        public decimal? Loan { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Deduction { get; set; }
        public string? Location_Name { get; set; }
        public string? Location_Name2 { get; set; }
        public string? Dep_Name { get; set; }
        public string? Dep_Name2 { get; set; }

        public string? Emp_Name2 { get; set; }
        public decimal? Daily_Working_hours { get; set; }

    }

    public class HRPayrollCreate2SpFilterDto
    {
        public string MSMonth { get; set; }
        public string FinancelYear { get; set; }
        public int? BRANCHID { get; set; }
        public int? DeptID { get; set; }
        public int? Location { get; set; }
        public int? FacilityID { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? NationalityID { get; set; }
    }

    public class HrPayrollAddDto
    {
        public List<HRPayrollCreate2SpDto> SpDtos { get; set; }
        //public long? MsCode { get; set; }
        public string? MsDate { get; set; }
        public string? MsTitle { get; set; }
        [Required]
        public string MsMonth { get; set; } = null!;

        [Required]
        public int? FinancelYear { get; set; } = null!;
        public int? State { get; set; }
        //public string? AuditBy { get; set; }
        //public DateTime? AuditOn { get; set; }
        //public string? ApproveBy { get; set; }
        //public DateTime? ApproveOn { get; set; }
        public int? FacilityId { get; set; }
        public int? PayrollTypeId { get; set; }
        //public string? StartDate { get; set; }
        //public string? EndDate { get; set; }
        //public string? PaymentDate { get; set; }
        //public string? DueDate { get; set; }
        //public long? AppId { get; set; }
        public long? BranchId { get; set; }
        //public bool? Posted { get; set; }
        public int? AppTypeId { get; set; } = 0;
        public bool IsForAll { get; set; } = false;
    }

    public class HRPayrollCreateSpFilterDto
    {
        [Required]
        public string MSMonth { get; set; } = null!;
        [Required]
        public string FinancelYear { get; set; } = null!;
        public int? BRANCHID { get; set; }
        public int? DeptID { get; set; }
        public int? Location { get; set; }
        public int? FacilityID { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? NationalityID { get; set; }
        public int? WagesProtection { get; set; }
        public int? SponsorsID { get; set; }
        public string? ContractTypeID { get; set; }
        public int? PaymentTypeID { get; set; }
        public int? SalaryGroupID { get; set; }
        public string? Branches { get; set; }

    }
    public class HrCommissionPayrollAddDto
    {
        public List<HrCommissionPayrollDetailsDto> DetailsDto { get; set; }
        public int? AppTypeId { get; set; } = 0;
        public int? BRANCHID { get; set; }
        public int? DeptID { get; set; }
        public int? Location { get; set; }
        public int? FacilityID { get; set; }
        [Required]
        public int? FinancelYear { get; set; } = null!;
        [Required]
        public string MsMonth { get; set; } = null!;

    }
    public class HrCommissionPayrollDetailsDto
    {
        public decimal? Commission { get; set; }
        public string? AccountNo { get; set; }
        public int? BankId { get; set; }
        public long? EmpId { get; set; }
    }


    public class HrPaymentDueAddDto
    {
        public List<HrPaymentDueDetailsDto> DetailsDto { get; set; }
        public List<SaveFileDto> fileDtos { get; set; }
        public int? PayrolllTypeId { get; set; }
        public int? AppTypeId { get; set; } = 0;
        [Required]
        public string MSMothTxt { get; set; } = null!;
        [Required]
        public string MSTitle { get; set; } = null!;
        [Required]
        public string MSDate { get; set; } = null!;
        public int? State { get; set; }
    }
    public class HrPaymentDueDetailsDto
    {
        public decimal? Amount { get; set; }
        public string? AccountNo { get; set; }
        public int? BankId { get; set; }
        public string? EmpCode { get; set; }
        public long? Id { get; set; }
    }

    public partial class HrPayrollApproveFilterResultDto
    {
        public long MsId { get; set; }
        public long? MsCode { get; set; }
        public string? MsDate { get; set; }
        public string? MsTitle { get; set; }
        public string? MsMonth { get; set; }
        public string? StatusName { get; set; }
        public int? FinancelYear { get; set; }
        public int? PayrollTypeId { get; set; }
        public string? TypeName { get; set; }
        public long? ApplicationCode { get; set; }
        public long? BranchId { get; set; }
        public int? Status { get; set; }
        // from ACC_Journal_Master Table 
        public string? JCode { get; set; }
        public string? JDateGregorian { get; set; }



    }
    public class HrPayrollApprovedDto
    {
        public long MSId { get; set; }
        public string? MSMonth { get; set; }
        public string? TxtJCode { get; set; }
        public int? FinancelYear { get; set; }
        public long? BRANCHID { get; set; }
        public int? DeptID { get; set; }
        public int? Location { get; set; }
        public int? FacilityID { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? NationalityID { get; set; }
        public int? WagesProtection { get; set; }
        public int? SponsorsID { get; set; }
        public int? ContractTypeID { get; set; }
        public int? PaymentTypeID { get; set; }
        public int? SalaryGroupID { get; set; }
        public string? Branches { get; set; }
        public string? MsTitle { get; set; }
        public string? StatusName { get; set; }
        public int? PayrollTypeId { get; set; }
        public string? TypeName { get; set; }
        public string? TypeName2 { get; set; }
        public string? StatusName2 { get; set; }
        public long? AppId { get; set; }
        public List<HrPayrollDVw>? hrPayrollDVws { get; set; }
        public List<SysFileDto>? files { get; set; }
    }

    public class HrPayrollApprovedFilterAndAddDto
    {
        public string? MSMonth { get; set; }
        public string? MSMonthText { get; set; }
        public int? FinancelYear { get; set; }
        public long? BRANCHID { get; set; }
        public int? DeptID { get; set; }
        public int? Location { get; set; }
        public int? FacilityID { get; set; }
        public int? SponsorsID { get; set; }
        public long MSId { get; set; }
        public string? OperationDate { get; set; }
        public string? TxtJCode { get; set; }


    }

    public partial class HrPayrollQueryFilterDto
    {

        public string? MsMonth { get; set; }
        public int? FinancelYear { get; set; }
        public string? EmpName { get; set; }
        public string? EmpCode { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? PaymentTypeId { get; set; }
        public int? PayrollTypeId { get; set; }
        public long? DeptId { get; set; }
        public long? FacilityId { get; set; }
        public long? Location { get; set; }
        public int? JobId { get; set; }
        public int? JobCatagoriesId { get; set; }


        ////////////////////////////////////////////////////////

    }
    public class HrUnpaidEmployeesVM
    {
        public string? EmpId { get; set; }
        public string? EmpName { get; set; }
        public string? EmpName2 { get; set; }
        public string? DepName { get; set; }
        public string? BraName { get; set; }
        public string? LocationName { get; set; }
        public string? CatName { get; set; }
        public string? StatusName { get; set; }
        public string? IdNo { get; set; }
        public string? EmpPhoto { get; set; }
        public string? Doappointment { get; set; }
        public string? ContractExpiryDate { get; set; }
    }

    public class HrUnpaidEmployeesFilter
    {
        public int FinancelYear { get; set; }
        public string MsMonth { get; set; } = null;
        public string? EmpId { get; set; }

        public string? EmpName { get; set; }
        public int? JobCatagoriesId { get; set; }
        public int? StatusId { get; set; }
        public int? NationalityId { get; set; }
        public int? DeptId { get; set; }
        public string? IdNo { get; set; }
        public int? Location { get; set; }
        public int? BranchId { get; set; }
        public int? SponsorId { get; set; }
        public int? FacilityId { get; set; }
        public int? ContractType { get; set; }
        public int? WagesProtection { get; set; }
        public string? EmpCode2 { get; set; }
        public int? BloodType { get; set; }
        public string? DirectPhoneNumber { get; set; }
    }


    public partial class HrPayrollCompareFilterDto
    {
        public string? CurrentMonth { get; set; }
        public string? PreviousMonth { get; set; }
        public string? BranchIds { get; set; }
        public int? FinancialYear { get; set; }
        public int? BranchId { get; set; }
        public int? DeptId { get; set; }
        public int? PayrollType { get; set; }
        public int? Location { get; set; }


    }

    public class HrPayrollCompareResult
    {
        public string? DepName { get; set; }
        public string? LocationName { get; set; }
        public int FinancelYear { get; set; }
        public int PervMonth { get; set; }
        public int CurMonth { get; set; }
        public int Difference { get; set; }
        public string? empCode { get; set; }
        public string? empName { get; set; }
        public string? MsMonth { get; set; }
        public long? EmpId { get; set; }
        public decimal? PreviousMonthNet { get; set; }
        public decimal? PresentMonthNet { get; set; }
        public int? CntEmp { get; set; }
        public decimal? TotalBasicSalary { get; set; }
        public decimal? TotalFixedAllowance { get; set; }
        public decimal? TotalOtherAllowance { get; set; }
        public decimal? TotalOverTime { get; set; }
        public decimal? TotalSalary { get; set; }
        public decimal? TotalLoan { get; set; }
        public decimal? TotalOtherDeduction { get; set; }
        public decimal? TotalNet { get; set; }
        public long? BranchId { get; set; }
        public string? BranchName { get; set; }
        public string? MonthName { get; set; }

    }
    public partial class HrShowWizardFilterDto
    {
        public string? MsMonth { get; set; }
        public int? FinancelYear { get; set; }
        public int? DeptId { get; set; }
        public int? Location { get; set; }


    }

    [Keyless]
    public class HRPayrollCreateAdvancedSpDto
    {
        public string? Dep_Name2 { get; set; }
        public string? Location_Name2 { get; set; }
        public string? Dep_Name { get; set; }
        public string? Location_Name { get; set; }
        public long? ID { get; set; }
        public string? Emp_ID { get; set; }
        public string? Emp_name { get; set; }
        public string? Emp_name2 { get; set; }
        public int? Bank_ID { get; set; }
        public string? Bank_Name { get; set; }
        public string? Account_No { get; set; }
        public int? Attendance { get; set; }
        public int? Cnt_Absence { get; set; }
        public int? Cnt_Delay { get; set; }
        public decimal? Absence { get; set; }
        public decimal? Delay { get; set; }
        public decimal? DelayHourByDay { get; set; }
        public decimal? Loan { get; set; }
        public decimal? OverTime { get; set; }
        public decimal? H_OverTime { get; set; } // Assuming H_OverTime is a decimal for hour-based calculations
        public decimal? Commission { get; set; }
        public int? Mandate { get; set; } = 0;
        public decimal? Penalties { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Deduction { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? AdvanceDeduction { get; set; }
        public decimal? SocialInsurance { get; set; }
        public decimal? TaxDeduction { get; set; }
        public string? BRA_NAME { get; set; }
        public string? BRA_NAME2 { get; set; }
        public string? Facility_Name { get; set; }
        public string? Facility_Name2 { get; set; }
        public string? ID_No { get; set; }
        public string? DOAppointment { get; set; } // Assuming it can be nullable based on your logic
        public string? Cat_name { get; set; }
        public string? Cat_name2 { get; set; }
        public string? Att_Start_date { get; set; }
        public string? Att_End_date { get; set; }
        public bool UseIncomeTaxCalc { get; set; } // Assuming UseIncomeTaxCalc is a boolean
        public int? TaxId { get; set; }
        public string? TaxCode { get; set; }
    }
    public class HrPayrollAdvancedAddDto
    {
        public List<HRPayrollAdvancedResultDto> SpDtos { get; set; }
        public string? MsDate { get; set; }
        public string? MsTitle { get; set; }
        [Required]
        public string MsMonth { get; set; } = null!;
        [Required]
        public int? FinancelYear { get; set; } = null!;
        public int? State { get; set; }

        public int? FacilityId { get; set; }
        public int? PayrollTypeId { get; set; }

        public long? BranchId { get; set; }
        public int? AppTypeId { get; set; } = 0;

    }

    public class TaxSlideDto
    {
        public int? Id { get; set; }
        public int? TaxSlideOrderNo { get; set; }
        public decimal? TaxSlideValue { get; set; }
        public decimal? TaxSlideRate { get; set; }
        public string? TaxSlideStartingFromTheSlideNo { get; set; }
        public string? TaxSlideNote { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? PersonalExemption { get; set; }
        public int? IncomeTaxId { get; set; }
        public string? TaxCode { get; set; }
        public string? TaxName { get; set; }
        public string? TaxName2 { get; set; }
    }


    public class HRPayrollAdvancedResultDto
    {
        public string? Dep_Name2 { get; set; }
        public string? Location_Name2 { get; set; }
        public string? Dep_Name { get; set; }
        public string? Location_Name { get; set; }
        public long? ID { get; set; }
        public string? Emp_ID { get; set; }
        public string? Emp_name { get; set; }
        public string? Emp_name2 { get; set; }
        public int? Bank_ID { get; set; }
        public string? Bank_Name { get; set; }
        public string? Account_No { get; set; }
        public int? Attendance { get; set; }
        public int? Cnt_Absence { get; set; }
        public int? Cnt_Delay { get; set; }
        public decimal? Absence { get; set; }
        public decimal? Delay { get; set; }
        public decimal? DelayHourByDay { get; set; }
        public decimal? Loan { get; set; }
        public decimal? OverTime { get; set; }
        public decimal? H_OverTime { get; set; } // Assuming H_OverTime is a decimal for hour-based calculations
        public decimal? Commission { get; set; }
        public int? Mandate { get; set; } = 0;
        public decimal? Penalties { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Deduction { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? AdvanceDeduction { get; set; }
        public decimal? SocialInsurance { get; set; }
        public decimal? TaxDeduction { get; set; }
        public string? BRA_NAME { get; set; }
        public string? BRA_NAME2 { get; set; }
        public string? Facility_Name { get; set; }
        public string? Facility_Name2 { get; set; }
        public string? ID_No { get; set; }
        public string? DOAppointment { get; set; } // Assuming it can be nullable based on your logic
        public string? Cat_name { get; set; }
        public string? Cat_name2 { get; set; }
        public string? Att_Start_date { get; set; }
        public string? Att_End_date { get; set; }
        public bool UseIncomeTaxCalc { get; set; } // Assuming UseIncomeTaxCalc is a boolean
        public int? TaxId { get; set; }
        public string? TaxCode { get; set; }
        public decimal? IncomeTax { get; set; }
        public decimal? Total { get; set; }
        public decimal? NetAfterTax { get; set; }
        public decimal? NetBeforTax { get; set; }
        public decimal? TotalDeductions { get; set; }

    }
    [Keyless]
    public class HRPayrollManuallCreateSpDto
    {
        public long ID { get; set; }
        public long? Emp_ID { get; set; }
        public string? Emp_Code { get; set; }
        public string? Emp_Name { get; set; }
        public int? Bank_ID { get; set; }
        public string? Bank_Name { get; set; }
        public string? Account_No { get; set; }
        public decimal? BasicSalary { get; set; }
        public int? Attendance { get; set; }  // Count_Day_Work
        public int? Cnt_Absence { get; set; } // Day_Absence
        public decimal? Absence { get; set; }
        public decimal? Delay { get; set; }
        public decimal? Commission { get; set; }
        public decimal? Penalties { get; set; }
        public decimal? OverTime { get; set; } // Extra_time
        public int? Mandate { get; set; }  // Always 0 in this query
        public decimal? H_OverTime { get; set; } // H_Extra_time
        public decimal? Loan { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Deduction { get; set; }

    }



    public class PayrollDueSearchInAddDto
    {
        public string? MsDate { get; set; }
        public string? MsTitle { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

    }
    public class HrOverTimeGroupedDto
    {
        public long? IdM { get; set; }
        public decimal? HoursSum { get; set; }
        public decimal? TotalSum { get; set; }
    }



    #region مسير خارج دوام

    public class PayrollOverTimeDetailsDto
    {
        public decimal? Cnt { get; set; }
        public decimal? Net { get; set; }
        public string? AccountNo { get; set; }
        public int? BankId { get; set; }
        public string? EmpCode { get; set; }
        public long? Id { get; set; }
    }
    public class PayrollOverTimeAddDto
    {
        public List<PayrollOverTimeDetailsDto> DetailsDto { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }
        public int? PayrolllTypeId { get; set; }
        public int? AppTypeId { get; set; } = 0;
        [Required]
        public string MSMothTxt { get; set; } = null!;
        [Required]
        public string MSTitle { get; set; } = null!;
        [Required]
        public string MSDate { get; set; } = null!;
        public int? State { get; set; }
    }

    #endregion

    #region مسير بدل سكن

    public class PayrollHousingAllowancesDetailsDto
    {
        public decimal? Net { get; set; }
        public string? EmpCode { get; set; }
    }
    public class PayrollHousingAllowancesAddDto
    {
        public List<PayrollHousingAllowancesDetailsDto> DetailsDto { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }
        public int? PayrolllTypeId { get; set; }
        public int? AppTypeId { get; set; } = 0;
        [Required]
        public string MSTitle { get; set; } = null!;
        [Required]
        public string MSDate { get; set; } = null!;
        public int? State { get; set; }
    }

    #endregion



    #region مسير انتداب

    public class PayrollMandateAddDto
    {
        public List<long> DetailsDto { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }
        public int? PayrolllTypeId { get; set; }
        public int? AppTypeId { get; set; } = 0;
        [Required]
        public string MSTitle { get; set; } = null!;
        [Required]
        public string MSDate { get; set; } = null!;
        public int? State { get; set; }
    }

    #endregion



    #region  مسير تذاكر مستحقة

    public class PayrollTicketAllowancesDetailsDto
    {
        public decimal Net { get; set; }
        public string EmpCode { get; set; }
    }
    public class PayrollTicketAllowancesAddDto
    {
        public List<PayrollTicketAllowancesDetailsDto> DetailsDto { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }
        public int? PayrolllTypeId { get; set; }
        public int? AppTypeId { get; set; } = 0;
        [Required]
        public string MSTitle { get; set; } = null!;
        [Required]
        public string MSDate { get; set; } = null!;
        public int? State { get; set; }
    }

    #endregion


    #region   خارج دوام يدوي

    public class PayrollOverTime2DetailsDto
    {
        public decimal Net { get; set; }
        public int WorkDaysCount { get; set; }
        public decimal HOverTime { get; set; }
        public string EmpCode { get; set; }
    }
    public class PayrollOverTime2AddDto
    {
        public List<PayrollOverTime2DetailsDto> DetailsDto { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }
        public int? PayrolllTypeId { get; set; }
        public int? AppTypeId { get; set; } = 0;
        [Required]
        public string MSTitle { get; set; } = null!;
        [Required]
        public string MSDate { get; set; } = null!;
        public int? State { get; set; }
    }

    #endregion



    #region  مسير دوام مرن

    public class PayrollFlexibleWorkingAddDto
    {
        public List<long> DetailsDto { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }
        public int? PayrolllTypeId { get; set; }
        public int? AppTypeId { get; set; } = 0;
        [Required]
        public string MSTitle { get; set; } = null!;
        [Required]
        public string MSDate { get; set; } = null!;
        public int? State { get; set; }
    }

    #endregion


    public class ApproveRejectPayrollDto
    {
        [Required]
        public long MsId { get; set; }
        public string? Note { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }
    }

    public class AllowanceDeductionCheckResult
    {
        public bool IsValid { get; set; } = true;
        public string AllowanceMismatch { get; set; } = "";
        public string DeductionMismatch { get; set; } = "";
    }
}
