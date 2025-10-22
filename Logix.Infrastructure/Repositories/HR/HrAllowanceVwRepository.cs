using System.Globalization;
using Logix.Application.Common;
using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrAllowanceVwRepository : GenericRepository<HrAllowanceVw>, IHrAllowanceVwRepository
    {
        private readonly ApplicationDbContext context;

        public HrAllowanceVwRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<HrAllowanceDto>> GetAllowanceFixedAndTemporary(long? empId, int? FinancelYear, string? Month_Code, int? Attendance, int IsPrepar = 0)
        {
            try
            {
                List<HrAllowanceDto> finalAllowanceList = new List<HrAllowanceDto>();

                string Att_Start_Day = "0", Att_End_Day = "0";
                var hrSetting = await context.Set<HrSetting>().AsNoTracking().Where(x => x.FacilityId == 1).FirstOrDefaultAsync();
                if (hrSetting != null)
                {
                    Att_Start_Day = hrSetting.MonthStartDay;
                    Att_End_Day = hrSetting.MonthEndDay;
                }

                string lastDayOfMonth = "";
                if (Att_End_Day == "30")
                {
                    DateTime firstDayOfNextMonth = DateTime.ParseExact($"01-{Month_Code}-{FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture).AddMonths(1);
                    lastDayOfMonth = (firstDayOfNextMonth.AddDays(-1)).ToString("dd");
                }
                else
                {
                    lastDayOfMonth = Att_End_Day.ToString();
                }

                string Att_Start_date = "", Att_End_date = "";
                if (Att_Start_Day != "01" && Att_Start_Day != "1")
                {
                    if (Month_Code != "01" && Month_Code != "1")
                    {
                        int PrevMonth = int.Parse(Month_Code.ToString()) - 1;
                        string PrevMonth_Str = PrevMonth.ToString("D2");
                        Att_Start_date = $"{FinancelYear}/{PrevMonth_Str}/{Att_Start_Day}";
                        Att_End_date = $"{FinancelYear}/{Month_Code}/{lastDayOfMonth}";
                    }
                    else
                    {
                        int PrevFinancelYear = int.Parse(FinancelYear.ToString()) - 1;
                        string PrevFinancelYear_Str = PrevFinancelYear.ToString();
                        Att_Start_date = $"{PrevFinancelYear_Str}/12/{Att_Start_Day}";
                        Att_End_date = $"{FinancelYear}/01/{lastDayOfMonth}";
                    }
                }
                else
                {
                    Att_Start_date = $"{FinancelYear}/{Month_Code}/{Att_Start_Day}";
                    Att_End_date = $"{FinancelYear}/{Month_Code}/{lastDayOfMonth}";
                }

                string From_Date = Att_Start_date;
                string To_Date = Att_End_date;

                var getFromHrAllowanceVw = await context.Set<HrAllowanceVw>().Where(X => X.EmpId == empId && X.IsDeleted == false && X.Status == true && (IsPrepar == 0 || X.PreparationSalariesId != 0)).AsNoTracking().ToListAsync();
                if (getFromHrAllowanceVw == null)
                {
                    return finalAllowanceList;
                }
                var fixedOrTemp1 = getFromHrAllowanceVw.Where(x => x.FixedOrTemporary == 1).ToList();
                if (fixedOrTemp1 != null)
                {
                    {
                        foreach (var item in fixedOrTemp1)
                        {
                            var newItem = new HrAllowanceDto
                            {
                                FixedOrTemporary = item.FixedOrTemporary,
                                Amount = Math.Round((decimal)(item.Amount / 30) * (decimal)Attendance, 2),
                                OriginalAmount = item.Amount,
                                AdId = item.AdId,
                            };
                            finalAllowanceList.Add(newItem);
                        }
                    }
                }

                var fixedOrTemp2 = getFromHrAllowanceVw.Where(x => x.FixedOrTemporary == 2).ToList();
                if (fixedOrTemp2 != null)
                {
                    var FilteredfixedOrTemp2 = fixedOrTemp2.Where(x => x.DueDate != null && DateHelper.StringToDate(x.DueDate) >= DateHelper.StringToDate(From_Date) && DateHelper.StringToDate(x.DueDate) <= DateHelper.StringToDate(To_Date)).ToList();
                    if (FilteredfixedOrTemp2 != null)
                    {
                        foreach (var item in FilteredfixedOrTemp2)
                        {
                            var newItem = new HrAllowanceDto
                            {
                                FixedOrTemporary = item.FixedOrTemporary,
                                Amount = item.Amount,
                                OriginalAmount = item.Amount
                            };
                            finalAllowanceList.Add(newItem);
                        }
                    }
                }
                return finalAllowanceList;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}
