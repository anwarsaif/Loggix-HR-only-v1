using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrMandateLocationMasterService : GenericQueryService<HrMandateLocationMaster, HrMandateLocationMasterDto, HrMandateLocationMasterVw>, IHrMandateLocationMasterService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper mapper;
        private readonly ICurrentData session;


        public HrMandateLocationMasterService(IQueryRepository<HrMandateLocationMaster> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this.mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrMandateLocationMasterDto>> Add(HrMandateLocationMasterDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrMandateLocationMasterDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);


                var newItem = mapper.Map<HrMandateLocationMaster>(entity);
                newItem.CreatedBy = session.UserId;
                newItem.CreatedOn = DateTime.Now;
                newItem.IsDeleted = false;
                newItem.FacilityId = session.FacilityId;
                var newEntity = await hrRepositoryManager.HrMandateLocationMasterRepository.AddAndReturn(newItem);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                foreach (var item in entity.Detaile)
                {
                    var newDetail = new HrMandateLocationDetaile();
                    newDetail.MlId = newItem.Id;
                    newDetail.CreatedBy = session.UserId;
                    newDetail.CreatedOn = DateTime.Now;
                    newDetail.IsDeleted = false;
                    newDetail.JobLevelId = item.JobLevelId;
                    newDetail.AllowanceValue = item.AllowanceValue ?? 0;
                    newDetail.TransportIsInsured = item.TransportIsInsured;
                    newDetail.TransportAmount = item.TransportAmount ?? 0;
                    newDetail.TransportIsInsured = item.TransportIsInsured;
                    newDetail.HouseingIsSecured = item.HouseingIsSecured;
                    newDetail.RatePerNight = item.RatePerNight ?? 0;
                    newDetail.RatePerNight = item.RatePerNight ?? 0;
                    newDetail.TicketValue = item.TicketValue ?? 0;
                    newDetail.Note = item.Note ?? "";
                    await hrRepositoryManager.HrMandateLocationDetaileRepository.Add(newDetail);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                var entityMap = mapper.Map<HrMandateLocationMasterDto>(newEntity);

                return await Result<HrMandateLocationMasterDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {
                return await Result<HrMandateLocationMasterDto>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrMandateLocationMasterRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrMandateLocationMasterDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;
                hrRepositoryManager.HrMandateLocationMasterRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var getAllDetails = await hrRepositoryManager.HrMandateLocationDetaileRepository.GetAll(x => x.MlId == Id);
                if (getAllDetails != null)
                {
                    foreach (var singleRecord in getAllDetails)
                    {
                        singleRecord.ModifiedBy = session.UserId;
                        singleRecord.ModifiedOn = DateTime.Now;
                        singleRecord.IsDeleted = true;
                        hrRepositoryManager.HrMandateLocationDetaileRepository.Update(singleRecord);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrMandateLocationMasterDto>.SuccessAsync(mapper.Map<HrMandateLocationMasterDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrMandateLocationMasterDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrMandateLocationMasterRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrMandateLocationMasterDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;
                hrRepositoryManager.HrMandateLocationMasterRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var getAllDetails = await hrRepositoryManager.HrMandateLocationDetaileRepository.GetAll(x => x.MlId == Id);
                if (getAllDetails != null)
                {
                    foreach (var singleRecord in getAllDetails)
                    {
                        singleRecord.ModifiedBy = session.UserId;
                        singleRecord.ModifiedOn = DateTime.Now;
                        singleRecord.IsDeleted = true;
                        hrRepositoryManager.HrMandateLocationDetaileRepository.Update(singleRecord);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrMandateLocationMasterDto>.SuccessAsync(mapper.Map<HrMandateLocationMasterDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrMandateLocationMasterDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrMandateLocationMasterEditDto>> Update(HrMandateLocationMasterEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrMandateLocationMasterEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            try
            {
                var item = await hrRepositoryManager.HrMandateLocationMasterRepository.GetById(entity.Id);

                if (item == null) return await Result<HrMandateLocationMasterEditDto>.FailAsync(localization.GetMessagesResource("NoItemFoundToEdit"));
                var getAllDetails = await hrRepositoryManager.HrMandateLocationDetaileRepository.GetAll(x => x.MlId == entity.Id && x.IsDeleted == false);
                if (getAllDetails.Count() < 1)
                {
                    return await Result<HrMandateLocationMasterEditDto>.FailAsync($" {localization.GetHrResource("AddLevel")}");

                }

                mapper.Map(entity, item);
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = false;
                item.FacilityId = session.FacilityId;
                hrRepositoryManager.HrMandateLocationMasterRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrMandateLocationMasterEditDto>.SuccessAsync(mapper.Map<HrMandateLocationMasterEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrMandateLocationMasterEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}