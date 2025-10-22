using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System.Globalization;

namespace Logix.Application.Services.HR
{
    public class HrRecruitmentCandidateService : GenericQueryService<HrRecruitmentCandidate, HrRecruitmentCandidateDto, HrRecruitmentCandidateVw>, IHrRecruitmentCandidateService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager _mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;

        public HrRecruitmentCandidateService(IQueryRepository<HrRecruitmentCandidate> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization, IHrRepositoryManager hrRepositoryManager) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this._mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
        }

   

        public async Task<IResult<HrRecruitmentCandidateDto>> Add(HrRecruitmentCandidateDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                entity.AppDate = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                if (entity.FacilityId <= 0)
                {
                    entity.FacilityId = 1;
                    entity.BranchId = 0;
                }
                var newEntity = await hrRepositoryManager.HrRecruitmentCandidateRepository.AddAndReturn(_mapper.Map<HrRecruitmentCandidate>(entity));

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrRecruitmentCandidateDto>(newEntity);

                return await Result<HrRecruitmentCandidateDto>.SuccessAsync(entityMap, localization.GetResource1("CreateSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<HrRecruitmentCandidateDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }
        }

        public async Task<IResult<HrRecruitmentCandidateDto>> RecruitmentCandidateAdd(HrRecruitmentCandidateDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                entity.CreatedBy = session.UserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsDeleted = false;
                entity.AppDate = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                var newEntity = await hrRepositoryManager.HrRecruitmentCandidateRepository.AddAndReturn(_mapper.Map<HrRecruitmentCandidate>(entity));

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var entityMap = _mapper.Map<HrRecruitmentCandidateDto>(newEntity);

                return await Result<HrRecruitmentCandidateDto>.SuccessAsync(entityMap, localization.GetResource1("CreateSuccess"));
            }
            catch (Exception exc)
            {
                return await Result<HrRecruitmentCandidateDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
            }

        }

        public async Task<IResult<HrRecruitmentCandidateEditDto>> RecruitmentCandidateEdit(HrRecruitmentCandidateEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrRecruitmentCandidateEditDto>.FailAsync($"Error in {GetType()} : the passed entity IS NULL.");
            try
            {
                var item = await hrRepositoryManager.HrRecruitmentCandidateRepository.GetById(entity.Id);

                if (item == null) return await Result<HrRecruitmentCandidateEditDto>.FailAsync($"--- there is no Data with this id: {entity.Id}---");
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                _mapper.Map(entity, item);

                hrRepositoryManager.HrRecruitmentCandidateRepository.Update(item);


                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrRecruitmentCandidateEditDto>.SuccessAsync(_mapper.Map<HrRecruitmentCandidateEditDto>(item), localization.GetResource1("UpdateSuccess"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrRecruitmentCandidateEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrRecruitmentCandidateRepository.GetById(Id);
                if (item == null) return Result<HrRecruitmentCandidateDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrRecruitmentCandidateRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrRecruitmentCandidateDto>.SuccessAsync(_mapper.Map<HrRecruitmentCandidateDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrRecruitmentCandidateDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await hrRepositoryManager.HrRecruitmentCandidateRepository.GetById(Id);
                if (item == null) return Result<HrRecruitmentCandidateDto>.Fail($"--- there is no Data with this id: {Id}---");
                item.IsDeleted = true;
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                hrRepositoryManager.HrRecruitmentCandidateRepository.Update(item);

                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrRecruitmentCandidateDto>.SuccessAsync(_mapper.Map<HrRecruitmentCandidateDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrRecruitmentCandidateDto>.FailAsync($"EXP in Remove at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

		public async Task<IResult<List<HrRecruitmentCandidateFilterDto>>> Search(HrRecruitmentCandidateFilterDto filter, CancellationToken cancellationToken = default)
		{
			try
			{
				List<HrRecruitmentCandidateFilterDto> resultList = new List<HrRecruitmentCandidateFilterDto>();
				var items = await hrRepositoryManager.HrRecruitmentCandidateRepository.GetAllVw(e => e.IsDeleted == false && e.FacilityId == session.FacilityId);
				if (items != null)
				{
					if (items.Count() > 0)
					{
						var res = items.AsQueryable();

						if (!string.IsNullOrEmpty(filter.Name))
						{
							res = res.Where(r => r.Name != null && r.Name.Contains(filter.Name));
						}

						if (filter.VacancyId > 0 && filter.VacancyId != null)
						{
							res = res.Where(r => r.VacancyId != null && r.VacancyId == filter.VacancyId);
						}
						if (filter.NationalityId > 0 && filter.NationalityId != null)
						{
							res = res.Where(r => r.NationalityId != null && r.NationalityId == filter.NationalityId);
						}
						if (filter.Gender > 0 && filter.Gender != null)
						{
							res = res.Where(r => r.Gender != null && r.Gender == filter.Gender);
						}
						if (filter.QualificationId > 0)
						{
							res = res.Where(r => r.QualificationId != null && r.QualificationId == filter.QualificationId);
						}
						if (filter.SpecializationId > 0 && filter.SpecializationId != null)
						{
							res = res.Where(r => r.SpecializationId != null && r.SpecializationId == filter.SpecializationId);
						}
						if (filter.MaritalStatus > 0)
						{
							res = res.Where(r => r.MaritalStatus != null && r.MaritalStatus == filter.MaritalStatus);
						}

						if (filter.YearOfExp > 0 && filter.YearOfExp != null)
						{
							res = res.Where(r => r.YearOfExp != null && r.YearOfExp == filter.YearOfExp);
						}
						if (!string.IsNullOrEmpty(filter.RangeExperience))
						{
							res = res.Where(r => r.RangeExperience != null && r.RangeExperience.Contains(filter.RangeExperience));
						}
						if (!string.IsNullOrEmpty(filter.YearGraduation))
						{
							res = res.Where(r => r.YearGraduation != null && r.YearGraduation.Contains(filter.YearGraduation));
						}

						foreach (var item in res)
						{
							var newRecord = new HrRecruitmentCandidateFilterDto
							{
								Id = item.Id,
								Name = item.Name,
								VacancyName = item.VacancyName,
								QualificationName = item.QualificationName,
								SpecializationName = item.SpecializationName,
								University = item.University,
								YearGraduation = item.YearGraduation,
								YearOfExp = item.YearOfExp,
								RangeExperience = item.RangeExperience,
								BirthDate = item.BirthDate,
								Mobile = item.Mobile,
								Email = item.Email,
								NationalityName = item.NationalityName,
								CreatedOn = item.CreatedOn.HasValue ? item.CreatedOn.Value.ToString("yyyy-MM-dd hh:mm:ss tt", CultureInfo.InvariantCulture) : ""

							};
							resultList.Add(newRecord);
						}
						if (resultList.Any())
							return await Result<List<HrRecruitmentCandidateFilterDto>>.SuccessAsync(resultList, "");
						return await Result<List<HrRecruitmentCandidateFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));
					}
					return await Result<List<HrRecruitmentCandidateFilterDto>>.SuccessAsync(resultList, localization.GetResource1("NosearchResult"));
				}
				return await Result<List<HrRecruitmentCandidateFilterDto>>.FailAsync("errrorrr");
			}
			catch (Exception ex)
			{
				return await Result<List<HrRecruitmentCandidateFilterDto>>.FailAsync(ex.Message);
			}
		}

		public Task<IResult<HrRecruitmentCandidateEditDto>> Update(HrRecruitmentCandidateEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
    }