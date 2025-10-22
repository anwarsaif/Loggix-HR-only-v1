using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories.HR;
using Logix.Domain.HR;
using Logix.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace Logix.Infrastructure.Repositories.HR
{
    public class HrGosiRepository : GenericRepository<HrGosi>, IHrGosiRepository
    {
        private readonly ApplicationDbContext context;

        public HrGosiRepository(ApplicationDbContext _context) : base(_context)
        {
            this.context = _context;
        }
        public async Task<IEnumerable<HRGOSIAccEntryDto>> GetGosiEmployeeAcc(long gosiId, long facilityId)
        {
            try
            {
                var getFromGosi = context.HrGosis.Where(x => x.Id == gosiId).Take(1).FirstOrDefault();


                var description = "قيد استحقاق التأمينات الإجتماعية لشهر "+ getFromGosi.TMonth+ " للعام " + getFromGosi.FinancelYear.ToString();

                // First query
                var gosiEmployeeAccQuery1 = from gosi in context.HrGosiEmployeeAccVws
                                            join salaryGroup in context.HrSalaryGroups on gosi.SalaryGroupId equals salaryGroup.Id
                                            where gosi.GosiId == gosiId && salaryGroup.IsDeleted == false
                                            select new HRGOSIAccEntryDto
                                            {
                                                AccountID = salaryGroup.AccountGosiId,
                                                Credit = 0,
                                                Debit = gosi.GosiCompany,
                                                Description = description + " " + gosi.EmpName,
                                                CC_ID = gosi.CcId,
                                                ReferenceNo = 0,
                                                ReferenceType_ID = 1
                                            };

                // Second query
                var gosiEmployeeAccQuery2 = from gosi in context.HrGosiEmployeeAccVws
                                            join salaryGroup in context.HrSalaryGroups on gosi.SalaryGroupId equals salaryGroup.Id
                                            where gosi.GosiId == gosiId && salaryGroup.IsDeleted == false
                                            select new HRGOSIAccEntryDto
                                            {
                                                AccountID = salaryGroup.AccountDueGosiId,
                                                Credit = gosi.GosiCompany,
                                                Debit = 0,
                                                Description = description + " " + gosi.EmpName,
                                                CC_ID = 0,
                                                ReferenceNo = 0,
                                                ReferenceType_ID = 1
                                            };

                // Combine and execute queries
                var combinedResults = await gosiEmployeeAccQuery1.Union(gosiEmployeeAccQuery2).ToListAsync();

                return combinedResults ??null;

            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw null;
            }
        }

        public async Task<IEnumerable<HRGOSIAccEntryDto>> GetHRGOSIAccbyCCID(long gosiId, long facilityId)
        {
            try
            {
                var getFromGosi = context.HrGosis.Where(x => x.Id == gosiId).FirstOrDefault();

                var description = "قيد استحقاق التأمينات الإجتماعية لشهر " + getFromGosi.TMonth + " للعام " + getFromGosi.FinancelYear.ToString();

                // First query
                var gosiEmployeeAccQuery1 = from gosi in context.HrGosiEmployeeAccVws
                                            join salaryGroup in context.HrSalaryGroups on gosi.SalaryGroupId equals salaryGroup.Id
                                            where gosi.GosiId == gosiId && salaryGroup.IsDeleted == false
                                            group gosi by new { salaryGroup.AccountGosiId, gosi.CcId } into grouped
                                            select new HRGOSIAccEntryDto
                                            {
                                                AccountID = grouped.Key.AccountGosiId,
                                                Credit = 0,
                                                Debit = grouped.Sum(g => g.GosiCompany),
                                                Description = description,
                                                CC_ID = grouped.Key.CcId,
                                                ReferenceNo = 0,
                                                ReferenceType_ID = 1
                                            };

                // Second query
                var gosiEmployeeAccQuery2 = from gosi in context.HrGosiEmployeeAccVws
                                            join salaryGroup in context.HrSalaryGroups on gosi.SalaryGroupId equals salaryGroup.Id
                                            where gosi.GosiId == gosiId && salaryGroup.IsDeleted == false
                                            group gosi by new { salaryGroup.AccountDueGosiId, gosi.CcId } into grouped
                                            select new HRGOSIAccEntryDto
                                            {
                                                AccountID = grouped.Key.AccountDueGosiId,
                                                Credit = grouped.Sum(g => g.GosiCompany),
                                                Debit = 0,
                                                Description = description,
                                                CC_ID = 0,
                                                ReferenceNo = 0,
                                                ReferenceType_ID = 1
                                            };

                // Combine and execute queries
                var combinedResults = await gosiEmployeeAccQuery1.Union(gosiEmployeeAccQuery2).ToListAsync();

                return combinedResults ?? null;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

        public async Task<IEnumerable<HRGOSIAccEntryDto>> GetHRGOSIAccbyReferenceTypeID(long gosiId, long facilityId)
        {
            try
            {
                // Fetching ReferenceType_ID
                var referenceTypeId = await context.SysPropertyValues
                    .Where(spv => spv.FacilityId == facilityId && spv.PropertyId == 297)
                    .Select(spv => spv.PropertyValue ?? "1")
                    .FirstOrDefaultAsync();

                // Fetching the description
                var getFromGosi = await context.HrGosis.Where(x => x.Id == gosiId).Select(x => new { x.TMonth, x.FinancelYear }).FirstOrDefaultAsync();

                var description = "قيد استحقاق التأمينات الإجتماعية لشهر " + getFromGosi.TMonth + " للعام " + getFromGosi.FinancelYear.ToString();

                // First query
                var gosiEmployeeAccQuery1 = from gosi in context.HrGosiEmployeeAccVws
                                            join salaryGroup in context.HrSalaryGroups on gosi.SalaryGroupId equals salaryGroup.Id
                                            where gosi.GosiId == gosiId && salaryGroup.IsDeleted == false
                                            group gosi by new { salaryGroup.AccountGosiId, gosi.CcId } into grouped
                                            select new HRGOSIAccEntryDto
                                            {
                                                AccountID = grouped.Key.AccountGosiId,
                                                Credit = 0,
                                                Debit = grouped.Sum(g => g.GosiCompany),
                                                Description = description,
                                                CC_ID = grouped.Key.CcId,
                                                ReferenceNo = 0,
                                                ReferenceType_ID = 1
                                            };

                // Second query
                var gosiEmployeeAccQuery2 = from gosi in context.HrGosiEmployeeAccVws
                                            join salaryGroup in context.HrSalaryGroups on gosi.SalaryGroupId equals salaryGroup.Id
                                            where gosi.GosiId == gosiId && gosi.GosiEmp > 0 && salaryGroup.IsDeleted == false
                                            group gosi by new { salaryGroup.AccountLoanId, gosi.EmpId, gosi.GosiEmp } into grouped
                                            select new HRGOSIAccEntryDto
                                            {
                                                AccountID = grouped.Key.AccountLoanId,
                                                Credit = 0,
                                                Debit = grouped.Key.GosiEmp,
                                                Description = description,
                                                CC_ID = 0,
                                                ReferenceNo = grouped.Key.EmpId,
                                                ReferenceType_ID = int.Parse(referenceTypeId)
                                            };

                // Third query
                var gosiEmployeeAccQuery3 = from gosi in context.HrGosiEmployeeAccVws
                                            join salaryGroup in context.HrSalaryGroups on gosi.SalaryGroupId equals salaryGroup.Id
                                            where gosi.GosiId == gosiId && salaryGroup.IsDeleted == false
                                            group gosi by salaryGroup.AccountDueGosiId into grouped
                                            select new HRGOSIAccEntryDto
                                            {
                                                AccountID = grouped.Key,
                                                Credit = grouped.Sum(g => g.GosiCompany + g.GosiEmp),
                                                Debit = 0,
                                                Description = description,
                                                CC_ID = 0,
                                                ReferenceNo = 0,
                                                ReferenceType_ID = 1
                                            };

                // Combine and execute queries
                var combinedResults = await gosiEmployeeAccQuery1
                    .Union(gosiEmployeeAccQuery2)
                    .Union(gosiEmployeeAccQuery3)
                    .ToListAsync();

                return combinedResults ?? null;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

    }
}
