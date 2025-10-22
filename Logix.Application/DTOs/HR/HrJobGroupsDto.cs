using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrJobGroupsDto
    {
        public long? Id { get; set; }
        [StringLength(50)]
        public string? Code { get; set; }
        [StringLength(250)]
        public string? Name { get; set; }
        [StringLength(250)]
        public string? Name2 { get; set; }
        public long? ParentId { get; set; }
        public bool? HasSubGroup { get; set; }
        public int? StatusId { get; set; }
		public long? StatusCode { get; set; }
		public bool IsDeleted { get; set; }
    }
    public class HrJobGroupsEditDto
    {
        public long? Id { get; set; }
        [StringLength(50)]
        public string? Code { get; set; }
        [StringLength(250)]
        public string? Name { get; set; }
        [StringLength(250)]
        public string? Name2 { get; set; }
        public long? ParentId { get; set; }
        public bool? HasSubGroup { get; set; }
        public int? StatusId { get; set; }
		public long? StatusCode { get; set; }

	}
	public class HrJobGroupsFilterDto
    {
		public long? Id { get; set; }
		[StringLength(50)]
		public string? Code { get; set; }
		[StringLength(250)]
		public string? Name { get; set; }
		[StringLength(250)]
		public string? Name2 { get; set; }
		public long? ParentId { get; set; }
		public string? ParentName { get; set; }
		public string? ParentName2 { get; set; }
		public bool? HasSubGroup { get; set; }
		public int? StatusId { get; set; }
		public string? StatusName { get; set; }
		public string? StatusName2 { get; set; }
		public long? StatusCode { get; set; }

	}
}
