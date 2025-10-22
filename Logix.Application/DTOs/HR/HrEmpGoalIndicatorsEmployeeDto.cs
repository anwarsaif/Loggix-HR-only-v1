namespace Logix.Application.DTOs.HR
{
    public class HrEmpGoalIndicatorsEmployeeDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public long? GoalIndicatorsId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class HrEmpGoalIndicatorsEmployeeEditDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public long? GoalIndicatorsId { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
