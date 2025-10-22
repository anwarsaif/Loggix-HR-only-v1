
using Logix.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrCardTemplateDto
    {
        public long Id { get; set; }
        [StringLength(500)]
       
        [Required]
        public string? Name { get; set; }
        [StringLength(500)]
   
        public string? ImgUrl { get; set; }
        public int? Status { get; set; }

      
        public decimal? TxtXposition { get; set; }

       
        public decimal? TxtYposition { get; set; }

       
        [StringLength(50)]
        public string? TxtSize { get; set; }

       
        [StringLength(50)]
        public string? TxtFont { get; set; }

        [StringLength(50)]
       
        public string? TxtColor { get; set; }

        public long? FacilityId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class HrCardTemplateEditDto
    {
        public long Id { get; set; }
        [StringLength(500)]
        [Required]
        public string? Name { get; set; }
        [StringLength(500)]

        public string? ImgUrl { get; set; }
        public int? Status { get; set; }

   
        public decimal? TxtXposition { get; set; }

     
        public decimal? TxtYposition { get; set; }

        
        [StringLength(50)]
        public string? TxtSize { get; set; }

       
        [StringLength(50)]
        public string? TxtFont { get; set; }

        [StringLength(50)]
        
        public string? TxtColor { get; set; }

        public long? FacilityId { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
    public class HrCardTemplateFilterDto
    {
       
        public string? Name { get; set; }
    }
}
