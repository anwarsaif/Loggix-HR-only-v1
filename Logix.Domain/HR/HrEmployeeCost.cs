using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Employee_Cost")]
    public partial class HrEmployeeCost:TraceEntity
    {
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("Emp_ID")]
    public int? EmpId { get; set; }

    [Column("Cost_Type_ID")]
    public int? CostTypeId { get; set; }

    [Column("Type_Calculation")]
    public int? TypeCalculation { get; set; }

    [Column("Cost_Rate")]
    public int? CostRate { get; set; }

    [Column("Cost_Value", TypeName = "decimal(18, 3)")]
    public decimal? CostValue { get; set; }

    [Column("Trans_Date", TypeName = "datetime")]
    public DateTime? TransDate { get; set; }

    public string? Description { get; set; }

    [Column("Start_Date")]
    [StringLength(10)]
    public string? StartDate { get; set; }

    [Column("End_Date")]
    [StringLength(10)]
    public string? EndDate { get; set; }

    public bool? Active { get; set; }
    [Column("Project_ID")]
    public long? ProjectId { get; set; }
  }
}
