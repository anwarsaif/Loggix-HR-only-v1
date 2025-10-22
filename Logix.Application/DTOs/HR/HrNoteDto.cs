using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrNoteDto
    {
        public long NoteId { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        [StringLength(4000)]
        public string? NoteText { get; set; }
        [StringLength(50)]
        public string? NoteDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class HrNoteEditDto
    {
        public long NoteId { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }

        [StringLength(4000)]
        public string? NoteText { get; set; }
        [StringLength(50)]
        public string? NoteDate { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
    }
    public class HrNoteFilterDto
    {
        public long? NoteId { get; set; }
        public string? EmpId { get; set; }
        public string? EmpName { get; set; }
        [StringLength(4000)]
        public string? NoteText { get; set; }
        [StringLength(50)]
        public string? NoteDate { get; set; }
        public string? EmpCode { get; set; }

    }
}
