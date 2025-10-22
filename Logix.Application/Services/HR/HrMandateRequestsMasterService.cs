using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrMandateRequestsMasterService : GenericQueryService<HrMandateRequestsMaster, HrMandateRequestsMasterDto, HrMandateRequestsMasterVw>, IHrMandateRequestsMasterService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper mapper;
        private readonly ICurrentData session;


        public HrMandateRequestsMasterService(IQueryRepository<HrMandateRequestsMaster> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this.mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public Task<IResult<HrMandateRequestsMasterDto>> Add(HrMandateRequestsMasterDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrMandateRequestsMasterRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrMandateRequestsMasterDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;
                hrRepositoryManager.HrMandateRequestsMasterRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrMandateRequestsMasterDto>.SuccessAsync(mapper.Map<HrMandateRequestsMasterDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrMandateRequestsMasterDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrMandateRequestsMasterRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrMandateRequestsMasterDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;
                hrRepositoryManager.HrMandateRequestsMasterRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrMandateRequestsMasterDto>.SuccessAsync(mapper.Map<HrMandateRequestsMasterDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrMandateRequestsMasterDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrMandateRequestsMasterEditDto>> Update(HrMandateRequestsMasterEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrMandateRequestsMasterEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            try
            {
                var item = await hrRepositoryManager.HrMandateRequestsMasterRepository.GetById(entity.Id);

                if (item == null) return await Result<HrMandateRequestsMasterEditDto>.FailAsync(localization.GetMessagesResource("NoItemFoundToEdit"));
                var getAllDetails = await hrRepositoryManager.HrMandateRequestsDetaileRepository.GetAll(x => x.MrId == entity.Id && x.IsDeleted == false);
                if (getAllDetails.Count() < 1)
                {
                    return await Result<HrMandateRequestsMasterEditDto>.FailAsync($" {localization.GetHrResource("AddLevel")}");

                }

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = false;
                item.FacilityId = session.FacilityId;
                item.FromDate = DateHelper.StringToDate(entity.FromDateStr);
                item.ToDate = DateHelper.StringToDate(entity.ToDateStr);
                item.Objective = entity.Objective;
                hrRepositoryManager.HrMandateRequestsMasterRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrMandateRequestsMasterEditDto>.SuccessAsync(mapper.Map<HrMandateRequestsMasterEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrMandateRequestsMasterEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }

}