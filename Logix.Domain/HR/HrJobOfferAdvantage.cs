using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Job_Offer_Advantages", Schema = "dbo")]
    public partial class HrJobOfferAdvantage
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Recru_Applicant_ID")]
        public long? RecruApplicantId { get; set; }
        [Column("Job_Offer_ID")]
        public long? JobOfferId { get; set; }
        [Column("Advantages_ID")]
        public long? AdvantagesId { get; set; }
        [Column("Advantages_Name")]
        public string? AdvantagesName { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
