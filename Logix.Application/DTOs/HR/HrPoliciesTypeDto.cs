using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrPoliciesTypeFilterDto
    {


        [StringLength(250)]
        public string? TypeName { get; set; }


    }
    public class HrPoliciesTypeDto
    {
        public int TypeId { get; set; }

        [StringLength(250)]
        public string? TypeName { get; set; }

        [StringLength(250)]
        public string? TypeName2 { get; set; }
    }
    public class HrPoliciesTypeEditDto
    {
        public int TypeId { get; set; }

        [StringLength(250)]
        public string? TypeName { get; set; }

        [StringLength(250)]
        public string? TypeName2 { get; set; }
    }
}
