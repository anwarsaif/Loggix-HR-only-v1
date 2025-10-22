using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.HR.EmployeeDto
{
    public class EmployeeContractInfoDto
    {
        public long Id { get; set; }
        //تاريخ انتهاء فترة التجربة
        [Column("Trial_Expiry_Date")]
        [StringLength(10)]
        public string? TrialExpiryDate { get; set; }
        //تاريخ العقد 
        [StringLength(10)]
        public string? ContractData { get; set; }
        //تاريخ انتهاء العقد 
        [StringLength(10)]
        public string? ContractExpiryDate { get; set; }
        //حالة فترة التجربة
        public int? ContractTypeId { get; set; }

        [Column("Trial_Status_ID")]
        public int? TrialStatusId { get; set; }
        //ملاحظات/شروط العقد
        public string? NoteContract { get; set; }
    }

}
