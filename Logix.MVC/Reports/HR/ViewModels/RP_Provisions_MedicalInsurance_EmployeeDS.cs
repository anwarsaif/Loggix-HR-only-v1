using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
	public class RP_Provisions_MedicalInsurance_EmployeeDS
	{
		public ReportBasicDataDto? BasicData { get; set; }
		public List<HrProvisionsMedicalInsuranceEmployeeVw>? Details { get; set; }
	}
}
