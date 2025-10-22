using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrIncrementsVw : TraceEntity
    {
        //    [Column("Emp_name")]
        //    [StringLength(250)]
        //    public string? EmpName { get; set; }
        //    [Column("ID")]
        //    public long Id { get; set; }
        //    [Column("Emp_ID")]
        //    public long? EmpId { get; set; }
        //    [Column("Increase_Date")]
        //    [StringLength(10)]
        //    public string? IncreaseDate { get; set; }
        //    [Column("Increase_Amount", TypeName = "decimal(18, 2)")]
        //    public decimal? IncreaseAmount { get; set; }
        //    [Column("Start_Date")]
        //    [StringLength(10)]
        //    public string? StartDate { get; set; }
        //    [Column(TypeName = "decimal(18, 2)")]
        //    public decimal? Salary { get; set; }
        //    [Column(TypeName = "decimal(18, 2)")]
        //    public decimal? Allowances { get; set; }
        //    [Column(TypeName = "decimal(18, 2)")]
        //    public decimal? Deductions { get; set; }
        //    [Column("BRANCH_ID")]
        //    public int? BranchId { get; set; }
        //    [Column("Emp_Code")]
        //    [StringLength(50)]
        //    public string EmpCode { get; set; } = null!;
        //    [Column("Status_ID")]
        //    public bool? StatusId { get; set; }
        //    [Column(TypeName = "decimal(18, 2)")]
        //    public decimal? NewSalary { get; set; }
        //    [Column("Emp_name2")]
        //    [StringLength(250)]
        //    public string? EmpName2 { get; set; }
        //    [Column("Nationality_Name")]
        //    [StringLength(250)]
        //    public string? NationalityName { get; set; }
        //    [Column("Dep_Name")]
        //    [StringLength(200)]
        //    public string? DepName { get; set; }
        //    [Column("Location_Name")]
        //    [StringLength(200)]
        //    public string? LocationName { get; set; }
        //    [Column("Cat_name")]
        //    [StringLength(250)]
        //    public string? CatName { get; set; }
        //    [Column("DOAppointment")]
        //    [StringLength(12)]
        //    public string? Doappointment { get; set; }
        //    [Column("New_Deduction", TypeName = "decimal(38, 2)")]
        //    public decimal NewDeduction { get; set; }
        //    [Column("New_Allowance", TypeName = "decimal(38, 2)")]
        //    public decimal NewAllowance { get; set; }
        //    [Column("Apply_Type")]
        //    public int? ApplyType { get; set; }
        //    public string? Note { get; set; }
        //    [Column("Decision_Date")]
        //    [StringLength(10)]
        //    public string? DecisionDate { get; set; }
        //    [Column("Decision_No")]
        //    [StringLength(250)]
        //    public string? DecisionNo { get; set; }
        //    [Column("New_Job_ID")]
        //    public long? NewJobId { get; set; }
        //    [Column("New_Cat_Job_ID")]
        //    public long? NewCatJobId { get; set; }
        //    [Column("New_Grad_ID")]
        //    public long? NewGradId { get; set; }
        //    [Column("New_Level_ID")]
        //    public long? NewLevelId { get; set; }
        //    [Column("Cur_Grad_ID")]
        //    public long? CurGradId { get; set; }
        //    [Column("Cur_Level_ID")]
        //    public long? CurLevelId { get; set; }
        //    [Column("Cur_Job_ID")]
        //    public long? CurJobId { get; set; }
        //    [Column("Cur_Cat_Job_ID")]
        //    public long? CurCatJobId { get; set; }
        //    [Column("Trans_Type_ID")]
        //    public int? TransTypeId { get; set; }
        //    [Column("Grade_Name")]
        //    [StringLength(50)]
        //    public string? GradeName { get; set; }
        //    [Column("New_Grade_Name")]
        //    [StringLength(50)]
        //    public string? NewGradeName { get; set; }
        //    [Column("New_Level_Name")]
        //    [StringLength(250)]
        //    public string? NewLevelName { get; set; }
        //    [Column("Level_Name")]
        //    [StringLength(250)]
        //    public string? LevelName { get; set; }
        //    [Column("Trans_Type_Name")]
        //    [StringLength(50)]
        //    public string? TransTypeName { get; set; }
        //    [Column("Trans_Type_Name2")]
        //    [StringLength(50)]
        //    public string? TransTypeName2 { get; set; }
        //    [Column("BRA_NAME")]
        //    public string? BraName { get; set; }
        //    [Column("Dep_Name2")]
        //    [StringLength(200)]
        //    public string? DepName2 { get; set; }
        //    [Column("Location_Name2")]
        //    [StringLength(200)]
        //    public string? LocationName2 { get; set; }
        //    [Column("BRA_NAME2")]
        //    public string? BraName2 { get; set; }
        //    [Column("Dept_ID")]
        //    public int? DeptId { get; set; }
        //    public int? Location { get; set; }
        //    [Column("App_ID")]
        //    public int? AppId { get; set; }
        //}
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }

        [Column("ID")]
        public long Id { get; set; }

        [Column("Emp_ID")]
        public long? EmpId { get; set; }

        [Column("Increase_Date")]
        [StringLength(10)]
        public string? IncreaseDate { get; set; }

        [Column("Increase_Amount", TypeName = "decimal(18, 2)")]
        public decimal? IncreaseAmount { get; set; }

        [Column("Start_Date")]
        [StringLength(10)]
        public string? StartDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Salary { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Allowances { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Deductions { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;

        [Column("Status_ID")]
        public bool? StatusId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? NewSalary { get; set; }

        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }

        [Column("Nationality_Name")]
        [StringLength(250)]
        public string? NationalityName { get; set; }

        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }

        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }

        [Column("Cat_name")]
        [StringLength(250)]
        public string? CatName { get; set; }

        [Column("DOAppointment")]
        [StringLength(12)]
        public string? Doappointment { get; set; }

        [Column("New_Deduction", TypeName = "decimal(38, 2)")]
        public decimal NewDeduction { get; set; }

        [Column("New_Allowance", TypeName = "decimal(38, 2)")]
        public decimal NewAllowance { get; set; }

        [Column("Apply_Type")]
        public long? ApplyType { get; set; }

        public string? Note { get; set; }

        [Column("Decision_Date")]
        [StringLength(10)]
        public string? DecisionDate { get; set; }

        [Column("Decision_No")]
        [StringLength(250)]
        public string? DecisionNo { get; set; }

        [Column("New_Job_ID")]
        public long? NewJobId { get; set; }

        [Column("New_Cat_Job_ID")]
        public long? NewCatJobId { get; set; }

        [Column("New_Grad_ID")]
        public long? NewGradId { get; set; }

        [Column("New_Level_ID")]
        public long? NewLevelId { get; set; }

        [Column("Cur_Grad_ID")]
        public long? CurGradId { get; set; }

        [Column("Cur_Level_ID")]
        public long? CurLevelId { get; set; }

        [Column("Cur_Job_ID")]
        public long? CurJobId { get; set; }

        [Column("Cur_Cat_Job_ID")]
        public long? CurCatJobId { get; set; }

        [Column("Trans_Type_ID")]
        public int? TransTypeId { get; set; }

        [Column("Grade_Name")]
        [StringLength(50)]
        public string? GradeName { get; set; }

        [Column("New_Grade_Name")]
        [StringLength(50)]
        public string? NewGradeName { get; set; }

        [Column("New_Level_Name")]
        [StringLength(250)]
        public string? NewLevelName { get; set; }

        [Column("Level_Name")]
        [StringLength(250)]
        public string? LevelName { get; set; }

        [Column("Trans_Type_Name")]
        [StringLength(50)]
        public string? TransTypeName { get; set; }

        [Column("Trans_Type_Name2")]
        [StringLength(50)]
        public string? TransTypeName2 { get; set; }

        [Column("BRA_NAME")]
        public string? BraName { get; set; }

        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }

        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }

        [Column("BRA_NAME2")]
        public string? BraName2 { get; set; }

        [Column("Dept_ID")]
        public int? DeptId { get; set; }

        public int? Location { get; set; }

        [Column("App_ID")]
        public int? AppId { get; set; }

        [Column("New_Cat_Name")]
        [StringLength(250)]
        public string? NewCatName { get; set; }

        [Column("New_Cat_Name2")]
        [StringLength(250)]
        public string? NewCatName2 { get; set; }
    }

}
