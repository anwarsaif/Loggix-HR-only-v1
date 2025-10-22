using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrSettingService : GenericQueryService<HrSetting, HrSettingDto, HrSetting>, IHrSettingService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;


        public HrSettingService(IQueryRepository<HrSetting> queryRepository, IHrRepositoryManager hrRepositoryManager, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this._mainRepositoryManager = mainRepositoryManager;
            this.hrRepositoryManager = hrRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrSettingDto>> Add(HrSettingDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrSettingDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");

            try
            {
                entity.Id = 0;
                var item = _mapper.Map<HrSetting>(entity);
                var newEntity = await hrRepositoryManager.HrSettingRepository.AddAndReturn(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrSettingDto>(newEntity);


                return await Result<HrSettingDto>.SuccessAsync(entityMap, "item added successfully");
            }
            catch (Exception exc)
            {

                return await Result<HrSettingDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }

        public Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<HrSettingDto>> Update(HrSettingDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrSettingDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = await hrRepositoryManager.HrSettingRepository.GetOne(i => i.FacilityId == entity.FacilityId);

                if (item == null) return await Result<HrSettingDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
                entity.Id = item.Id;
                _mapper.Map(entity, item);
                if (entity.hrPayrollTransactionTypeValues != null)
                {
                    foreach (var value in entity.hrPayrollTransactionTypeValues)
                    {
                        var payrollValue = await hrRepositoryManager.HrPayrollTransactionTypeValueRepository.GetOne(x => x.PayrollTransId == value.PayrollTransId && x.FacilityId == session.FacilityId);
                        if (payrollValue == null)
                        {
                            payrollValue = new HrPayrollTransactionTypeValue
                            {
                                FacilityId = (int)session.FacilityId,
                                Value = value.Value,
                                PayrollTransId = value.PayrollTransId
                            };
                            await hrRepositoryManager.HrPayrollTransactionTypeValueRepository.Add(payrollValue);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }
                        else
                        {
                            payrollValue.FacilityId = (int)session.FacilityId;
                            payrollValue.Value = value.Value;
                            payrollValue.PayrollTransId = value.PayrollTransId;
                            //hrRepositoryManager.HrPayrollTransactionTypeValueRepository.Update(payrollValue);
                            //await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                            await hrRepositoryManager.HrPayrollTransactionTypeValueRepository.save(payrollValue);
                        }
                    }
                }
                hrRepositoryManager.HrSettingRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<HrSettingDto>.SuccessAsync(_mapper.Map<HrSettingDto>(item), "Item updated successfully");
            }
            catch (Exception exp)
            {
                return await Result<HrSettingDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }

}