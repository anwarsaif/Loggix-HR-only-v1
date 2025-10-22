using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR;

[Table("HR_Visit_Schedule_Location")]
public partial class HrVisitScheduleLocation : TraceEntity
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("Visit_Schedule_ID")]
    public long? VisitScheduleId { get; set; }

    [Column("Emp_ID")]
    public long? EmpId { get; set; }

    [Column("Customer_ID")]
    public long? CustomerId { get; set; }

    [Column("Location_ID")]
    public long? LocationId { get; set; }

    [Column("Shift_ID")]
    public long? ShiftId { get; set; }

    [Column("Start_Time")]
    [StringLength(10)]
    public string? StartTime { get; set; }

    [Column("End_Time")]
    [StringLength(10)]
    public string? EndTime { get; set; }

    public string? Note { get; set; }

    [Column("Check_In", TypeName = "datetime")]
    public DateTime? CheckIn { get; set; }

    [Column("Check_Out", TypeName = "datetime")]
    public DateTime? CheckOut { get; set; }

    public string? Summary { get; set; }

    [Column("Status_ID")]
    public int? StatusId { get; set; }

    [Column("App_ID")]
    public long? AppId { get; set; }

    [Column("Step_ID")]
    public long? StepId { get; set; }

    [Column("Last_Note")]
    public string? LastNote { get; set; }

    [Column("Cur_longitude")]
    public bool? CurLongitude { get; set; }

    [Column("Cur_Latitude")]
    public bool? CurLatitude { get; set; }

    [Column("File_URL")]
    public string? FileUrl { get; set; }
}
