
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrSettingDto
    {
        public long Id { get; set; }
        
        public long? FacilityId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        
        [Required]
        public decimal? OverTime { get; set; }
        
        public int? ApplyAttDisciplinary { get; set; }
        
        public int? ApplyAttDelay { get; set; }
       
        [StringLength(2)]
        
        public string? MonthStartDay { get; set; }
       
        [StringLength(2)]
        
        public string? MonthEndDay { get; set; }
        
        public int? AppAbsenceDisciplinary { get; set; }

        
        public int? HousingAllowance { get; set; }
        
        public int? TransportAllowance { get; set; }
        
        public int? MobileAllowance { get; set; }

        
        public int? PrevMonthAllowance { get; set; }

        
        public int? BonusesAllowance { get; set; }

     
        public int? BadalatAllowance { get; set; }

       
        public int? FoodAllowance { get; set; }



        
        public int? GosiDeduction { get; set; }

        
        public int? MororDeduction { get; set; }

        
        public int? HousingDeduction { get; set; }
        
        public int? MobileDeduction { get; set; }
        public int? UpdetDepLocExl { get; set; }
        
        public int? OtherDeduction { get; set; }
        public int? PenaltiesDeduction { get; set; }

        
        public int? VacationDueAllowance { get; set; }

        
        public int? LeaveBenefitsAllowance { get; set; }
        
        public int? TicketAllowance { get; set; }

        
        public int? LeaveDeduction { get; set; }

        
        public int? ApplyShortfall { get; set; }
        
        public int? ApplyingDisciplinaryShortfall { get; set; }
        public List<HrPayrollTransactionTypeValueDto> hrPayrollTransactionTypeValues { get; set; }
    }
}
