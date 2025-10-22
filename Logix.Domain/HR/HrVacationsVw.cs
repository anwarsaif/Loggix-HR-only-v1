using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrVacationsVw
    {
        [Column("Vacation_ID")]
        public long VacationId { get; set; }

        [Column("Vacation_Type_Name")]
        [StringLength(500)]
        public string? VacationTypeName { get; set; }

        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("Vacation_SDate")]
        [StringLength(10)]
        [Unicode(false)]
        public string? VacationSdate { get; set; }

        [Column("Vacation_Account_Day", TypeName = "decimal(18, 2)")]
        public decimal? VacationAccountDay { get; set; }

        [Column("Vacation_EDate")]
        [StringLength(10)]
        [Unicode(false)]
        public string? VacationEdate { get; set; }

        [Column("Vacation_Type_Id")]
        public int? VacationTypeId { get; set; }

        [StringLength(4000)]
        public string? Note { get; set; }

        public bool? IsDeleted { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }

        public long? ModifiedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }

        public long? CreatedBy { get; set; }

        public bool? Approve { get; set; }

        public int? FinancelYear { get; set; }

        [Column("Decision_Date")]
        [StringLength(10)]
        [Unicode(false)]
        public string? DecisionDate { get; set; }

        [Column("Decision_No")]
        [StringLength(50)]
        public string? DecisionNo { get; set; }

        [Column("HR_VDT_Id")]
        public int? HrVdtId { get; set; }

        [Column("Vacation_RDate")]
        [StringLength(10)]
        [Unicode(false)]
        public string? VacationRdate { get; set; }

        [Column("Emp_ID")]
        public long? EmpId { get; set; }

        public bool? IsSalary { get; set; }

        [Column("Status_Name")]
        [StringLength(50)]
        public string? StatusName { get; set; }

        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;

        [Column("Dept_ID")]
        public int? DeptId { get; set; }

        public int? Location { get; set; }

        [Column("Deducted_Balance_Vacation")]
        public bool? DeductedBalanceVacation { get; set; }

        [Column("Weekend_Include")]
        public bool? WeekendInclude { get; set; }

        [Column("Deducted_Service_Period")]
        public bool? DeductedServicePeriod { get; set; }

        [Column("Vacation_Type_Name2")]
        [StringLength(500)]
        public string? VacationTypeName2 { get; set; }

        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }

        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }

        [Column("Cat_ID")]
        public int? CatId { get; set; }

        public bool? NeedJoinRequest { get; set; }

        [Column("Status_Id")]
        public int? StatusId { get; set; }

        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }

        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }

        [Column("Status_Name2")]
        [StringLength(250)]
        public string? StatusName2 { get; set; }

        [Column("Facility_Name2")]
        [StringLength(500)]
        public string? FacilityName2 { get; set; }

        [Column("Qualification_Name2")]
        [StringLength(250)]
        public string? QualificationName2 { get; set; }

        [Column("Cat_name2")]
        [StringLength(250)]
        public string? CatName2 { get; set; }

        [Column("Nationality_Name2")]
        [StringLength(250)]
        public string? NationalityName2 { get; set; }

        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }

        [Column("BRA_NAME")]
        public string? BraName { get; set; }

        [Column("BRA_NAME2")]
        public string? BraName2 { get; set; }

        [Column("Location_ID")]
        public int? LocationId { get; set; }

        [Column("Shift_ID")]
        public int? ShiftId { get; set; }

        [Column("TimeTable_ID")]
        public int? TimeTableId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Salary { get; set; }

        [Column("Alternative_Emp_ID")]
        public long? AlternativeEmpId { get; set; }

        [Column("Alternative_Emp_Code")]
        [StringLength(50)]
        public string? AlternativeEmpCode { get; set; }

        [Column("Alternative_Emp_Name")]
        [StringLength(250)]
        public string? AlternativeEmpName { get; set; }

        [Column("Manager_ID")]
        public long? ManagerId { get; set; }

        [Column("Manager_Code")]
        [StringLength(50)]
        public string? ManagerCode { get; set; }

        [Column("Vacations_Day_Type_ID")]
        public int? VacationsDayTypeId { get; set; }

        [Column("Vacations_Day_Type_Name")]
        public string? VacationsDayTypeName { get; set; }

        [Column("Vacations_Day_Type_Name2")]
        public string? VacationsDayTypeName2 { get; set; }

        [Column("Vacations_Day_Type_Value", TypeName = "decimal(18, 2)")]
        public decimal? VacationsDayTypeValue { get; set; }

        [Column("Application_ID")]
        public long? ApplicationId { get; set; }

        [Column("Facility_ID")]
        public int? FacilityId { get; set; }

        [Column("Trans_Type_Id")]
        public int? TransTypeId { get; set; }
    }

}
