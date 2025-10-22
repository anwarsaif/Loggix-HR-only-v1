using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrRequestDetailsDto
    {
        public long Id { get; set; }
        //نوع الطلب 

        public long? RequestId { get; set; }
        /// <summary>
        /// هذا الحقل يجب تعبئته بالقيمة في القائمة المنسدلة  للحقل الذي يسمى النوع 
        /// </summary>
        public string? AllownceDeductionName { get; set; }
        public long? EmpId { get; set; }
        public string? empCode { get; set; }
        public long? RequestType { get; set; }
        public long? AllownceId { get; set; }
        public long? DeductionId { get; set; }

        public long? OverTimeId { get; set; }
        public decimal? Value { get; set; }
        public string? IdNo { get; set; }

        public string? IdExpireDate { get; set; }

        public string? PassExpireDate { get; set; }
        public string? Description { get; set; }
        public long? AbsenceTypeId { get; set; }
        public string? AbsenceDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public int? type { get; set; }
        public int? typeId { get; set; }

    }
    public class HrRequestDetailsEditDto
    {
        public long Id { get; set; }
        [Column("Request_ID")]
        public long? RequestId { get; set; }
        [Column("EMP_ID")]
        public long? EmpId { get; set; }
        [Column("Request_Type")]
        public long? RequestType { get; set; }
        [Column("Allownce_ID")]
        public long? AllownceId { get; set; }
        [Column("Deduction_ID")]
        public long? DeductionId { get; set; }
        [Column("OverTime_ID")]
        public long? OverTimeId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Value { get; set; }
        [Column("ID_No")]
        [StringLength(50)]
        public string? IdNo { get; set; }
        [Column("ID_Expire_Date")]
        [StringLength(10)]
        public string? IdExpireDate { get; set; }
        [Column("Pass_Expire_Date")]
        [StringLength(10)]
        public string? PassExpireDate { get; set; }
        public string? Description { get; set; }
        [Column("Absence_Type_Id")]
        public long? AbsenceTypeId { get; set; }
        [Column("Absence_Date")]
        [StringLength(10)]
        public string? AbsenceDate { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }

    }
}
