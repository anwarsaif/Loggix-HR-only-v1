using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Mandate_Location_Master", Schema = "dbo")]
    public partial class HrMandateLocationMaster
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Name2 { get; set; }
        [Column("Country_Classification_ID")]
        public long? CountryClassificationId { get; set; }
        public string? Note { get; set; }
        [Column("Type_ID")]
        public long? TypeId { get; set; }
        [Column("From_Location")]
        [StringLength(250)]
        public string? FromLocation { get; set; }
        [Column("To_location")]
        [StringLength(250)]
        public string? ToLocation { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
