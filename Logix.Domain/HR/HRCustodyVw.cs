using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Keyless]

    public class HrCustodyVw
    {
        [Column("ID")]
        public long Id { get; set; }
        public long? Code { get; set; }
        [Column("T_Date")]
        [StringLength(10)]
        public string? TDate { get; set; }
        [Column("Trans_Type_ID")]
        public int? TransTypeId { get; set; }
        [Column("Emp_Id")]
        public long? EmpId { get; set; }
        public string? Note { get; set; }
        public long? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Refrance_Type")]
        public int? RefranceType { get; set; }
        [Column("Refrance_No")]
        public long? RefranceNo { get; set; }
        [Column("Refrance_Code")]
        [StringLength(250)]
        public string? RefranceCode { get; set; }
        [Column("Refrance_Name")]
        [StringLength(250)]
        public string? RefranceName { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Dep_ID")]
        public long? DepId { get; set; }
        [Column("Dep_Code")]
        public long? DepCode { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column("Type_Name")]
        [StringLength(50)]
        public string? TypeName { get; set; }
        [Column("Type_Name2")]
        [StringLength(50)]
        public string? TypeName2 { get; set; }
        public long? Expr1 { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string? EmpCode { get; set; }
        [Column("Responsible_Emp_ID")]
        public int? ResponsibleEmpId { get; set; }
        [Column("Responsible_Emp_name")]
        [StringLength(250)]
        public string? ResponsibleEmpName { get; set; }
        [Column("Emp_ID_To")]
        public long? EmpIdTo { get; set; }
        [Column("Dep_ID_To")]
        public long? DepIdTo { get; set; }
        public long? Responsible { get; set; }
        [Column("Inventory_ID")]
        public long? InventoryId { get; set; }
        public long? IsConfirmed { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ConfirmedDate { get; set; }
        [Column("Reason_ID")]
        public int? ReasonId { get; set; }
    }
}
