using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrVacationsCatagoryDto
    {
        public int CatId { get; set; }
        [StringLength(250)]
        public string? CatName { get; set; }
        [StringLength(250)]
        public string? CatName2 { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class HrVacationsCatagoryEditDto
    {
        public int CatId { get; set; }
        [StringLength(250)]
        public string? CatName { get; set; }
        [StringLength(250)]
        public string? CatName2 { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

}
