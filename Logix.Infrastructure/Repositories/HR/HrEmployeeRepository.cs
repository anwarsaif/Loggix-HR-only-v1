using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrEmployeeRepository : GenericRepository<HrEmployee, HrEmployeeVw>, IHrEmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public HrEmployeeRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public new async Task<HrEmployee?> GetById(int Id)
        {
            return await Task.FromResult(context.HrEmployees.Single(e => e.EmpId == Id.ToString()));
        }


        public new async Task<HrEmployee?> GetById(long Id)
        {
            return await Task.FromResult(context.HrEmployees.Single(e => e.EmpId == Id.ToString()));
        }

        public async Task<IEnumerable<HrAttShift>> GetHrAttShift(Expression<Func<HrAttShift, bool>> expression)
        {
            return await Task.FromResult(context.HrAttShifts.Where(expression));
        }

        public async Task<IEnumerable<HrJobVw>> GetHrJobVw(Expression<Func<HrJobVw, bool>> expression)
        {
            return await Task.FromResult(context.HrJobVws.Where(expression));
        }

        public async Task<IEnumerable<HrJobProgramVw>> GetHrJobProgramVw(Expression<Func<HrJobProgramVw, bool>> expression)
        {
            return await Task.FromResult(context.HrJobProgramVws.Where(expression));
        }

        public async Task<IEnumerable<HrAttendanceReportDto>> GetHrAttendanceReport(string EmpCode = "", long BranchId = 0, long TimeTableId = 0, int StatusId = 0, long Location = 0, long DeptId = 0, int AttendanceType = 0, int SponsorsId = 0)
        {
            var r = context.Set<HrAttendanceReportDto>().FromSqlRaw($"exec HR_Attendance_Report4_SP @Emp_Code={EmpCode},@BRANCH_ID={BranchId},@TimeTable_ID={TimeTableId},@Status_ID={StatusId},@Location={Location},@Dept_ID={DeptId},@Attendance_Type={AttendanceType},@Sponsors_ID={SponsorsId}").AsEnumerable().ToList();

            return r;
        }
        public async Task<long> GetEmpId(long facilityId, string EmpCode)
        {
            long EmpId = 0;
            try
            {
                await context.InvestEmployees
                .Where(a => a.EmpId == EmpCode && a.Isdel == false && a.FacilityId == facilityId)
                .Select(x => x.Id).SingleOrDefaultAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return EmpId;
        }
        public async Task<int> chkEmpid(string EmpCode)
        {
            var count = await context.HrEmployees
                .Where(j => j.IsDeleted == false && j.EmpId == EmpCode)
                .CountAsync();

            return count;
        }

        public async Task<int> CkeckEmpStatus(string EmpCode, long facilityId)
        {
            int StatusId = 0;
            try
            {
                await context.InvestEmployees
               .Where(a => a.EmpId == EmpCode && a.Isdel == false && a.FacilityId == facilityId)
               .Select(x => x.StatusId).SingleOrDefaultAsync(); ;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return StatusId;


        }


        public async Task<HrEmployee> GetEmpByCode(string EmpCode, long facilityId)
        {
            return await Task.FromResult(context.HrEmployees.Single(e => e.EmpId == EmpCode && e.FacilityId == facilityId));

        }
    }
}
