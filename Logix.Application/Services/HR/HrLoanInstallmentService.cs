using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Helpers;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrLoanInstallmentService : GenericQueryService<HrLoanInstallment, HrLoanInstallmentDto, HrLoanInstallmentVw>, IHrLoanInstallmentService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrLoanInstallmentService(IQueryRepository<HrLoanInstallment> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public Task<IResult<HrLoanInstallmentDto>> Add(HrLoanInstallmentDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {



                var item = await hrRepositoryManager.HrLoanInstallmentRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrLoanInstallmentDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.IsDeleted = true;
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrLoanInstallmentRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrLoanInstallmentDto>.SuccessAsync(_mapper.Map<HrLoanInstallmentDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrLoanInstallmentDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {



                var item = await hrRepositoryManager.HrLoanInstallmentRepository.GetOne(x => x.Id == Id);
                if (item == null) return Result<HrLoanInstallmentDto>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                item.IsDeleted = true;
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrLoanInstallmentRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrLoanInstallmentDto>.SuccessAsync(_mapper.Map<HrLoanInstallmentDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrLoanInstallmentDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrLoanInstallmentEditDto>> Update(HrLoanInstallmentEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrLoanInstallmentEditDto>.FailAsync(localization.GetMessagesResource("NoIdInUpdate"));

            try
            {
                var item = await hrRepositoryManager.HrLoanInstallmentRepository.GetById(entity.Id);

                if (item == null) return await Result<HrLoanInstallmentEditDto>.FailAsync("the Installment Is Not Found");

                entity.ModifiedBy = session.UserId;
                entity.ModifiedOn = DateTime.Now;
                _mapper.Map(entity, item);

                hrRepositoryManager.HrLoanInstallmentRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrLoanInstallmentEditDto>.SuccessAsync(_mapper.Map<HrLoanInstallmentEditDto>(item), localization.GetMessagesResource("success"));
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrLoanInstallmentEditDto>.FailAsync($"EXP in Update at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}