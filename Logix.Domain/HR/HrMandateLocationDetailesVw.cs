using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrMandateLocationDetailesVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("ML_ID")]
        public long? MlId { get; set; }
        public string? Name { get; set; }
        public string? Name2 { get; set; }
        [Column("ML_Note")]
        public string? MlNote { get; set; }
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
        [Column("Allowance_value", TypeName = "decimal(18, 2)")]
        public decimal? AllowanceValue { get; set; }
        [Column("Houseing_IsSecured")]
        public bool? HouseingIsSecured { get; set; }
        [Column("Rate_per_night", TypeName = "decimal(18, 2)")]
        public decimal? RatePerNight { get; set; }
        [Column("Transport_IsInsured")]
        public bool? TransportIsInsured { get; set; }
        [Column("Transport_Amount", TypeName = "decimal(18, 2)")]
        public decimal? TransportAmount { get; set; }
        [Column("Ticket_Value", TypeName = "decimal(18, 2)")]
        public decimal? TicketValue { get; set; }
        public string? Note { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Job_Level_ID")]
        public long? JobLevelId { get; set; }
        [Column("Level_Name")]
        [StringLength(250)]
        public string? LevelName { get; set; }
        [Column("Level_Min_Salary", TypeName = "decimal(18, 2)")]
        public decimal? LevelMinSalary { get; set; }
        [Column("Level_Max_Salary", TypeName = "decimal(18, 2)")]
        public decimal? LevelMaxSalary { get; set; }
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
    }
}
