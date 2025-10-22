using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrKpiTypeDto
    {
    
        public int Id { get; set; }
        [StringLength(250)]
        public string? Name { get; set; }
        [StringLength(250)]
        public string? Name2 { get; set; }
        public bool? Isdeleted { get; set; }
        public int? SystemId { get; set; }
    }

    public class HrKpiTypeEditDto
    {

        public int Id { get; set; }
        [StringLength(250)]
        public string? Name { get; set; }
        [StringLength(250)]
        public string? Name2 { get; set; }
        public bool? Isdeleted { get; set; }
        public int? SystemId { get; set; }
    }

}
