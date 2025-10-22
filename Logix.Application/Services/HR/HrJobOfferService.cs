using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrJobOfferService : GenericQueryService<HrJobOffer, HrJobOfferDto, HrJobOfferVw>, IHrJobOfferService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrJobOfferService(IQueryRepository<HrJobOffer> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public async Task<IResult<HrJobOfferDto>> Add(HrJobOfferDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrJobOfferDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);
                // فحص هل له عرض وظيفي مسبقا
                var checkIfAlreadyExist = await hrRepositoryManager.HrJobOfferRepository.GetAll(x => x.IsDeleted == false && x.RecruApplicantId == entity.ApplicantId);
                if (checkIfAlreadyExist.Count() > 0)
                    return await Result<HrJobOfferDto>.FailAsync($"تم ارسال له عرض وظيفي مسبقا ");


                var newItem = new HrJobOffer
                {
                    RecruApplicantId = entity.ApplicantId,
                    JobCatId = entity.JobCatId,
                    TrialType = entity.TrialType,
                    TrialCount = entity.TrialCount ?? 0,
                    ContractTypeId = entity.ContractTypeId,
                    BasicSalary = entity.BasicSalary,
                    TotalSalary = (entity.BasicSalary + entity.HousingAllowance + entity.OtherAllowance + entity.TransportAllowance),
                    MedicalInsurance = 0,
                    FlightTickets = 0,
                    HousingAllowance = entity.HousingAllowance,
                    TransportAllowance = entity.TransportAllowance,
                    OtherAllowance = entity.OtherAllowance,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false,
                    ShiftId=0,
                    DurationExperiment=0,
                    TypeDurationExperimentId=0,
                    IsFamilyMedicalInsurance= false,
                    IsFamilyFlightTickets=false,
                };


                var newEntity = await hrRepositoryManager.HrJobOfferRepository.AddAndReturn(newItem);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                //  ----------- تغير المرحلةالى العرض الوظيفي ----------------------------
                var getRecrumentApplication = await hrRepositoryManager.HrRecruitmentApplicationRepository.GetOne(x => x.IsDeleted == false && x.Id == entity.ApplicantId);
                if (getRecrumentApplication != null)
                {
                    getRecrumentApplication.StatusId = 4;
                    getRecrumentApplication.ModifiedBy = session.UserId;
                    getRecrumentApplication.ModifiedOn = DateTime.Now;
                    hrRepositoryManager.HrRecruitmentApplicationRepository.Update(getRecrumentApplication);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                //  ادخال مزايا العرض
                if (!string.IsNullOrEmpty(entity.AdvantagesList))
                {
                    var AdvantagesList = entity.AdvantagesList
                        .Split(',')
                        .Where(x => !string.IsNullOrWhiteSpace(x)) // Ensure there are no empty or null values
                        .Select(x => long.Parse(x)) // Convert each item to long
                        .ToList();
                    var getAllAdvantageNames = await mainRepositoryManager.SysLookupDataRepository.GetAll(x => x.CatagoriesId == 540);
                    foreach (var item in AdvantagesList)
                    {
                        var AdvantagesName = getAllAdvantageNames.Where(x => x.Code == item).Select(x => x.Name).FirstOrDefault() ?? "";
                        var SingleAdvantages = new HrJobOfferAdvantage
                        {
                            AdvantagesId = item,
                            AdvantagesName = AdvantagesName,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            IsDeleted = false,
                            JobOfferId = newEntity.Id,
                            RecruApplicantId = entity.ApplicantId
                        };
                        await hrRepositoryManager.HrJobOfferAdvantageRepository.Add(SingleAdvantages);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }
                newEntity.FileUrl = "~\\Files\\Job_Offer\\" + newEntity.Id + ".pdf";
                newEntity.ModifiedBy = session.UserId;
                newEntity.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrJobOfferRepository.Update(newEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrJobOfferDto>(newEntity);

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrJobOfferDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception)
            {
                return await Result<HrJobOfferDto>.FailAsync(localization.GetResource1("ErrorOccurredDuring"));
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrJobOfferRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrJobOfferDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrJobOfferRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrJobOfferDto>.SuccessAsync(_mapper.Map<HrJobOfferDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrJobOfferDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrJobOfferRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrJobOfferDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrJobOfferRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrJobOfferDto>.SuccessAsync(_mapper.Map<HrJobOfferDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrJobOfferDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrJobOfferEditDto>> Update(HrJobOfferEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrJobOfferEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            try
            {
                var item = await hrRepositoryManager.HrJobOfferRepository.GetById(entity.Id);

                if (item == null) return await Result<HrJobOfferEditDto>.FailAsync("the Record Is Not Found");

                _mapper.Map(entity, item);
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrJobOfferRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrJobOfferEditDto>.SuccessAsync(_mapper.Map<HrJobOfferEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrJobOfferEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}