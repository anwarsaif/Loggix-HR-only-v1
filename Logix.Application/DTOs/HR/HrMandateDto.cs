using Logix.Application.DTOs.Main;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrMandateDto
    {
        public long Id { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public string? FromLocation { get; set; }
        public string? ToLocation { get; set; }
        public string? Objective { get; set; }
        public int? VisaTravel { get; set; }
        public int? TravelBy { get; set; }
        public int? Accommodation { get; set; }
        public int? NoOfNight { get; set; }
        public decimal? RatePerNight { get; set; }
        public decimal? OtherExpenses { get; set; }
        public decimal? ActualExpenses { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? Note { get; set; }
        public decimal? TransportAmount { get; set; }
        public int? TypeId { get; set; }
        public int? TicketType { get; set; }
        public decimal? TicketValue { get; set; }
        public long? PayrollId { get; set; }
        public int? CatId { get; set; }
        public long? AppId { get; set; }
    }


    public class HrMandateEditDto
    {
        public long Id { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public string? FromLocation { get; set; }
        public string? ToLocation { get; set; }
        public string? Objective { get; set; }
        public int? VisaTravel { get; set; }
        public int? TravelBy { get; set; }
        public int? Accommodation { get; set; }
        public int NoOfNight { get; set; }
        public decimal RatePerNight { get; set; }
        public decimal OtherExpenses { get; set; }
        public decimal ActualExpenses { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? Note { get; set; }
        public decimal TransportAmount { get; set; }
        public int? TypeId { get; set; }
        public int? TicketType { get; set; }
        public decimal TicketValue { get; set; }
        public long? PayrollId { get; set; }
        public int? CatId { get; set; }
        public long? AppId { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }
    }

    public class HrMandateFilterDto
    {
        public long Id { get; set; }

        public string? EmpCode { get; set; }
        public int? BranchId { get; set; }
        public string? EmpName { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? FromLocation { get; set; }
        public string? ToLocation { get; set; }
        public int? DeptId { get; set; }
        public int? LocationId { get; set; }
        public int? JobCategory { get; set; }
        public int? MabdateCategory { get; set; }

        public decimal? ActualExpenses { get; set; }
        public string? Note { get; set; }
    }

    public class HrMandateAddDto
    {
        [Required]
        public string? FromDate { get; set; }
        [Required]
        public string? ToDate { get; set; }
        [Required]
        public string? EmpCode { get; set; }
        [Required]
        public string? FromLocation { get; set; }
        [Required]
        public string? ToLocation { get; set; }
        [Required]
        public string? Objective { get; set; }
        public int? VisaTravel { get; set; }
        public int? TravelBy { get; set; }
        public int? Accommodation { get; set; }
        [Required]
        public int NoOfNight { get; set; }
        [Required]
        public decimal RatePerNight { get; set; }
        public decimal OtherExpenses { get; set; }
        public decimal ActualExpenses { get; set; }
        public string? Note { get; set; }
        public decimal TransportAmount { get; set; }
        [Required]
        public int? TypeId { get; set; }
        [Required]
        public int? TicketType { get; set; }
        [Required]
        public decimal TicketValue { get; set; }
        [Required]
        public int? CatId { get; set; }
        public long? AppId { get; set; }
        /////////////////
        public int? AppTypeId { get; set; } = 0;



        public long? StatusId { get; set; }

        public List<SaveFileDto> fileDtos { get; set; }

    }

    public class HrMandateDashboardFilterDto
    {
        public int? DeptId { get; set; }
        public int? Location { get; set; }
        public int? BranchId { get; set; }
        public int? TypeId { get; set; }
        public int? JobCatagoryID { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? Cnt { get; set; }
        public string? LeaveTypeName { get; set; }
        public string? Icon { get; set; }
        public string? Url { get; set; }
        public string? Color { get; set; }
        public string? LocationName { get; set; }
        public string? LocationName2 { get; set; }
        public string? BraName { get; set; }
        public string? BraName2 { get; set; }
        public string? DepName { get; set; }
        public string? DepName2 { get; set; }
        public decimal? TotalActualExpenses { get; set; }
    }

    public class HRMandatePayrollAddDto
    {
        public long Id { get; set; }
        public string PayDate { get; set; } = null!;

        public int? PayrolllTypeId { get; set; }
        public int? State { get; set; }
    }
}
