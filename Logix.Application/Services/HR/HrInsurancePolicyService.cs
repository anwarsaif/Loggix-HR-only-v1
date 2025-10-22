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
    public class HrInsurancePolicyService : GenericQueryService<HrInsurancePolicy, HrInsurancePolicyDto, HrInsurancePolicy>, IHrInsurancePolicyService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;
        public HrInsurancePolicyService(IQueryRepository<HrInsurancePolicy> queryRepository, IHrRepositoryManager hrRepositoryManager, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this.hrRepositoryManager = hrRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrInsurancePolicyDto>> Add(HrInsurancePolicyDto entity, CancellationToken cancellationToken = default)
        {

            if (entity == null) return await Result<HrInsurancePolicyDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var InsurancePolicy = new HrInsurancePolicy
                {
                    IsDeleted = false,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    Code = entity.Code,
                    Name = entity.Name,
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate,
                    CompanyId = entity.CompanyId,
                    Note = entity.Note,
                    FacilityId = session.FacilityId,
                };

                var newEntity = await hrRepositoryManager.HrInsurancePolicyRepository.AddAndReturn(InsurancePolicy);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, newEntity.Id, 81);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                var entityMap = _mapper.Map<HrInsurancePolicyDto>(newEntity);
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrInsurancePolicyDto>.SuccessAsync(entityMap, localization.GetResource1("CreateSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<HrInsurancePolicyDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrInsurancePolicyRepository.GetById(Id);
                if (item == null) return Result<HrInsurancePolicyDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;
                hrRepositoryManager.HrInsurancePolicyRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrInsurancePolicyDto>.SuccessAsync(_mapper.Map<HrInsurancePolicyDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrInsurancePolicyDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }


        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrInsurancePolicyRepository.GetById(Id);
                if (item == null) return Result<HrInsurancePolicyDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrInsurancePolicyRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrInsurancePolicyDto>.SuccessAsync(_mapper.Map<HrInsurancePolicyDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrInsurancePolicyDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrInsurancePolicyEditDto>> Update(HrInsurancePolicyEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrInsurancePolicyEditDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = await hrRepositoryManager.HrInsurancePolicyRepository.GetById(entity.Id);

                if (item == null) return await Result<HrInsurancePolicyEditDto>.FailAsync(localization.GetMessagesResource("NoItemFoundToEdit"));

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.Code = entity.Code;
                item.Name = entity.Name;
                item.StartDate = entity.StartDate;
                item.EndDate = entity.EndDate;
                item.CompanyId = entity.CompanyId;
                item.Note = entity.Note;
                item.FacilityId = session.FacilityId;
                hrRepositoryManager.HrInsurancePolicyRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, entity.Id, 81);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrInsurancePolicyEditDto>.SuccessAsync(_mapper.Map<HrInsurancePolicyEditDto>(item), localization.GetResource1("UpdateSuccess"));
            }


            catch (Exception exc)
            {
                return (IResult<HrInsurancePolicyEditDto>)await Result<HrInsurancePolicyDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }
    }
}
