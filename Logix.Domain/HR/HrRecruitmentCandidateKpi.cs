using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Recruitment_Candidate_KPI")]
    public partial class HrRecruitmentCandidateKpi
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Candidate_ID")]
        public long? CandidateId { get; set; }
        [Column("KPI_Tem_ID")]
        public int? KpiTemId { get; set; }
        [Column("Eva_Date")]
        [StringLength(50)]
        public string? EvaDate { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        [Column("App_ID")]
        public long? AppId { get; set; }
        [Column("Recru_Applicant_ID")]
        public long? RecruApplicantId { get; set; }
    }
}
