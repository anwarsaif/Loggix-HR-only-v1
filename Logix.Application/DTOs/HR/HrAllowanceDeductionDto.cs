using Logix.Application.DTOs.Main;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrAllowanceDeductionDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public int? TypeId { get; set; }
        public int? AdId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        [StringLength(50)]
        public string? DecisionNo { get; set; }
        [StringLength(50)]
        public string? DecisionDate { get; set; }
        [StringLength(10)]
        public string? StartDate { get; set; }
        [StringLength(10)]
        public string? EndDate { get; set; }
        public string? ContractDate { get; set; }
        public string? Note { get; set; }
        public int? FinancelYear { get; set; }
        public bool? Status { get; set; }
        public int? FixedOrTemporary { get; set; }
        [StringLength(10)]
        public string? DueDate { get; set; }
        public long? PreparationSalariesId { get; set; }
        public long? MAdId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }

    }


    public class HrAllowanceDeductionEditDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public int? TypeId { get; set; }
        public int? AdId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        [StringLength(50)]
        public string? DecisionNo { get; set; }
        public string? EmpCode { get; set; }

        [StringLength(50)]
        public string? DecisionDate { get; set; }
        [StringLength(10)]
        public string? StartDate { get; set; }
        [StringLength(10)]
        public string? EndDate { get; set; }
        public string? Note { get; set; }
        public int? FinancelYear { get; set; }
        public bool? Status { get; set; }
        public int? FixedOrTemporary { get; set; }
        [StringLength(10)]
        public string? DueDate { get; set; }
        public long? PreparationSalariesId { get; set; }
        public long? MAdId { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }



    public class HrAllowanceDeductionExtraVM
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        //public string? EmpCode { get; set; }
        public int? TypeId { get; set; }
        public int? AdId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
    }


    public class HrAllowanceDeductionFilterDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? TypeName { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public decimal? Amount { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

    }

    public class HrAllowanceDeductionAutoDataDto
    {
        public string? EmpId { get; set; }
        public string? EmpName { get; set; }
        //public string? TypeName { get; set; }
        public decimal? allowanceAmount { get; set; }
        public decimal? deductionAmount { get; set; }
        public decimal? salary { get; set; }
        //  الصافي
        public decimal? totalSalary { get; set; }
    }


    public class HrAllowanceDeductionOtherFilterDto
    {
        public long Id { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public decimal? Amount { get; set; }

        public string? DueDate { get; set; }
        public int? DeptId { get; set; }
        public int? BranchId { get; set; }
        public int? Location { get; set; }

        public int? Type { get; set; }

        public int? ADID { get; set; }
        public int? categoryId { get; set; }
        //////////////////////////////
        public string? EmpName2 { get; set; }
        public string? DeptName { get; set; }
        public string? CatName { get; set; }

    }


    public class HrOtherAllowanceDeductionAddDto
    {
        public long Id { get; set; }
        public string? EmpCode { get; set; }
        [Required]
        public int? TypeId { get; set; }
        [Required]
        public int? AdId { get; set; }
        public decimal? Rate { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        public string? Note { get; set; }
        public int? FixedOrTemporary { get; set; }
        [Required]
        public string? DueDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }

    }
    public class HrOtherAllowanceDeductionEditDto
    {
        public long Id { get; set; }
        public string? EmpCode { get; set; }
        public int? TypeId { get; set; }
        public int? AdId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public string? Note { get; set; }
        public int? FixedOrTemporary { get; set; }
        public string? DueDate { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }


    }

    // اضافة - حسميات أو بدلات متعددة
    public class HrOtherAllowanceDeductionMultiAddDto
    {
        [Required]
        public int? TypeId { get; set; }
        [Required]
        public int? AdId { get; set; }
        [Required]
        public string? DueDate { get; set; }
        public List<HrOtherAllowanceDeductionMultiAddDataDto>? dataDto { get; set; }

    }
    public class HrOtherAllowanceDeductionMultiAddDataDto
    {
        public string? EmpCode { get; set; }
        public decimal? Amount { get; set; }

    }

    // اضافة - حسميات أو بدلات أخرى لفترة   
    public class HrOtherAllowanceDeductionIntervalAddDto
    {
        public int? TypeId { get; set; }
        public int? AdId { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? Note { get; set; }
        public decimal? Amount { get; set; }
        public string? EmpCode { get; set; }

    }

    //  سحب البدلات والحسميات من الإكسل
    public class HrOtherAllowanceDeductionAddFromExcelDto
    {
        public string? EmpCode { get; set; }
        public int? TypeId { get; set; }
        public int? AdId { get; set; }
        public decimal? Amount { get; set; }
        public string? Note { get; set; }
        public string? DueDate { get; set; }
    }
    public class AddFromExcelResultDto
    {
        public int? SavedRecord { get; set; }
        public string? SavedRecordMessage { get; set; }
        public List<string>? EmpWithProblems { get; set; }

    }

}
