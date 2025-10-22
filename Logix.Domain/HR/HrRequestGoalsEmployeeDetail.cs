using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR;

[Table("HR_Request_Goals_Employee_Details")]
public partial class HrRequestGoalsEmployeeDetail : TraceEntity
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }
    public string? Target { get; set; }
    public long? Weight { get; set; }
    [Column("Request_Goals_Emp_ID")]
    public long? RequestGoalsEmpId { get; set; }
    [Column(TypeName = "decimal(16, 2)")]
    public decimal? TargetValue { get; set; }
}
