using Castle.MicroKernel.SubSystems.Conversion;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Logix.Application.DTOs.WF;
using Logix.Domain.HR;

namespace Logix.Application.DTOs.HR
{
    public class HrRecruitmentCandidateKpiDto
    {
        public long Id { get; set; }
        public long? CandidateId { get; set; }
        public int? KpiTemId { get; set; }
        public string? EvaDate { get; set; }
        public int? StatusId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public long? AppId { get; set; }
        public long? RecruApplicantId { get; set; }
    }

    public class HrRecruitmentCandidateKpiEditDto
    {
        public long Id { get; set; }
        public long? CandidateId { get; set; }
        public int? KpiTemId { get; set; }
        public string? EvaDate { get; set; }
        public int? StatusId { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? AppId { get; set; }
        public long? RecruApplicantId { get; set; }
    }

    public partial class HrRecruitmentCandidateKpiFilterDto
    {
        public long? CandidateId { get; set; }
        public string? CandidateName { get; set; }
        public int? KpiTemId { get; set; }
        public string? EvaDate { get; set; }
        public string? EvaDateTo { get; set; }

        public long Id { get; set; }
        public decimal? EvaValue { get; set; }
        public decimal? EvaDegree { get; set; }
        public string? TemName { get; set; }
        public string? VacancyName { get; set; }

    }

    public class HrCandidateKPIDtoForOperations
    {

        public int KpiId { get; set; }
        public long? CandidateId { get; set; }
        public int? KpiTemId { get; set; }
        public string? EvaDate { get; set; }
        // لايتم ادخاله من الواجهة لانه في البداية ثابت ويساوي 2

        public int? StatusId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public long? AppId { get; set; }
        public long? RecruApplicantId { get; set; }
        public long? ApplicantsId { get; set; }
        public string? ApplicationDate { get; set; }
        public int? ApplicationsTypeId { get; set; }
        // لايتم ادخاله من الواجهة لانه في البداية ثابت ويساوي 1
        public int? StepId { get; set; }
        public List<HrRecruitmentCandidateKpiDDto>? candidateKpiDDtos{ get; set; }
    }

    public class HrCandidateKPIDtoForGetById
    {
        public long? CandidateId { get; set; }
        public string? CandidateName { get; set; }
        public string? VacancyName { get; set; }
        public int? Id { get; set; }
        public int? KpiTemId { get; set; }
        public string? EvaDate { get; set; }
        // لايتم ادخاله من الواجهة لانه في البداية ثابت ويساوي 2

        public int? StatusId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public long? AppId { get; set; }
        public long? RecruApplicantId { get; set; }
        public long? ApplicantsId { get; set; }
        public string? ApplicationDate { get; set; }
        public int? ApplicationsTypeId { get; set; }
        // لايتم ادخاله من الواجهة لانه في البداية ثابت ويساوي 1
        public int? StepId { get; set; }
        public List<HrRecruitmentCandidateKpiDVw>? KpiDVw { get; set; }
    }



}
