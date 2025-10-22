using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrRequestTypeDto
    {
        public long Id { get; set; }
        [Column("Request_Name")]
        [StringLength(250)]
        public string? RequestName { get; set; }
        [Column("Request_Name2")]
        [StringLength(250)]
        public string? RequestName2 { get; set; }
        public bool? IsDeleted { get; set; }

    }
}
