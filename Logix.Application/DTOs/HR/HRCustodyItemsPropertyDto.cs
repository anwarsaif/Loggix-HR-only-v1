using Logix.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrCustodyItemsPropertyDto : TraceEntity
    {
        public long Id { get; set; }
        public long? CustodyItemId { get; set; }
        public long? ItemId { get; set; }
        public long? PropertyId { get; set; }
        public bool? PropertyValue { get; set; }
        public string? Note { get; set; }

    } 
    public class HrCustodyItemsPropertyEditDto
    {
        public long Id { get; set; }
        public long? CustodyItemId { get; set; }
        public long? ItemId { get; set; }
        public long? PropertyId { get; set; }
        public bool? PropertyValue { get; set; }
        public string? Note { get; set; }

        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
