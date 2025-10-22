using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR;

[Keyless]
public partial class HrVisitScheduleLocationVw
{
    public long? Code { get; set; }

    [Column("Start_Date")]
    [StringLength(10)]
    public string? StartDate { get; set; }

    [Column("End_Date")]
    [StringLength(10)]
    public string? EndDate { get; set; }

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

    public long? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    public long? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public bool? IsDeleted { get; set; }

    [Column("Check_In", TypeName = "datetime")]
    public DateTime? CheckIn { get; set; }

    [Column("Check_Out", TypeName = "datetime")]
    public DateTime? CheckOut { get; set; }

    public string? Summary { get; set; }

    [Column("Status_ID")]
    public int? StatusId { get; set; }

    [Column("App_ID")]
    public long? AppId { get; set; }

    public bool? IsDeletedM { get; set; }

    [Column("Location_Name")]
    [StringLength(200)]
    public string LocationName { get; set; } = null!;

    [Column("Location_Code")]
    public long LocationCode { get; set; }

    [Column("latitude")]
    [StringLength(2400)]
    public string? Latitude { get; set; }

    [Column("longitude")]
    [StringLength(2400)]
    public string? Longitude { get; set; }

    [Column("Customer_name")]
    [StringLength(2500)]
    public string? CustomerName { get; set; }

    [Column("Customer_Code")]
    [StringLength(250)]
    public string? CustomerCode { get; set; }

    [Column("Status_Name")]
    [StringLength(250)]
    public string? StatusName { get; set; }

    [Column("Emp_Code")]
    [StringLength(50)]
    public string EmpCode { get; set; } = null!;

    [Column("Emp_name")]
    [StringLength(250)]
    public string? EmpName { get; set; }

    [Column("Step_ID")]
    public long? StepId { get; set; }

    [Column("Step_Name")]
    [StringLength(500)]
    public string? StepName { get; set; }

    [Column("Step_Name2")]
    [StringLength(500)]
    public string? StepName2 { get; set; }

    [Column("Groups_ID")]
    public string? GroupsId { get; set; }

    [Column("Last_Note")]
    public string? LastNote { get; set; }

    [Column("To_Step_ID")]
    public long? ToStepId { get; set; }

    [Column("Next_Step_Name")]
    [StringLength(500)]
    public string? NextStepName { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("TimeTable_Name")]
    [StringLength(50)]
    public string? TimeTableName { get; set; }

    [Column("Cur_Latitude")]
    public bool? CurLatitude { get; set; }

    [Column("Cur_longitude")]
    public bool? CurLongitude { get; set; }

    [Column("File_URL")]
    public string? FileUrl { get; set; }

    [Column("Application_Code")]
    public long? ApplicationCode { get; set; }

    [Column("Applications_Type_ID")]
    public int? ApplicationsTypeId { get; set; }
}
