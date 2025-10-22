using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.HR.EmployeeDto
{
    public class EmployeeJobInfoDto
    {
        public long Id { get; set; }
        public int? JobType { get; set; }

        [Column("Apply_Salary_ladder")]
        public bool? ApplySalaryLadder { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("Job_Catagories_ID")]
        public int? JobCatagoriesId { get; set; }

        [Column("Status_ID")]
        public int? StatusId { get; set; }

        [Column("Qualification_ID")]
        public int? QualificationId { get; set; }

        [Column("Specialization_ID")]
        public int? SpecializationId { get; set; }

        [Column("DOAppointmentold")]
        [StringLength(50)]
        public string? Doappointmentold { get; set; }

        [Column("Dept_ID")]
        public int? DeptId { get; set; }

        public int? Location { get; set; }

        [Column("Program_ID")]
        public int? ProgramId { get; set; }

        [Column("Manager_ID")]
        public long? ManagerId { get; set; }

        [Column("Manager2_ID")]
        public long? Manager2Id { get; set; }

        [Column("Manager3_ID")]
        public long? Manager3Id { get; set; }

        [Column("Level_ID")]
        public int? LevelId { get; set; }
        public int? DegreeId { get; set; }
        public string? JobNo { get; set; }
        public int? AnnualIncreaseMethod { get; set; }
        public string? JobDescription { get; set; }


    }

}
