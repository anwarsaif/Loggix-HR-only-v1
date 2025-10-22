using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System.Linq;

namespace Logix.Application.Services.HR
{
    public class HrPolicyService : GenericQueryService<HrPolicy, HrPolicyDto, HrPoliciesVw>, IHrPolicyService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;


        public HrPolicyService(IQueryRepository<HrPolicy> queryRepository, IHrRepositoryManager hrRepositoryManager, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this.hrRepositoryManager = hrRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrPolicyDto>> Add(HrPolicyDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrPolicyDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {

                var item = _mapper.Map<HrPolicy>(entity);
                var newEntity = await hrRepositoryManager.HrPolicyRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrPolicyDto>(newEntity);


                return await Result<HrPolicyDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<HrPolicyDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrPolicyRepository.GetById(Id);
            if (item == null) return Result<HrPolicyDto>.Fail($"--- there is no Data with this id: {Id}---");
            hrRepositoryManager.HrPolicyRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrPolicyDto>.SuccessAsync(_mapper.Map<HrPolicyDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrPolicyDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrPolicyRepository.GetById(Id);
            if (item == null) return Result<HrPolicyDto>.Fail($"--- there is no Data with this id: {Id}---");
            hrRepositoryManager.HrPolicyRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrPolicyDto>.SuccessAsync(_mapper.Map<HrPolicyDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrPolicyDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrPolicyEditDto>> Update(HrPolicyEditDto entity, CancellationToken cancellationToken = default)
        {

            if (entity == null) return await Result<HrPolicyEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

            try
            {
                var GetHRPolicies = await hrRepositoryManager.HrPolicyRepository.GetAll(x => x.IsDeleted == false && x.PolicieId == entity.PolicieId);
                if (GetHRPolicies.Any())
                {
                    var item = await hrRepositoryManager.HrPolicyRepository.GetById(entity.Id);

                    if (item == null) return await Result<HrPolicyEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
                    item.ModifiedOn = DateTime.Now;
                    item.ModifiedBy = (int)session.UserId;
                    item.FacilityId = session.FacilityId;
                    item.IsDeleted = false;
                    item.RateType = entity.RateType;
                    item.PolicieId = entity.Id;
                    item.Salary = entity.Salary;
                    item.Allawance = entity.Allawance;
                    item.Deductions = entity.Deductions;
                    item.TotalRate = 0;
                    item.SalaryRate = 0;
                    hrRepositoryManager.HrPolicyRepository.Update(item);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    return await Result<HrPolicyEditDto>.SuccessAsync(_mapper.Map<HrPolicyEditDto>(item),localization.GetResource1("UpdateSuccess"));


                }
                else
                {
                    var item =new  HrPolicy();
                    item.CreatedOn = DateTime.Now;
                    item.CreatedBy = (int)session.UserId;
                    item.FacilityId = session.FacilityId;
                    item.IsDeleted = false;
                    item.RateType = entity.RateType;
                    item.Salary = entity.Salary;
                    item.Allawance = entity.Allawance;
                    item.Deductions = entity.Deductions;
                    item.PolicieId = entity.Id;
                    item.TotalRate = 0;
                    item.SalaryRate = 0;
                    var newEntity = await hrRepositoryManager.HrPolicyRepository.AddAndReturn(item);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    var entityMap = _mapper.Map<HrPolicyEditDto>(newEntity);


                    return await Result<HrPolicyEditDto>.SuccessAsync(entityMap, localization.GetResource1("UpdateSuccess"));

                }
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrPolicyEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        //public async Task<decimal> ApplyPoliciesAsync(long facilityId, long policyId, long? empId)
        //{
        //    // Initialize amounts
        //    decimal totalDeductionAmount = 0;
        //    decimal totalAllowanceAmount = 0;
        //    decimal totalDeductionCustomAmount = 0;
        //    decimal totalAllawanceCustomAmount = 0;

        //    // Fetch policy details
        //    var policy = await hrRepositoryManager.HrPolicyRepository.GetOne(p => p.FacilityId == facilityId && p.IsDeleted == false && p.PolicieId == policyId);
        //    if (policy == null)
        //    {
        //        // Default settings based on policyId
        //        int defaultRateType = policyId switch
        //        {
        //            2 => 1,
        //            3 => 2,
        //            4 => 3,
        //            5 => 2,
        //            8 => 1,
        //            _ => throw new Exception("Invalid Policy ID")
        //        };

        //        // Create a default policy object
        //        policy = new HrPolicy
        //        {
        //            RateType = defaultRateType
        //        };
        //    }

        //    // Fetch employee salary
        //    var employee = await hrRepositoryManager.HrEmployeeRepository.GetOne(e => e.Id == empId);
        //    if (employee == null)
        //    {
        //        throw new Exception("Employee not found.");
        //    }

        //    // Calculate total allowance and deductions
        //    var totalDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(ad => ad.IsDeleted == false && ad.EmpId == empId && ad.FixedOrTemporary == 1 && ad.TypeId == 2);
        //    totalDeductionAmount = totalDeduction.Sum(x => x.Amount) ?? 0;

        //    var totalAllawance = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(ad => ad.IsDeleted == false && ad.EmpId == empId && ad.FixedOrTemporary == 1 && ad.TypeId == 1);
        //    totalAllowanceAmount = totalAllawance.Sum(x => x.Amount) ?? 0;

        //    // Calculate custom allowance and deductions
        //    if (!string.IsNullOrEmpty(policy.Deductions))
        //    {
        //        var deductionIds = policy.Deductions.Split(",").Select(int.Parse).ToList();
        //        var totalDeductionCustom = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(ad => ad.IsDeleted == false && ad.EmpId == empId && ad.FixedOrTemporary == 1 && ad.TypeId == 2 && deductionIds.Contains((int)ad.AdId));
        //        totalDeductionCustomAmount = totalDeductionCustom.Sum(x => x.Amount) ?? 0;
        //    }

        //    if (!string.IsNullOrEmpty(policy.Allawance))
        //    {
        //        var allowanceIds = policy.Allawance.Split(",").Select(int.Parse).ToList();
        //        var totalAllawanceCustom = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(ad => ad.IsDeleted == false && ad.EmpId == empId && ad.FixedOrTemporary == 1 && ad.TypeId == 1 && allowanceIds.Contains((int)ad.AdId));
        //        totalAllawanceCustomAmount = totalAllawanceCustom.Sum(x => x.Amount) ?? 0;
        //    }

        //    // Calculate return amount based on rate type
        //    decimal retAmount = policy.RateType switch
        //    {
        //        1 => employee.Salary ?? 0,
        //        2 => (employee.Salary ?? 0) + totalAllowanceAmount,
        //        3 => (employee.Salary ?? 0) + totalAllowanceAmount - totalDeductionAmount,
        //        4 => (policy.Salary == true ? (employee.Salary ?? 0) : 0) + totalAllawanceCustomAmount - totalDeductionCustomAmount,
        //        _ => throw new Exception("Invalid Rate Type")
        //    };

        //    return retAmount;
        //}


    }

}