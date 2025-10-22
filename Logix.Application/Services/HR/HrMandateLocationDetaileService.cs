using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrMandateLocationDetaileService : GenericQueryService<HrMandateLocationDetaile, HrMandateLocationDetaileDto, HrMandateLocationDetailesVw>, IHrMandateLocationDetaileService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper mapper;
        private readonly ICurrentData session;


        public HrMandateLocationDetaileService(IQueryRepository<HrMandateLocationDetaile> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this.mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrMandateLocationDetaileDto>> Add(HrMandateLocationDetaileDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrMandateLocationDetaileDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {

                // للتأكد هل المستوى موجود مسبقا
                var checkMasterExist = await hrRepositoryManager.HrMandateLocationMasterRepository.GetOne(x => x.Id == entity.MlId && x.IsDeleted == false);
                if (checkMasterExist == null)
                {
                    return await Result<HrMandateLocationDetaileDto>.FailAsync($" الجهة رقم {entity.MlId} غير موجودة ");
                }

                // للتأكد هل المستوى موجود مسبقا
                var getAllDetails = await hrRepositoryManager.HrMandateLocationDetaileRepository.GetAll(x => x.MlId == entity.MlId && x.IsDeleted == false && x.JobLevelId == entity.JobLevelId);
                if (getAllDetails.Count() >= 1)
                {
                    return await Result<HrMandateLocationDetaileDto>.FailAsync($" {localization.GetHrResource("LevelExists")}");
                }


                var newDetail = new HrMandateLocationDetaile();
                newDetail.MlId = entity.MlId;
                newDetail.CreatedBy = session.UserId;
                newDetail.CreatedOn = DateTime.Now;
                newDetail.IsDeleted = false;
                newDetail.JobLevelId = entity.JobLevelId;
                newDetail.AllowanceValue = entity.AllowanceValue ?? 0;
                newDetail.TransportIsInsured = entity.TransportIsInsured;
                newDetail.TransportAmount = entity.TransportAmount ?? 0;
                newDetail.TransportIsInsured = entity.TransportIsInsured;
                newDetail.HouseingIsSecured = entity.HouseingIsSecured;
                newDetail.RatePerNight = entity.RatePerNight ?? 0;
                newDetail.RatePerNight = entity.RatePerNight ?? 0;
                newDetail.TicketValue = entity.TicketValue ?? 0;
                newDetail.Note = entity.Note ?? "";
                var newEntity = await hrRepositoryManager.HrMandateLocationDetaileRepository.AddAndReturn(newDetail);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);




                var entityMap = mapper.Map<HrMandateLocationDetaileDto>(newEntity);

                return await Result<HrMandateLocationDetaileDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {
                return await Result<HrMandateLocationDetaileDto>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrMandateLocationDetaileRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrMandateLocationDetaileDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;
                hrRepositoryManager.HrMandateLocationDetaileRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                return await Result<HrMandateLocationDetaileDto>.SuccessAsync(mapper.Map<HrMandateLocationDetaileDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrMandateLocationDetaileDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrMandateLocationDetaileRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrMandateLocationDetaileDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;
                hrRepositoryManager.HrMandateLocationDetaileRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                return await Result<HrMandateLocationDetaileDto>.SuccessAsync(mapper.Map<HrMandateLocationDetaileDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrMandateLocationDetaileDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrMandateLocationDetaileEditDto>> Update(HrMandateLocationDetaileEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrMandateLocationDetaileEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            try
            {
                var item = await hrRepositoryManager.HrMandateLocationDetaileRepository.GetById(entity.Id);

                if (item == null) return await Result<HrMandateLocationDetaileEditDto>.FailAsync(localization.GetMessagesResource("NoItemFoundToEdit"));

                // للتأكد هل المستوى موجود مسبقا
                var getAllDetails = await hrRepositoryManager.HrMandateLocationDetaileRepository.GetAll(x => x.MlId == entity.MlId && x.IsDeleted == false && x.JobLevelId == entity.JobLevelId);
                if (getAllDetails.Count() > 1)
                {
                    return await Result<HrMandateLocationDetaileEditDto>.FailAsync($" {localization.GetHrResource("LevelExists")}");
                }

                mapper.Map(entity, item);
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = false;
                item.JobLevelId = entity.JobLevelId;
                item.MlId = entity.MlId;
                hrRepositoryManager.HrMandateLocationDetaileRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrMandateLocationDetaileEditDto>.SuccessAsync(mapper.Map<HrMandateLocationDetaileEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrMandateLocationDetaileEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}