using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Services.HR
{
    public class HrCostTypeService : GenericQueryService<HrCostType, HrCostTypeDto, HrCostTypeVw>, IHrCostTypeService
    {
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        public HrCostTypeService(IQueryRepository<HrCostType> queryRepository, IMainRepositoryManager mainRepositoryManager, ILocalizationService localization, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }


        public async Task<IResult<HrCostTypeDto>> Add(HrCostTypeDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrCostTypeDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
            try
            {
                var item = new HrCostType();

                item.CreatedBy = session.UserId;
                item.CreatedOn = DateTime.Now;
                item.IsDeleted = false;
                item.TypeName = entity.TypeName;
                item.TypeNameEn = entity.TypeNameEn;
                item.TypeId = entity.TypeId;
                item.TypeNationality = entity.TypeNationality;
                item.TypeCalculation = entity.TypeCalculation;
                item.TypeCalculation = entity.TypeCalculation;
                item.Allowance = entity.Allowance;
                item.Deduction = entity.Deduction;
                item.RateType = entity.RateType;
                item.SalaryBasic = entity.SalaryBasic;

                if (entity.CalculationRate == null || entity.CalculationRate <= 0)
                {
                    item.CalculationRate = 0;
                }
                else
                {
                    item.CalculationRate = entity.TypeCalculation;

                }

                if (entity.CalculationValue == null || entity.CalculationValue <= 0)
                {
                    item.CalculationValue = 0;
                }
                else
                {
                    item.CalculationValue = entity.CalculationValue;

                }

                var newEntity = await hrRepositoryManager.HrCostTypeRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                var entityMap = _mapper.Map<HrCostTypeDto>(newEntity);

                return await Result<HrCostTypeDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {

                return await Result<HrCostTypeDto>.FailAsync(localization.GetResource1("AddError"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {

                var item = await hrRepositoryManager.HrCostTypeRepository.GetById(Id);
                if (item == null) return Result<HrCostTypeDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrCostTypeRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrCostTypeDto>.SuccessAsync(_mapper.Map<HrCostTypeDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrCostTypeDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {

                var item = await hrRepositoryManager.HrCostTypeRepository.GetById(Id);
                if (item == null) return Result<HrCostTypeDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrCostTypeRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrCostTypeDto>.SuccessAsync(_mapper.Map<HrCostTypeDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrCostTypeDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrCostTypeEditDto>> Update(HrCostTypeEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrCostTypeEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            try
            {
                var item = await hrRepositoryManager.HrCostTypeRepository.GetById(entity.Id);

                if (item == null) return await Result<HrCostTypeEditDto>.FailAsync(localization.GetResource1("UpdateError"));

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = false;
                item.TypeName = entity.TypeName;
                item.TypeNameEn = entity.TypeNameEn;
                item.TypeId = entity.TypeId;
                item.TypeNationality = entity.TypeNationality;
                item.TypeCalculation = entity.TypeCalculation;
                item.TypeCalculation = entity.TypeCalculation;
                item.Allowance = entity.Allowance;
                item.Deduction = entity.Deduction;
                item.RateType = entity.RateType;
                item.SalaryBasic = entity.SalaryBasic;

                if (entity.CalculationRate == null || entity.CalculationRate <= 0)
                {
                    item.CalculationRate = 0;
                }
                else
                {
                    item.CalculationRate = entity.TypeCalculation;

                }

                if (entity.CalculationValue == null || entity.CalculationValue <= 0)
                {
                    item.CalculationValue = 0;
                }
                else
                {
                    item.CalculationValue = entity.CalculationValue;

                }

                hrRepositoryManager.HrCostTypeRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrCostTypeEditDto>.SuccessAsync(_mapper.Map<HrCostTypeEditDto>(item), localization.GetResource1("UpdateSuccess"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrCostTypeEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}
