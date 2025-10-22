using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrCompensatoryVacationsVw
    {
        [Column("Compensatory_ID")]
        public long CompensatoryId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Vacation_Type_Id")]
        public int? VacationTypeId { get; set; }
        [Column("Vacation_Account_Day")]
        public int? VacationAccountDay { get; set; }
        [Column("Vacation_SDate")]
        [StringLength(10)]
        [Unicode(false)]
        public string? VacationSdate { get; set; }
        [Column("Vacation_EDate")]
        [StringLength(10)]
        [Unicode(false)]
        public string? VacationEdate { get; set; }
        [StringLength(4000)]
        public string? Note { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        public int? Location { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("BRA_NAME2")]
        public string? BraName2 { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        [Column("Vacation_Type_Name")]
        [StringLength(500)]
        public string? VacationTypeName { get; set; }
        [Column("Vacation_Type_Name2")]
        [StringLength(500)]
        public string? VacationTypeName2 { get; set; }
        [Column("Status_ID")]
        public long? StatusId { get; set; }
        [Column("App_ID")]
        public long? AppId { get; set; }
        //[Column("Dept_ID")]
        //public int? DeptId { get; set; }
    }
}
