using Logix.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrCustodyDto : TraceEntity
    {
        [Key]
        public long Id { get; set; }
        public long? Code { get; set; }
        [StringLength(10)]
        public string? TDate { get; set; }
        public int? TransTypeId { get; set; }
        public int? TypeId { get; set; }
        public long? EmpId { get; set; }
        public long? DepId { get; set; }
        public string? Note { get; set; }


        public int? RefranceType { get; set; }
        public long? RefranceNo { get; set; }
        [StringLength(250)]
        public string? RefranceCode { get; set; }
        [StringLength(250)]
        public string? RefranceName { get; set; }
        public int? ResponsibleEmpId { get; set; }
        public long? EmpIdTo { get; set; }
        public long? DepIdTo { get; set; }
        public long? Responsible { get; set; }
        public long? InventoryId { get; set; }
        public long? IsConfirmed { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public int? ReasonId { get; set; }
        public int? RefranceId { get; set; }
    }
    public class HrCustodyEditDto
    {
        [Key]
        public long Id { get; set; }
        public long? Code { get; set; }
        [StringLength(10)]
        public string? TDate { get; set; }
        public int? TransTypeId { get; set; }
        public int? TypeId { get; set; }
        public long? EmpId { get; set; }
        public long? DepId { get; set; }
        public string? Note { get; set; }

        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public int? RefranceType { get; set; }
        public long? RefranceNo { get; set; }
        [StringLength(250)]
        public string? RefranceCode { get; set; }
        [StringLength(250)]
        public string? RefranceName { get; set; }
        public int? ResponsibleEmpId { get; set; }
        public long? EmpIdTo { get; set; }
        public long? DepIdTo { get; set; }
        public long? Responsible { get; set; }
        public long? InventoryId { get; set; }
        public long? IsConfirmed { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public int? ReasonId { get; set; }
        public int? RefranceId { get; set; }
    }
}
