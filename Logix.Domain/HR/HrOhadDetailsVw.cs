using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrOhadDetailsVw 
    {
        public long? ItemId { get; set; }
        [StringLength(500)]
        public string? ItemName { get; set; }
        [StringLength(150)]
        public string? ItemState { get; set; }
        [StringLength(1500)]
        public string? ItemDetails { get; set; }
        [StringLength(50)]
        public string? Note { get; set; }
        [Column("Ohad_Det_Id")]
        public long OhadDetId { get; set; }
        [Column("Qty_in", TypeName = "decimal(18, 2)")]
        public decimal? QtyIn { get; set; }
        [Column("Qty_Out", TypeName = "decimal(18, 2)")]
        public decimal? QtyOut { get; set; }
        public long? OhdaId { get; set; }
        [Column("Emp_Id")]
        public long? EmpId { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string? EmpCode { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [StringLength(50)]
        public string? OhdaDate { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("ItemState_ID")]
        public int? ItemStateId { get; set; }
        [Column("Trans_Type_ID")]
        public int? TransTypeId { get; set; }
        public long? Code { get; set; }
        [Column("Trans_Type_Name")]
        [StringLength(250)]
        public string? TransTypeName { get; set; }
        [Column("Orgnal_Id")]
        public long? OrgnalId { get; set; }
        [Column("ID_Ohda")]
        public long IdOhda { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        public int? Location { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        [Column("Emp_ID_Recipient")]
        public long? EmpIdRecipient { get; set; }
        [Column("Emp_name_Recipient")]
        [StringLength(250)]
        public string? EmpNameRecipient { get; set; }
        [Column("Emp_Code_Recipient")]
        [StringLength(50)]
        public string? EmpCodeRecipient { get; set; }
        [Column("ItemState_Name")]
        [StringLength(250)]
        public string? ItemStateName { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("Ohad_Note")]
        public string? OhadNote { get; set; }
        [Column("Emp_Code_To")]
        [StringLength(50)]
        public string? EmpCodeTo { get; set; }
        [Column("Emp_Name_To")]
        [StringLength(250)]
        public string? EmpNameTo { get; set; }
    }
}
