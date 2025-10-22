using Logix.Application.DTOs.Main;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrVisaDto
    {
        public long Id { get; set; }
        [Required]
        public string? EmpCode { get; set; }
        [Required]
        public int? VisaType { get; set; }
        [Required]
        public string? VisaDate { get; set; }
        [Required]
        public string? PlaceOfVisit { get; set; }
        [Required]
        public string? StartDate { get; set; }
        [Required]
        public string? EndDate { get; set; }
        [Required]
        public int? VisaDays { get; set; }
        public string? Purpose { get; set; }
        [Required]
        public int? IsBillable { get; set; }
        public string? Note { get; set; }
        public bool IsDeleted { get; set; }
        public string? PdfFilePath { get; set; }
        public string? VisaNumber { get; set; }
        public string? VisaState { get; set; }

        public List<SaveFileDto>? fileDtos { get; set; }
    }

    public class HrVisaEditDto
    {
        public long Id { get; set; }
        [Required]
        public string? EmpCode { get; set; }
        public long? EmpId { get; set; }
        [Required]
        public int? VisaType { get; set; }
        [Required]
        public string? VisaDate { get; set; }
        [Required]
        public string? PlaceOfVisit { get; set; }
        [Required]
        public string? StartDate { get; set; }
        [Required]
        public string? EndDate { get; set; }
        [Required]
        public int? VisaDays { get; set; }
        public string? Purpose { get; set; }
        [Required]
        public int? IsBillable { get; set; }
        public string? Note { get; set; }

        public string? VisaNumber { get; set; }
        public string? VisaState { get; set; }

        public List<SaveFileDto>? fileDtos { get; set; }
    }

    public partial class HrVisaFilterDto
    {
        public long? Id { get; set; }
        public string? EmpCode { get; set; }
        public int? VisaType { get; set; }
        public string? VisaDate { get; set; }
        public string? PlaceOfVisit { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int? VisaDays { get; set; }
        public string? EmpName { get; set; }
        public string? VisaTypeType { get; set; }
        public int? BranchId { get; set; }
        public int? DeptId { get; set; }
        public int? Location { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? DateType { get; set; }
    }

}
