using AutoMapper;
using iText.Commons.Bouncycastle.Asn1.X509;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.PM.PmProjects;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System.Globalization;

namespace Logix.Application.Services.HR
{
    public class HrRecruitmentVacancyService : GenericQueryService<HrRecruitmentVacancy, HrRecruitmentVacancyDto, HrRecruitmentVacancyVw>, IHrRecruitmentVacancyService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;

        public HrRecruitmentVacancyService(IQueryRepository<HrRecruitmentVacancy> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrRecruitmentVacancyDto>> Add(HrRecruitmentVacancyDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                var newEntity = await hrRepositoryManager.HrRecruitmentVacancyRepository.AddAndReturn(_mapper.Map<HrRecruitmentVacancy>(entity));

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrRecruitmentVacancyDto>(newEntity);

                return await Result<HrRecruitmentVacancyDto>.SuccessAsync(entityMap, localization.GetResource1("CreateSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<HrRecruitmentVacancyDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<IEnumerable<HrRecruitmentVacancyVwDto>>> GetAllHRRecruitmentVacancy(CancellationToken cancellationToken = default)
        {
            List<HrRecruitmentVacancyVwDto> result = new List<HrRecruitmentVacancyVwDto>();
            try
            {
                var getAllVacancyData = await hrRepositoryManager.HrRecruitmentVacancyRepository.GetAllVW(x => x.IsDeleted == false && x.StatusId == 2);
                if (getAllVacancyData == null) return await Result<IEnumerable<HrRecruitmentVacancyVwDto>>.SuccessAsync($"There is No Jobs");


                foreach (var item in getAllVacancyData)
                {
                    var newVw = _mapper.Map<HrRecruitmentVacancyVwDto>(item);
                    var getCount = await hrRepositoryManager.HrRecruitmentApplicationRepository.GetAll(x => x.VacancyId == item.Id);
                    newVw.CntApplicants = getCount.Count().ToString();
                    result.Add(newVw);
                }
                return await Result<IEnumerable<HrRecruitmentVacancyVwDto>>.SuccessAsync(result);
            }
            catch (Exception exp)
            {

                return await Result<IEnumerable<HrRecruitmentVacancyVwDto>>.FailAsync($"EXP in GetAllHRRecruitmentVacancy at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrRecruitmentVacancyRepository.GetById(Id);
                if (item == null) return Result<HrRecruitmentVacancyDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrRecruitmentVacancyRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrRecruitmentVacancyDto>.SuccessAsync(_mapper.Map<HrRecruitmentVacancyDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrRecruitmentVacancyDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrRecruitmentVacancyRepository.GetById(Id);
                if (item == null) return Result<HrRecruitmentVacancyDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                item.IsDeleted = true;

                hrRepositoryManager.HrRecruitmentVacancyRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrRecruitmentVacancyDto>.SuccessAsync(_mapper.Map<HrRecruitmentVacancyDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrRecruitmentVacancyDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<IEnumerable<HrRecruitmentVacancyVwDto>>> SearchHRRecruitmentVacancy(HrRecruitmentVacancyFilterDto filter, CancellationToken cancellationToken = default)
        {
            List<HrRecruitmentVacancyVwDto> result = new List<HrRecruitmentVacancyVwDto>();
            try
            {
                var getAllVacancyData = await hrRepositoryManager.HrRecruitmentVacancyRepository.GetAllVW(x => x.IsDeleted == false);
                if (getAllVacancyData == null) return await Result<IEnumerable<HrRecruitmentVacancyVwDto>>.SuccessAsync($"There is No Jobs");


                foreach (var item in getAllVacancyData)
                {
                    var newVw = _mapper.Map<HrRecruitmentVacancyVwDto>(item);
                    newVw.CreatedOnString = item.CreatedOn.HasValue ? item.CreatedOn.Value.ToString("yyyy/MM/dd hh:mm:ss tt", CultureInfo.InvariantCulture) : "";
                    var getCount = await hrRepositoryManager.HrRecruitmentApplicationRepository.GetAll(x => x.VacancyId == item.Id);
                    newVw.CntApplicants = getCount.Count().ToString();
                    result.Add(newVw);
                }
                if (result.Count() <= 0)
                {
                    return await Result<List<HrRecruitmentVacancyVwDto>>.SuccessAsync(localization.GetResource1("NosearchResult"));

                }
                if (filter.JobId > 0)
                {
                    result = result.Where(x => x.JobId == filter.JobId).ToList();
                }
                if (filter.StatusId > 0)
                {
                    result = result.Where(x => x.StatusId == filter.StatusId).ToList();
                }
                if (!string.IsNullOrEmpty(filter.VacancyName))
                {
                    result = result.Where(x => x.VacancyName != null && x.VacancyName.Contains(filter.VacancyName)).ToList();
                }


                if (result.Count() > 0)
                {
                    result = result.OrderByDescending(x => x.Id).ToList();
                    return await Result<List<HrRecruitmentVacancyVwDto>>.SuccessAsync(result.ToList(), "");
                }
                return await Result<List<HrRecruitmentVacancyVwDto>>.SuccessAsync(localization.GetResource1("NosearchResult"));
            }
            catch (Exception exp)
            {

                return await Result<IEnumerable<HrRecruitmentVacancyVwDto>>.FailAsync($"EXP in GetAllHRRecruitmentVacancy at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrRecruitmentVacancyEditDto>> Update(HrRecruitmentVacancyEditDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrRecruitmentVacancyRepository.GetById(entity.Id);
                if (item == null) return await Result<HrRecruitmentVacancyEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");

                entity.ModifiedBy = session.UserId;
                entity.ModifiedOn = DateTime.Now;
                _mapper.Map(entity, item);

                hrRepositoryManager.HrRecruitmentVacancyRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrRecruitmentVacancyEditDto>.SuccessAsync(_mapper.Map<HrRecruitmentVacancyEditDto>(item), localization.GetResource1("UpdateSuccess"));

            }
            catch (Exception exc)
            {
                return await Result<HrRecruitmentVacancyEditDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message} && {localization.GetResource1("UpdateError")}");
            }
        }
    }
}