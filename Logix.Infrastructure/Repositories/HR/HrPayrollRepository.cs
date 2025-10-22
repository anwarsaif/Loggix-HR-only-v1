using Logix.Application.Common;
using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Application.Wrapper;
using Logix.Domain.Hr;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Logix.Infrastructure.Repositories;
using System.Globalization;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrPayrollRepository : GenericRepository<HrPayroll>, IHrPayrollRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ICurrentData session;

        public HrPayrollRepository(ApplicationDbContext context, ICurrentData session) : base(context)
        {
            this.context = context;
            this.session = session;
        }
        public async Task<HrPayroll> AddPayroll(HrPayroll hrPayroll, CancellationToken cancellationToken)
        {

            // Begin Of Add To HrPayroll
            string? AttEndDate;
            string? AttStartDate;
            string? AttStartDay;
            string? AttEndDay;
            string? PrevMonthStr;
            string? PrevFinancelYearStr;
            //  جلب بداية ونهاية الشهر من اعدادات الموارد البشرية
            var getFromHrSetting = context.HrSettings.FirstOrDefault(x => x.FacilityId == hrPayroll.FacilityId);
            AttStartDay = getFromHrSetting.MonthStartDay;
            AttEndDay = getFromHrSetting.MonthEndDay;
            //اذا كان شهر واحد يتم احتساب من شهر 12 للسنة الماضية اما غير الشهور يتم الإحتساب من الشهر السابق

            if (new[] { "29", "30", "31" }.Contains(AttEndDay))
            {
                if (hrPayroll.MsMonth == "02" || hrPayroll.MsMonth == "2")
                {
                    DateTime date = DateTime.ParseExact($"01-{hrPayroll.MsMonth}-{hrPayroll.FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                    AttEndDay = previousMonthEndDate.ToString("dd");
                }
            }
            if (AttEndDay == "31")
            {
                if (new[] { "04", "06", "09", "11" }.Contains(hrPayroll.MsMonth))
                {
                    DateTime date = DateTime.ParseExact($"01-{hrPayroll.MsMonth}-{hrPayroll.FinancelYear}", "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    DateTime previousMonthEndDate = date.AddMonths(1).AddDays(-1);
                    AttEndDay = previousMonthEndDate.ToString("dd");
                }
            }


            if (Convert.ToInt32(AttStartDay) != 1 && Convert.ToInt32(AttStartDay) != 01)
            {
                if (Convert.ToInt32(hrPayroll.MsMonth) != 1 && Convert.ToInt32(hrPayroll.MsMonth) != 01)
                {
                    int PrevMonth = int.Parse(hrPayroll.MsMonth) - 1;
                    PrevMonthStr = PrevMonth.ToString("D2");
                    AttStartDate = $"{hrPayroll.FinancelYear}/{PrevMonthStr}/{AttStartDay}";
                    AttEndDate = $"{hrPayroll.FinancelYear}/{hrPayroll.MsMonth}/{AttEndDay}";
                }
                else
                {
                    int PrevFinancelYear = int.Parse(hrPayroll.FinancelYear.ToString()) - 1;
                    PrevFinancelYearStr = PrevFinancelYear.ToString();
                    AttStartDate = $"{PrevFinancelYearStr}/12/{AttStartDay}";
                    AttEndDate = $"{hrPayroll.FinancelYear}/01/{AttEndDay}";
                }
            }
            else
            {
                AttStartDate = $"{hrPayroll.FinancelYear}/{hrPayroll.MsMonth}/{AttStartDay}";
                AttEndDate = $"{hrPayroll.FinancelYear}/{hrPayroll.MsMonth}/{AttEndDay}";
            }

            var MsMonthTxt = DateHelper.GetMonthName(Convert.ToInt32(hrPayroll.MsMonth));

            var getMaxCode = context.HrPayrolls.Max(x => x.MsCode);
            var newPayrollEntity = new HrPayroll
            {
                MsCode = getMaxCode + 1,
                MsDate = hrPayroll.MsDate,
                MsTitle = hrPayroll.MsTitle,
                MsMonth = hrPayroll.MsMonth,
                MsMothTxt = MsMonthTxt,
                FinancelYear = Convert.ToInt32(hrPayroll.FinancelYear),
                State = hrPayroll.State,
                CreatedBy = session.UserId,
                CreatedOn = DateTime.Now,
                FacilityId = Convert.ToInt32(hrPayroll.FacilityId),
                PayrollTypeId = hrPayroll.PayrollTypeId,
                StartDate = AttStartDate,
                EndDate = AttEndDate,
                BranchId = hrPayroll.BranchId,
                AppId = 0,
            };

            var AddedPayrollEntity = (await context.HrPayrolls.AddAsync(newPayrollEntity, cancellationToken)).Entity;
            await context.SaveChangesAsync(cancellationToken);
            return AddedPayrollEntity;
        }


        public async Task ChangeStatusPayrollTrans(HrPayrollNote hrPayrollNote, CancellationToken cancellationToken)
        {
            // Begin ChangeStatus_Payroll_Trans



            var newHrPayrollNote = new HrPayrollNote
            {
                MsId = hrPayrollNote.MsId,
                StateId = hrPayrollNote.StateId,
                Note = hrPayrollNote.Note,
                CreatedBy = session.UserId,
                CreatedOn = DateTime.Now,
            };
            var AddedPayrollNoteEntity = await context.HrPayrollNotes.AddAsync(newHrPayrollNote);
            await context.SaveChangesAsync(cancellationToken);
            //كما هو موجود بال sp
            var ms = context.HrPayrolls.FirstOrDefault(x => x.MsId == hrPayrollNote.MsId);
            if (ms == null) 
                throw new Exception("Payroll not found");
            ms.State = newHrPayrollNote.StateId;
            ms.ModifiedBy = newHrPayrollNote.CreatedBy;
            ms.ModifiedOn = newHrPayrollNote.CreatedOn;
            context.HrPayrolls.Update(ms);

            // تحديث السلف الى مدفوعة بعد اعتماد المسير
            if (hrPayrollNote.StateId == 4)
            {
                var LoanInstallmentID = context.HrLoanInstallmentPayments.Where(x => x.PayrollId == hrPayrollNote.MsId && x.IsDeleted == false).Select(x=>x.Id);
                var getLoanInstallment = context.HrLoanInstallments.Where(x => LoanInstallmentID.Contains(x.Id));

                if (getLoanInstallment.Count() > 0)
                {
                    foreach (var singleLoanInstallment in getLoanInstallment)
                    {
                        singleLoanInstallment.IsPaid = true;
                        singleLoanInstallment.ModifiedBy = session.UserId;
                        singleLoanInstallment.ModifiedOn = DateTime.Now;
                    }
                    context.HrLoanInstallments.UpdateRange(getLoanInstallment);
                    await context.SaveChangesAsync(cancellationToken);
                }
            }

            // End ChangeStatus_Payroll_Trans
        }
    }

}
