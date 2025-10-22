
using Logix.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrVacationsTypeDto 
    {
        public int VacationTypeId { get; set; }
        public string? VacationTypeName { get; set; }
        public decimal? VacationTypeMinmam { get; set; }

        public decimal? VacationTypeMaxmam { get; set; }
    
        public int? EmpTypeId { get; set; }

        public string? SsCode { get; set; }

        public decimal? DeductionDays { get; set; }

        public bool? ValidateBalance { get; set; }
        public int? CatId { get; set; }
    
        public string? VacationTypeName2 { get; set; }

        public bool? WeekendInclude { get; set; }

        public bool? DeductedServicePeriod { get; set; }

        public bool? DeductedBalanceVacation { get; set; }
        public int? limitdays { get; set; }
     

        public bool? NeedJoinRequest { get; set; }

        public bool? AttachRequired { get; set; }
        public bool? AlternativeEmpRequired { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }

    }
    public class HrVacationsTypeEditDto 
    {
		public int VacationTypeId { get; set; }
		public string? VacationTypeName { get; set; }
		public decimal? VacationTypeMinmam { get; set; }

		public decimal? VacationTypeMaxmam { get; set; }

		public int? EmpTypeId { get; set; }

		public string? SsCode { get; set; }

		public decimal? DeductionDays { get; set; }

		public bool? ValidateBalance { get; set; }
		public int? CatId { get; set; }

		public string? VacationTypeName2 { get; set; }

		public bool? WeekendInclude { get; set; }

		public bool? DeductedServicePeriod { get; set; }

		public bool? DeductedBalanceVacation { get; set; }
		public int? limitdays { get; set; }


		public bool? NeedJoinRequest { get; set; }

		public bool? AttachRequired { get; set; }
		public bool? AlternativeEmpRequired { get; set; }
		public long? ModifiedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

	}

    public class HrVacationsTypeEditVacationPoliciesDto
    {
        public int VacationTypeId { get; set; }
        public int? RateType { get; set; }
        public bool? SalaryBasic { get; set; }
        public string? Allowance { get; set; }
        public string? Deduction { get; set; }

    }

}
