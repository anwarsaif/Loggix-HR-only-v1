using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrMandateRequestsMasterVw
    {
        [Column("ID")]
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Name2 { get; set; }
        public string? Note { get; set; }
        [Column("Type_ID")]
        public long? TypeId { get; set; }
        [Column("Type_Name")]
        [StringLength(250)]
        public string? TypeName { get; set; }
        [Column("Type_Name2")]
        [StringLength(250)]
        public string? TypeName2 { get; set; }
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
        public bool? IsDeleted { get; set; }
        [Column("Country_Classification_ID")]
        public long? CountryClassificationId { get; set; }
        [Column("Country_Classification_Name")]
        [StringLength(250)]
        public string? CountryClassificationName { get; set; }
        [Column("Country_Classification_Name2")]
        [StringLength(250)]
        public string? CountryClassificationName2 { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        [Column("Mandate_Location_ID")]
        public long? MandateLocationId { get; set; }
        [Column("App_ID")]
        public long? AppId { get; set; }
        [Column("From_Date", TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        [Column("To_Date", TypeName = "datetime")]
        public DateTime? ToDate { get; set; }
        public string? Objective { get; set; }
    }
}
