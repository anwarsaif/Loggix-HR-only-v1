using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Transfers")]
    public partial class HrTransfer
    {
        [Column("ID")]
        public long Id { get; set; }

        [Column("Emp_ID")]
        public long? EmpId { get; set; }

        [Column("Resolution_No")]
        [StringLength(50)]
        public string? ResolutionNo { get; set; }

        [Column("Trans_Location_From")]
        public int? TransLocationFrom { get; set; }

        [Column("Trans_Location_To")]
        public int? TransLocationTo { get; set; }

        [Column("Trans_Department_From")]
        public int? TransDepartmentFrom { get; set; }

        [Column("Trans_Department_To")]
        public int? TransDepartmentTo { get; set; }

        public string? Note { get; set; }

        [Column("Transfer_Date")]
        [StringLength(10)]
        public string? TransferDate { get; set; }

        public long? CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }

        public long? ModifiedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("BRANCH_ID_To")]
        public int? BranchIdTo { get; set; }
    }
}