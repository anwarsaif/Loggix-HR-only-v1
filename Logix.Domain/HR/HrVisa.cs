using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Visa")]
    public partial class HrVisa : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Visa_Type")]
        public int? VisaType { get; set; }
        [Column("Visa_Date")]
        [StringLength(10)]
        public string? VisaDate { get; set; }
        [Column("Place_of_visit")]
        [StringLength(250)]
        public string? PlaceOfVisit { get; set; }
        [Column("Start_date")]
        [StringLength(10)]
        public string? StartDate { get; set; }
        [Column("End_Date")]
        [StringLength(10)]
        public string? EndDate { get; set; }
        [Column("Visa_days")]
        public int? VisaDays { get; set; }
        public string? Purpose { get; set; }
        [Column("Is_billable")]
        public int? IsBillable { get; set; }
        public string? Note { get; set; }
        [Column("PDF_FilePath")]
        [Unicode(false)]
        public string? PdfFilePath { get; set; }
        [Column("Visa_Number")]
        public string? VisaNumber { get; set; }
        [Column("Visa_State")]
        [StringLength(30)]
        public string? VisaState { get; set; }
    }
}
