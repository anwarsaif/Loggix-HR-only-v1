using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Logix.Application.DTOs.Main;

namespace Logix.Application.DTOs.HR
{
    public class HrRequestDto
    {
        public long Id { get; set; }
        [Column("RE_Date")]
        [StringLength(10)]
        public string? ReDate { get; set; }
        [Column("Branch_ID")]
        public long? BranchId { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        [Column("App_ID")]
        public long? AppId { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        [Column("EMP_ID")]
        public long? EmpId { get; set; }
        [Column("Request_Type")]
        public long? RequestType { get; set; }
        [Column("Date_From")]
        [StringLength(10)]
        public string? DateFrom { get; set; }
        [Column("Date_To")]
        [StringLength(10)]
        public string? DateTo { get; set; }
        public string? Subject { get; set; }
        public string? Note { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }

    }
    public class HrRequestEditDto
    {
        public long Id { get; set; }
        [Column("RE_Date")]
        [StringLength(10)]
        public string? ReDate { get; set; }
        [Column("Branch_ID")]
        public long? BranchId { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        [Column("App_ID")]
        public long? AppId { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        [Column("EMP_ID")]
        public long? EmpId { get; set; }
        [Column("Request_Type")]
        public long? RequestType { get; set; }
        [Column("Date_From")]
        [StringLength(10)]
        public string? DateFrom { get; set; }
        [Column("Date_To")]
        [StringLength(10)]
        public string? DateTo { get; set; }
        public string? Subject { get; set; }
        public string? Note { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public int? AppTypeId { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }
        public List<HrRequestDetailsDto>? RequestDto { get; set; }

    }


    public class HrRequestFilterDto
    {
        public long? Id { get; set; }
        public string? DateFrom { get; set; }
        public string? DateTo { get; set; }
        public int? StatusId { get; set; }
        public long? BranchId { get; set; }
        public long? ApplicationCode { get; set; }
    }


    public class RequestSubDto
    {
        public string? EmpName { get; set; }
        public string? Doappointment { get; set; }
        public int? Gender { get; set; }
        public int? MaritalStatus { get; set; }
        public int? NationalityId { get; set; }
        public int? JobCatagoriesId { get; set; }
        public int? BranchId { get; set; }
        public string EmpId { get; set; } = null!;
        public int? Location { get; set; }
        public decimal? Salary { get; set; }
        public int? ProgramId { get; set; }
        public int? FacilityId { get; set; }
        public int? DeptId { get; set; }
        public long? ParentId { get; set; }
        public string? ContractExpiryDate { get; set; }
        public string? ContractData { get; set; }
        public long? SalaryGroupId { get; set; }
        public string? Iban { get; set; }
        public string? AccountNo { get; set; }
        public int? BankId { get; set; }
        public long? ManagerId { get; set; }
        public decimal? DailyWorkingHours { get; set; }
        public string? IdNo { get; set; }

        public List<DeductionVM>? deduction { get; set; }
        public List<AllowanceVM>? allowance { get; set; }
        public long? ShitID { get; set; }
        public long? AttTimeTableID { get; set; }
    }

    public class HrRequestAddDto
    {
        public long? Id { get; set; }
        public string? ReDate { get; set; }
        public long? BranchId { get; set; }
        public long? FacilityId { get; set; }
        public int? AppTypeId { get; set; }
        public int? StatusId { get; set; }
        public long? EmpId { get; set; }
        public long? RequestType { get; set; }

        public string? DateFrom { get; set; }
        public string? DateTo { get; set; }
        public string? Subject { get; set; }
        public string? Note { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }
        public List<HrRequestDetailsDto>? RequestDto { get; set; }
    }
}
