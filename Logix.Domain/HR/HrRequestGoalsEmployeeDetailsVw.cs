using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR;

[Keyless]
public partial class HrRequestGoalsEmployeeDetailsVw
{
    [Column("ID")]
    public long Id { get; set; }

    public string? Target { get; set; }

    public long? Weight { get; set; }

    [Column("App_ID")]
    public long? AppId { get; set; }

    [StringLength(255)]
    public string? Title { get; set; }

    [StringLength(10)]
    public string? StartDate { get; set; }

    [StringLength(10)]
    public string? EndDate { get; set; }

    public int? MaxGoals { get; set; }

    public int? MinGoals { get; set; }

    [StringLength(10)]
    public string? LastRegistrationDate { get; set; }

    public string? Instructions { get; set; }

    [Column("Emp_ID")]
    public long? EmpId { get; set; }

    [Column("Emp_Code")]
    [StringLength(50)]
    public string EmpCode { get; set; } = null!;

    [Column("Emp_name")]
    [StringLength(250)]
    public string? EmpName { get; set; }

    public bool? IsDeleted { get; set; }

    [Column("Facility_ID")]
    public long FacilityId { get; set; }

    [Column("Request_Goals_Emp_ID")]
    public long? RequestGoalsEmpId { get; set; }

    [Column("Request_Goals_ID")]
    public long? RequestGoalsId { get; set; }

    [Column(TypeName = "decimal(16, 2)")]
    public decimal? TargetValue { get; set; }
}
