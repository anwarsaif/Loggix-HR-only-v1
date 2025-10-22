using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using Logix.Domain.Main;
using System.Reflection.Emit;
using System.Security;

namespace Logix.Application.Services.HR
{
    public class HrEmployeeCostService : GenericQueryService<HrEmployeeCost, HrEmployeeCostDto, HrEmployeeCostVw>, IHrEmployeeCostService
    {
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly ISysConfigurationAppHelper sysConfigurationAppHelper;
        private readonly IPMRepositoryManager pMRepositoryManager;
        public HrEmployeeCostService(IQueryRepository<HrEmployeeCost> queryRepository, IMainRepositoryManager mainRepositoryManager, ILocalizationService localization, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ISysConfigurationAppHelper sysConfigurationAppHelper, IPMRepositoryManager pMRepositoryManager) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
            this.sysConfigurationAppHelper = sysConfigurationAppHelper;
            this.pMRepositoryManager = pMRepositoryManager;
        }

        #region شاشة الاضافة


        public Task<IResult<HrEmployeeCostDto>> Add(HrEmployeeCostDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<string>> Add(HrEmployeeCostDataDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                int NationalityID = 0;
                long ProjectId = 0;

                if (!(string.IsNullOrEmpty(entity.ProjectCod)))
                {
                    var GetProjectId = await pMRepositoryManager.PMProjectsRepository.GetPMProjectIdByCode(entity.ProjectCod, session.FacilityId);

                    if (GetProjectId != null)
                        ProjectId = GetProjectId.ProjectId;
                }
                var investEmployees = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == entity.EmpCode && e.IsDeleted == false && e.Isdel == false);
                if (investEmployees == null)
                {
                    return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                }
                NationalityID = Convert.ToInt32((await sysConfigurationAppHelper.GetValue(98, session.FacilityId)));


                if (entity.costType != null)
                {
                    foreach (var item in entity.costType)
                    {
                        //في حالة تطبيق على سعوديين
                        if (item.TypeNationality == 1 && entity.NationalityId == NationalityID)
                        {
                            var checkIfCostExist = await hrRepositoryManager.HrEmployeeCostRepository.GetOne(x => x.IsDeleted == false && x.EmpId == Convert.ToInt32(investEmployees.Id) && x.CostTypeId == item.TypeId);


                            if (checkIfCostExist == null)
                            {
                                var neCostItem = new HrEmployeeCost
                                {
                                    CostRate = (int?)item.CalculationRate,
                                    CostTypeId = item.TypeId,
                                    TypeCalculation = item.TypeCalculation,
                                    CostValue = item.CalculationValue,
                                    Description = item.Deduction,
                                    Active = item.ChkActive,
                                    CreatedBy = session.UserId,
                                    CreatedOn = DateTime.Now,
                                    ProjectId = ProjectId

                                };

                                await hrRepositoryManager.HrEmployeeCostRepository.Add(neCostItem);
                                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                            }
                            else
                            {
                                checkIfCostExist.CostRate = (int?)item.CalculationRate;
                                checkIfCostExist.CostValue = item.CalculationValue;
                                checkIfCostExist.Description = item.Description;
                                checkIfCostExist.Active = item.ChkActive;
                                checkIfCostExist.ModifiedBy = session.UserId;
                                checkIfCostExist.ModifiedOn = DateTime.Now;
                                checkIfCostExist.ProjectId = ProjectId;
                                hrRepositoryManager.HrEmployeeCostRepository.Update(checkIfCostExist);
                                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                            }
                        }
                        // في حالة تطبيق على المقييمين
                        else if (item.TypeNationality == 2 && entity.NationalityId != NationalityID)
                        {
                            var checkIfCostExist = await hrRepositoryManager.HrEmployeeCostRepository.GetOne(x => x.IsDeleted == false && x.EmpId == Convert.ToInt32(investEmployees.Id) && x.CostTypeId == item.TypeId);


                            if (checkIfCostExist == null)
                            {
                                var neCostItem = new HrEmployeeCost
                                {
                                    CostRate = (int?)item.CalculationRate,
                                    CostTypeId = item.TypeId,
                                    TypeCalculation = item.TypeCalculation,
                                    CostValue = item.CalculationValue,
                                    Description = item.Deduction,
                                    Active = item.ChkActive,
                                    CreatedBy = session.UserId,
                                    CreatedOn = DateTime.Now,
                                    ProjectId = ProjectId

                                };



                                await hrRepositoryManager.HrEmployeeCostRepository.Add(neCostItem);
                                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                            }
                            else
                            {
                                checkIfCostExist.CostRate = (int?)item.CalculationRate;
                                checkIfCostExist.CostValue = item.CalculationValue;
                                checkIfCostExist.Description = item.Description;
                                checkIfCostExist.Active = item.ChkActive;
                                checkIfCostExist.ModifiedBy = session.UserId;
                                checkIfCostExist.ModifiedOn = DateTime.Now;
                                checkIfCostExist.ProjectId =ProjectId;
                                hrRepositoryManager.HrEmployeeCostRepository.Update(checkIfCostExist);
                                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                            }

                        }
                        // في حالة تطبيق على المقييمن و السعوديين
                        else if (item.TypeNationality == 3 && (entity.NationalityId != NationalityID || entity.NationalityId == NationalityID))
                        {
                            var checkIfCostExist = await hrRepositoryManager.HrEmployeeCostRepository.GetOne(x => x.IsDeleted == false && x.EmpId == Convert.ToInt32(investEmployees.Id) && x.CostTypeId == item.TypeId);


                            if (checkIfCostExist == null)
                            {
                                var neCostItem = new HrEmployeeCost
                                {
                                    CostRate = (int?)item.CalculationRate,
                                    CostTypeId = item.TypeId,
                                    TypeCalculation = item.TypeCalculation,
                                    CostValue = item.CalculationValue,
                                    Description = item.Deduction,
                                    Active = item.ChkActive,
                                    CreatedBy = session.UserId,
                                    CreatedOn = DateTime.Now,
                                    ProjectId = ProjectId
                                };

                                await hrRepositoryManager.HrEmployeeCostRepository.Add(neCostItem);
                                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                            }
                            else
                            {
                                checkIfCostExist.CostRate = (int?)item.CalculationRate;
                                checkIfCostExist.CostValue = item.CalculationValue;
                                checkIfCostExist.Description = item.Description;
                                checkIfCostExist.Active = item.ChkActive;
                                checkIfCostExist.ModifiedBy = session.UserId;
                                checkIfCostExist.ModifiedOn = DateTime.Now;
                                checkIfCostExist.ProjectId = ProjectId;
                                hrRepositoryManager.HrEmployeeCostRepository.Update(checkIfCostExist);
                                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                            }
                        }
                    }
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync("", localization.GetResource1("AddSuccess"), 200);

            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP in Add at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
        public async Task<IResult<HrEmployeeCostDataDto>> GetEmpDataByEmpId(string empCode, CancellationToken cancellationToken = default)
        {
            try
            {
                var BranchesList = session.Branches.Split(',');
                decimal TotalAllowance = 0;
                decimal TotalDeduction = 0;
                List<HrCostTypeDto> resultList = new List<HrCostTypeDto>();
                List<HrAllowanceVM> allAllowanceList = new List<HrAllowanceVM>();
                List<HrDeductionVM> allDeductionList = new List<HrDeductionVM>();
                var investEmployees = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == empCode && e.IsDeleted == false && e.Isdel == false);
                if (investEmployees == null)
                {
                    return await Result<HrEmployeeCostDataDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                }
                var GetHREmpAllData = await hrRepositoryManager.HrEmployeeRepository.GetOne(x => x.Id == investEmployees.Id && x.IsDeleted == false && x.Isdel == false && BranchesList.Contains(x.BranchId.ToString()));
                if (GetHREmpAllData == null)
                {
                    return await Result<HrEmployeeCostDataDto>.FailAsync(localization.GetHrResource("UserNotHasPermissionToEmp"));
                }
                var getTotalAllowance = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(e => e.EmpId == investEmployees.Id && e.IsDeleted == false && e.TypeId == 1 && e.FixedOrTemporary == 1);
                if (getTotalAllowance != null)
                {
                    foreach (var item in getTotalAllowance)
                    {
                        TotalAllowance += (item.Amount != null ? item.Amount.Value : 0);
                        var allAllowance = new HrAllowanceVM
                        {
                            Id = item.Id,
                            AllowanceAmount = item.Amount,
                            AllowanceRate = item.Rate,
                            AddId = item.AdId
                        };
                        allAllowanceList.Add(allAllowance);
                    }
                }
                var getTotalDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetAll(e => e.EmpId == investEmployees.Id && e.IsDeleted == false && e.TypeId == 2 && e.FixedOrTemporary == 1);
                if (getTotalDeduction != null)
                {
                    foreach (var item in getTotalDeduction)
                    {
                        TotalDeduction += (item.Amount != null ? item.Amount.Value : 0);
                        var alldeduction = new HrDeductionVM
                        {
                            Id = item.Id,
                            DeductionAmount = item.Amount,
                            DeductionRate = item.Rate,
                            AddId = item.AdId
                        };
                        allDeductionList.Add(alldeduction);
                    }
                }

                var empDataResult = new HrEmployeeCostDataDto
                {
                    Salary = GetHREmpAllData.Salary,
                    EmpCode = GetHREmpAllData.EmpId,
                    NetSalary = GetHREmpAllData.Salary + TotalAllowance - TotalDeduction,
                    AllowancesAmount = TotalAllowance,
                    DeductionsAmount = TotalDeduction,
                    EmpName = GetHREmpAllData.EmpName,
                    deduction = allDeductionList,
                    allowance = allAllowanceList,
                    NationalityId = GetHREmpAllData.NationalityId,
                };
                var getCostType = await hrRepositoryManager.HrCostTypeRepository.GetAll(X => X.IsDeleted == false);
                foreach (var item in getCostType)
                {
                    decimal? total = 0;
                    decimal? total1 = 0;
                    if (item.TypeCalculation == 2 || item.TypeCalculation == 4)
                    {
                        switch (item.RateType)
                        {
                            case 1:
                                total = empDataResult.Salary * item.CalculationRate / 100;
                                break;
                            case 2:
                                total1 = empDataResult.Salary + empDataResult.AllowancesAmount;
                                total = total1 * item.CalculationRate / 100;

                                break;
                            case 3:
                                total1 = empDataResult.Salary + empDataResult.AllowancesAmount;
                                total = total1 * item.CalculationRate / 100;
                                break;
                            case 4:
                                if (item.SalaryBasic == true)
                                {
                                    total1 = empDataResult.Salary + empDataResult.AllowancesAmount - empDataResult.DeductionsAmount;
                                    total = total1 * item.CalculationRate / 100;
                                }
                                if (item.SalaryBasic == false)
                                {
                                    total1 = empDataResult.AllowancesAmount - empDataResult.DeductionsAmount;
                                    total = total1 * item.CalculationRate / 100;
                                }
                                break;
                            default:
                                break;
                        }

                        var HrCostTypeDto = new HrCostTypeDto
                        {
                            CalculationValue = total,
                            TypeName = item.TypeName,
                            Id = item.Id,
                            ChkActive = true,
                            TypeNationality = item.TypeNationality,
                            TypeCalculation = item.TypeCalculation,
                            CalculationRate = item.CalculationRate,
                        };
                        resultList.Add(HrCostTypeDto);

                    }
                    else
                    {
                        var HrCostTypeDto = new HrCostTypeDto
                        {
                            CalculationValue = item.CalculationValue,
                            TypeName = item.TypeName,
                            Id = item.Id,
                            ChkActive = true,
                            TypeNationality = item.TypeNationality,
                            TypeCalculation = item.TypeCalculation,
                            CalculationRate = item.CalculationRate,
                        };
                        resultList.Add(HrCostTypeDto);
                    }
                }
                empDataResult.costType = resultList;

                return await Result<HrEmployeeCostDataDto>.SuccessAsync(empDataResult, "");

            }
            catch (Exception exp)
            {
                return await Result<HrEmployeeCostDataDto>.FailAsync($"EXP in GetEmpDataByEmpId at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


        #endregion

        #region شاشة تقرير بتكاليف الموظفين


        public async Task<IResult<IEnumerable<dynamic>>> GetRPEmployeeCost(string EmpCode, string EmpName, string nationalityId, string StartDate, string EndDate, string DeptId, string Location, int LanguageType)
        {
            var items = await hrRepositoryManager.HrEmployeeCostRepository.GetRPEmployeeCost(EmpCode, EmpName, nationalityId, StartDate, EndDate, DeptId, Location, LanguageType);
            if (items == null) return await Result<IEnumerable<dynamic>>.FailAsync("No data Found");
            return await Result<IEnumerable<dynamic>>.SuccessAsync(items, "report  records retrieved");
        }

        #endregion

        #region    شاشة تكاليف الموظفين خلال فترة العقد

        public async Task<IResult<IEnumerable<dynamic>>> GetRPEmployeeContract(string EmpCode, string EmpName, string nationalityId, string DeptId, string Location, int LanguageType, string BranchID, string? BranchsID, string StatusID)
        {
            var items = await hrRepositoryManager.HrEmployeeCostRepository.GetRPEmployeeContract(EmpCode, EmpName, nationalityId, DeptId, Location, LanguageType, BranchID, BranchsID, StatusID);
            if (items == null) return await Result<IEnumerable<dynamic>>.FailAsync("No data Found");
            return await Result<IEnumerable<dynamic>>.SuccessAsync(items, "report  records retrieved");
        }


        #endregion


        #region شاشة التعديل


        public async Task<IResult<string>> Update(HrEmployeeCostDataDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<string>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                long? ProjectId = 0;
                if (entity == null) return await Result<string>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

                var checkEmp = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmp == null) return await Result<string>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                if (!(string.IsNullOrEmpty(entity.ProjectCod)))
                {
                    var GetProjectId = await pMRepositoryManager.PMProjectsRepository.GetPMProjectIdByCode(entity.ProjectCod, session.FacilityId);

                    if (GetProjectId != null)
                        ProjectId = GetProjectId.ProjectId;
                }

                if (entity.costType != null)
                {
                    foreach (var EmpCost in entity.costType)
                    {
                        var item = await hrRepositoryManager.HrEmployeeCostRepository.GetOne(x => x.IsDeleted == false && x.EmpId == (int?)checkEmp.Id && x.Id == EmpCost.Id && x.CostTypeId == EmpCost.TypeId);

                        if (item == null) return await Result<string>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
                        item.ModifiedOn = DateTime.Now;
                        item.ModifiedBy = (int)session.UserId;
                        item.Active = EmpCost.ChkActive;
                        item.CostRate = (int?)EmpCost.CalculationRate;
                        item.CostValue = EmpCost.CalculationValue;
                        item.Description = EmpCost.Description;
                        item.ProjectId = ProjectId;

                        hrRepositoryManager.HrEmployeeCostRepository.Update(item);

                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                    }


                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync("", localization.GetResource1("UpdateSuccess"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<string>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrEmployeeCostDataDto>> GetDataById(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = new HrEmployeeCostDataDto();
                result.costType = new List<HrCostTypeDto>();
                var BranchesList = session.Branches.Split(',');
                decimal TotalAllowance = 0;
                decimal TotalDeduction = 0;

                var getCost = await hrRepositoryManager.HrEmployeeCostRepository.GetOneVw(X => X.IsDeleted == false && X.Id == Id);
                if (getCost == null) return await Result<HrEmployeeCostDataDto>.FailAsync($"لا توجد بيانات لهذا الرقم {Id}");
                var empCode = getCost.EmpCode;

                var investEmployees = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == empCode && e.IsDeleted == false && e.Isdel == false);
                if (investEmployees == null)
                {
                    return await Result<HrEmployeeCostDataDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                }
                var GetHREmpAllData = await hrRepositoryManager.HrEmployeeRepository.GetOne(x => x.Id == investEmployees.Id && x.IsDeleted == false && x.Isdel == false && BranchesList.Contains(x.BranchId.ToString()));
                if (GetHREmpAllData == null)
                {
                    return await Result<HrEmployeeCostDataDto>.FailAsync(localization.GetHrResource("UserNotHasPermissionToEmp"));
                }
                TotalAllowance = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalAllowances(investEmployees.Id);
                TotalDeduction = await hrRepositoryManager.HrAllowanceDeductionRepository.GetTotalDeduction(investEmployees.Id);

                result.Id = getCost.Id;
                result.EmpCode = investEmployees.EmpId;
                result.ProjectCod = getCost.ProjectCode.ToString();
                result.Salary = GetHREmpAllData.Salary;
                result.NetSalary = GetHREmpAllData.Salary + TotalAllowance - TotalDeduction;
                result.AllowancesAmount = TotalAllowance;
                result.DeductionsAmount = TotalDeduction;
                result.EmpName = GetHREmpAllData.EmpName;

                var getEmployeeCost = await hrRepositoryManager.HrEmployeeCostRepository.GetAllVw(X => X.IsDeleted == false && X.EmpId == investEmployees.Id && X.CostTypeId == getCost.CostTypeId);
                foreach (var item in getEmployeeCost)
                {

                    var HrCostTypeDto = new HrCostTypeDto
                    {
                        CalculationValue = item.CostValue,
                        TypeName = item.TypeName,
                        Id = item.Id,
                        ChkActive = item.Active,
                        Description = item.Description,
                        TypeNationality = item.TypeNationality,
                        TypeCalculation = item.TypeCalculation,
                        CalculationRate = item.CostRate,
                        RateType = item.RateType,
                        TypeId = item.CostTypeId
                    };
                    result.costType.Add(HrCostTypeDto);

                }


                return await Result<HrEmployeeCostDataDto>.SuccessAsync(result, "");

            }
            catch (Exception exp)
            {
                return await Result<HrEmployeeCostDataDto>.FailAsync($"EXP in GetEmpDataByEmpId at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public Task<IResult<HrEmployeeCostEditDto>> Update(HrEmployeeCostEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }



        #endregion


        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {

                var item = await hrRepositoryManager.HrEmployeeCostRepository.GetById(Id);
                if (item == null) return Result<HrEmployeeCostDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = session.UserId;
                hrRepositoryManager.HrEmployeeCostRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrEmployeeCostDto>.SuccessAsync(_mapper.Map<HrEmployeeCostDto>(item), localization.GetMessagesResource("Deletesuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrEmployeeCostDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {

                var item = await hrRepositoryManager.HrEmployeeCostRepository.GetById(Id);
                if (item == null) return Result<HrEmployeeCostDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = session.UserId;
                hrRepositoryManager.HrEmployeeCostRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrEmployeeCostDto>.SuccessAsync(_mapper.Map<HrEmployeeCostDto>(item), localization.GetMessagesResource("Deletesuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrEmployeeCostDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

    public async Task<IResult<List<HrEmployeeCostFilterDto>>> Search(HrEmployeeCostFilterDto filter, CancellationToken cancellationToken = default)
    {
      List<HrEmployeeCostFilterDto> resultList = new List<HrEmployeeCostFilterDto>();
      try
      {
        filter.Location ??= 0;
        filter.NationalityId ??= 0;
        filter.DeptId ??= 0;
        var items = await hrRepositoryManager.HrEmployeeCostRepository.GetAllVw(e => e.IsDeleted == false
        && (filter.DeptId == 0 || filter.DeptId == e.DeptId)
        && (filter.Location == 0 || filter.Location == e.Location)
        && (filter.NationalityId == 0 || filter.NationalityId == e.NationalityId)
        && (string.IsNullOrEmpty(filter.EmpName) || (e.EmpName != null && e.EmpName.ToLower().Contains(filter.EmpName.ToLower())) || (e.EmpName2 != null && e.EmpName2.ToLower().Contains(filter.EmpName.ToLower())))
       && (string.IsNullOrEmpty(filter.EmpCode) || e.EmpCode == filter.EmpCode)
        );
        if (items != null)
        {
          if (items.Count() > 0)
          {
            var res = items.AsQueryable();

            foreach (var item in res)
            {
              var newRecord = new HrEmployeeCostFilterDto
              {

                Id = item.Id,
                EmpCode = item.EmpCode,
                EmpName = session.Language == 1 ? item.EmpName : item.EmpName2,
                //TypeCalculationName = item.TypeCalculationName,
                TypeName = session.Language == 1 ? item.TypeName : item.TypeNameEn,
                CostValue = item.CostValue,

              };
              resultList.Add(newRecord);

            }
            return await Result<List<HrEmployeeCostFilterDto>>.SuccessAsync(resultList);
          }

          return await Result<List<HrEmployeeCostFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));

        }

        return await Result<List<HrEmployeeCostFilterDto>>.FailAsync("item errors");
      }
      catch (Exception ex)
      {
        return await Result<List<HrEmployeeCostFilterDto>>.FailAsync(ex.Message);
      }
    }
  }
}
