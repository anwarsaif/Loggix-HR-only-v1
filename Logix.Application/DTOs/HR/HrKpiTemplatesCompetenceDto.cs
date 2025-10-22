using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Application.DTOs.HR
{
    public class HrKpiTemplatesCompetenceDto
    {
        public long Id { get; set; }
        public string? Subject { get; set; }
        public long? CompetencesId { get; set; }
        public string? Description { get; set; }
        public int? Score { get; set; }
        public int? TemplateId { get; set; }

        public bool? IsDeleted { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Target { get; set; }
    }

    public class HrKpiTemplatesCompetenceEditDto
    {
        public long Id { get; set; }
        public string? Subject { get; set; }
        public long? CompetencesId { get; set; }
        public string? Description { get; set; }
        public int? Score { get; set; }
        public int? TemplateId { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Target { get; set; }
    }

}
