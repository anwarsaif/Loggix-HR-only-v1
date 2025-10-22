using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Recruitment_Candidate_KPI_D")]
    public partial class HrRecruitmentCandidateKpiD
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("KPI_ID")]
        public long? KpiId { get; set; }
        [Column("KPI_Tem_Com_ID")]
        [StringLength(10)]
        public string? KpiTemComId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Degree { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
