using Logix.Application.DTOs.Main;
using Logix.Domain.HR;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public partial class HrExpenseDto
    {
        public long? Id { get; set; }
        public string? Title { get; set; }
        public long? No { get; set; }
        public string? Code { get; set; }
        public string? ExDate { get; set; }
        public bool IsDeleted { get; set; }
        public long? AppId { get; set; }
        public int? StatusId { get; set; }
        public string? Note { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? Total { get; set; }
        public int? FacilityId { get; set; }
    }
    public partial class HrExpenseEditDto
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? RequestDate { get; set; }
        public long? AppId { get; set; }
        public string? Note { get; set; }
        public string? ApplicantCode { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? Total { get; set; }
        public List<HrExpensesEmployeeAddDto> employeeDetails { get; set; }
        public List<SaveFileDto> fileDtos { get; set; }
        public int? AppTypeID { get; set; }
    }
    public partial class HrExpenseFilterDto
    {
        public string? Title { get; set; }
        public string? Code { get; set; }
        public string? ExDate { get; set; }
        public long? AppCode { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

    }
    public partial class HrExpenseAddDto
    {
        public long? Id { get; set; }
        public string? Title { get; set; }
        public string? RequestDate { get; set; }
        public long? AppId { get; set; }
        public string? Note { get; set; }
        public string? ApplicantCode { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? Total { get; set; }
        public List<HrExpensesEmployeeAddDto> employeeDetails { get; set; }
        public List<SaveFileDto> fileDtos { get; set; }
        public int? AppTypeID { get; set; }

    }
    public class HrCreateExpensesEntryDto
    {
        public long Id { get; set; }

        [Required]
        public string RequestDate { get; set; } = null!;
        [Required]

        public string JournalDate { get; set; } = null!;


    }

}

