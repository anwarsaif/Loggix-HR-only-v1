using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrPerformanceVw
    {
        [Column("ID")]
        public long Id { get; set; }
        public string? Description { get; set; }
        [Column("Type_ID")]
        public long? TypeId { get; set; }
        [Column("Start_Date")]
        [StringLength(10)]
        public string? StartDate { get; set; }
        [Column("End_Date")]
        [StringLength(10)]
        public string? EndDate { get; set; }
        [Column("Due_Date")]
        [StringLength(10)]
        public string? DueDate { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Status_ID")]
        public long? StatusId { get; set; }
        [Column("Groups_ID")]
        public string? GroupsId { get; set; }
        [Column("Evaluation_For")]
        public long? EvaluationFor { get; set; }
        [Column("Type_Name")]
        [StringLength(250)]
        public string? TypeName { get; set; }
        [Column("Status_Name")]
        [StringLength(250)]
        public string? StatusName { get; set; }
        [Column("Evaluation_Name")]
        [StringLength(250)]
        public string? EvaluationName { get; set; }
        [Column("Evaluation_Name2")]
        [StringLength(250)]
        public string? EvaluationName2 { get; set; }
        [Column("Status_Name2")]
        [StringLength(250)]
        public string? StatusName2 { get; set; }
        [Column("Type_Name2")]
        [StringLength(250)]
        public string? TypeName2 { get; set; }
        [Column("Departments_Ids")]
        public string? DepartmentsIds { get; set; }
        [Column("Location_Ids")]
        public string? LocationIds { get; set; }
        [Column("JobsCat_Ids")]
        public string? JobsCatIds { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
    }
}
