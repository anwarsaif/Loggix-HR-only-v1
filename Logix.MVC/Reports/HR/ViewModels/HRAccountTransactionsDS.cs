using Logix.Application.DTOs.ACC;
using Logix.Application.DTOs.HR;

namespace Logix.MVC.Reports.HR.ViewModels
{
  public class HRAccountTransactionsDS
  {
    public ReportBasicDataDto? BasicData { get; set; }
    public AccountBalanceSheetFilterDto? Filter { get; set; }
    public List<AccountBalanceSheetDto>? Details { get; set; }
  }
}
