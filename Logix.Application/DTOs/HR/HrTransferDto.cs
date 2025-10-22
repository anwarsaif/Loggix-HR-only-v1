using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Logix.Application.DTOs.HR
{
    public class HrTransferDto
    {
        public long Id { get; set; }
        public string? EmpCode { get; set; }
        public long? EmpId { get; set; }
        [StringLength(50)]
        public string? ResolutionNo { get; set; }
        public int? TransLocationFrom { get; set; }
        public int? TransLocationTo { get; set; }
        public int? TransDepartmentFrom { get; set; }
        public int? TransDepartmentTo { get; set; }
        public string? Note { get; set; }
        [StringLength(10)]
        public string? TransferDate { get; set; }
        public int? BranchId { get; set; }
        public int? BranchIdTo { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        
        //  قمنا باضافته من اجل الورديات
        public long? ShitID { get; set; }

    }
    public class HrTransferEditDto
    {
        public long Id { get; set; }
        public string? EmpCode { get; set; }
        public long? EmpId { get; set; }

        [StringLength(50)]
        public string? ResolutionNo { get; set; }
        public int? TransLocationFrom { get; set; }
        public int? TransLocationTo { get; set; }
        public int? TransDepartmentFrom { get; set; }
        public int? TransDepartmentTo { get; set; }
        public string? Note { get; set; }
        [StringLength(10)]
        public string? TransferDate { get; set; }
        public int? BranchId { get; set; }
        public int? BranchIdTo { get; set; }
        public long? ModifiedBy { get; set; }
        //  قمنا باضافته من اجل الورديات
        public long? ShitID { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

    public class HrTransferFilterDto
    {
        public long? Id { get; set; }
        public int? BranchId { get; set; }
        public string? EmpId { get; set; }
        public string? EmpName { get; set; }
        public int? LocationId { get; set; }
        public int? DeptId { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        /////////////
        public string? DescisionNumber { get; set; }
        public string? TransferDate { get; set; }
        public long? LocationFromId { get; set; }
        public long? LocationToId { get; set; }
        public long? BranchToId { get; set; }
        public long? BranchFromId { get; set; }
        public long? TransDepartmentFrom { get; set; }
        public long? TransDepartmentTo { get; set; }
    }  
    public class HrTransferSecondFilterDto
    {
        public long Id { get; set; }
        public string? EmpId { get; set; }
        public string? EmpName { get; set; }
        public int? TransLocationFrom { get; set; }

        public int? TransDepartmentFrom { get; set; }
        public int? TransBranchFrom { get; set; }
        public string? DescisionNumber { get; set; }
        public string? TransferDate { get; set; }
        public string? BranchFromName { get; set; }
        public string? LocationFromName { get; set; }
        public string? DeptFromName { get; set; }
        public string? Note { get; set; }
    }
    public class HrTransfersAllAddDto
    {
        public long Id { get; set; }
        [StringLength(50)]
        public string? ResolutionNo { get; set; }
        [Required]
        public int? TransLocationFrom { get; set; }
        [Required]
        public int? TransLocationTo { get; set; }
        [Required]
        public int? TransDepartmentFrom { get; set; }
        [Required]
        public int? TransDepartmentTo { get; set; }
        [Required]
        public string? Note { get; set; }
        [StringLength(10)]
        [Required]
        public string? TransferDate { get; set; }
        public int? BranchId { get; set; }
        public int? BranchIdTo { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public long? EmpId { get; set; }

        //  قمنا باضافته من اجل الورديات
        public long? ShitID { get; set; }
       public List<string> ?EmpIds { get; set; }

    }

    public class HrTransfersAdd2Dto
    {
        public long Id { get; set; }
        [Required]
        public string? EmpCode { get; set; }
        public long? EmpId { get; set; }

        [StringLength(50)]
        public string? EmpName { get; set; }
        public string? ResolutionNo { get; set; }
        [Required]
        public int? TransLocationFrom { get; set; }
        [Required]
        public int? TransLocationTo { get; set; }
        [Required]
        public int? TransDepartmentFrom { get; set; }
        [Required]
        public int? TransDepartmentTo { get; set; }
        public string? Note { get; set; }
        [StringLength(10)]
        public string? TransferDate { get; set; }
        [Required]
        public int? TransBranchIdfrom { get; set; }
        [Required]
        public int? TransBranchIdTo { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [Required]
        public decimal NewSalary { get; set; }  
        public decimal? Salary { get; set; }  
        public decimal AllowancesAmount { get; set; }  
        public decimal DeductionsAmount { get; set; }
        public decimal? NetSalary { get; set; }
        [Required]
        public decimal IncreaseAmount { get; set; }
        //  قمنا باضافته من اجل الورديات
        [Required]
        public long? ShitID { get; set; }
        public List<HrDeductionVM>? deduction { get; set; }
        public List<HrAllowanceVM>? allowance { get; set; }

    }
    public class HrDeductionVM
    {

        public long Id { get; set; }
        public int? TypeId { get; set; }
        public int? AddId { get; set; }
        public decimal? DeductionRate { get; set; }
        public decimal? DeductionAmount { get; set; }
        public decimal? NewAmount { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AllDedId { get; set; }
        public bool IsNew { get; set; }
    }
    public class HrAllowanceVM
    {

        public long Id { get; set; }
        public int? TypeId { get; set; }
        public int? AddId { get; set; }
        public decimal? AllowanceRate { get; set; }
        public decimal? AllowanceAmount { get; set; }
        public decimal? NewAmount { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AllDedId { get; set; }

        public bool IsNew { get; set; }
    }
}
