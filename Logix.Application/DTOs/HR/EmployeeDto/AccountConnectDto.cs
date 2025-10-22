using Castle.MicroKernel.SubSystems.Conversion;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.HR.EmployeeDto
{
    public class AccountConnectDto
    {
        public long Id { get; set; }


        // مجموعة الرواتب 
        public long? SalaryGroupId { get; set; }

        // الشركة
        public int? FacilityId { get; set; }


        //حساب السلف
        public long? AccountId { get; set; }
        public string? AccAccountCode { get; set; }


        //رقم مركز التكلفة1 
        public string? CcId { get; set; }
        //رقم مركز التكلفة 2 

        public string? CcId2 { get; set; }
        //رقم مركز التكلفة3 
        public string? CcId3 { get; set; }
        //رقم مركز التكلفة 4
        public string? CcId4 { get; set; }

        //رقم مركز التكلفة  5 
        public string? CcId5 { get; set; }

        // نسبه تكلفة1 
        public decimal? CcRate { get; set; }
        // نسبه تكلفة2 


        public decimal? CcRate2 { get; set; }
        // نسبه تكلفة3 

        public decimal? CcRate3 { get; set; }
        // نسبه تكلفة4 

        public decimal? CcRate4 { get; set; }
        // نسبه تكلفة5 

        public decimal? CcRate5 { get; set; }



        //اسم مركز التكلفة 1
        public string? CcName1 { get; set; }
        // 2 اسم مركز التكلفة

        public string? CcName2 { get; set; }
        // 3 اسم مركز التكلفة

        public string? CcName3 { get; set; }
        //اسم مركز التكلفة 4

        public string? CcName4 { get; set; }
        //اسم مركز التكلفة 5

        public string? CcName5 { get; set; }

    }

}
