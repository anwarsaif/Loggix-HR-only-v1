using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;

namespace Logix.Application.Services.HR
{
    public class HrLoanPaymentService : GenericQueryService<HrLoanPayment, HrLoanPaymentDto, HrLoanPaymentVw>, IHrLoanPaymentService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly ILocalizationService localization;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;


        public HrLoanPaymentService(IQueryRepository<HrLoanPayment> queryRepository, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, IHrRepositoryManager hrRepositoryManager, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.hrRepositoryManager = hrRepositoryManager;
            this.localization = localization;
        }

        public Task<IResult<HrLoanPaymentDto>> Add(HrLoanPaymentDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IResult<HrLoanPaymentEditDto>> Update(HrLoanPaymentEditDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public async Task<IResult<string>> DeleteHrLoanPayment(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var LoanPaymentitem = await hrRepositoryManager.HrLoanPaymentRepository.GetOne(x => x.Id == Id);
                if (LoanPaymentitem == null) return Result<string>.Fail($"{localization.GetMessagesResource("NoIdInDelete")}");

                LoanPaymentitem.ModifiedBy = session.UserId;
                LoanPaymentitem.ModifiedOn = DateTime.Now;
                LoanPaymentitem.IsDeleted = true;
                hrRepositoryManager.HrLoanPaymentRepository.Update(LoanPaymentitem);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                /////////////////////////////////////////////

                var LoanInstallmentPaymentItem = await hrRepositoryManager.HrLoanInstallmentPaymentRepository.GetAll(x => x.LoanPaymentId == Id);
                if (LoanInstallmentPaymentItem.Count() > 0)
                {
                    foreach (var item in LoanInstallmentPaymentItem)
                    {
                        item.ModifiedBy = session.UserId;
                        item.ModifiedOn = DateTime.Now;
                        item.IsDeleted = true;
                        hrRepositoryManager.HrLoanInstallmentPaymentRepository.Update(item);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }

                }

                /////////////////////////////////////////////

                var getFromLoanInstallmentPayment = await hrRepositoryManager.HrLoanInstallmentPaymentRepository.GetAll(x => x.LoanPaymentId == Id);
                var IdsList = getFromLoanInstallmentPayment.Select(x => x.LoanInstallmentId);
                var getFromLoanInstallment = await hrRepositoryManager.HrLoanInstallmentRepository.GetAll(x => IdsList.Contains(x.Id));
                if (getFromLoanInstallment.Count() > 0)
                {
                    foreach (var item in getFromLoanInstallment)
                    {
                        item.IsPaid = false;
                        item.ModifiedBy = session.UserId;
                        item.ModifiedOn = DateTime.Now;
                        item.RecepitDate = null;
                        item.RecepitNo = null;
                        hrRepositoryManager.HrLoanInstallmentRepository.Update(item);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    }
                }

                /////////////////////////////////////////////
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<string>.SuccessAsync("", localization.GetResource1("DeleteSuccess"));

            }
            catch (Exception exp)
            {
                return await Result<string>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrLoanPaymentDto>> Add(HrLoanPaymentAddDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                // check if Emp Is Exist
                var checkEmp = await mainRepositoryManager.InvestEmployeeRepository.GetOne(x => x.EmpId == entity.EmpCode && x.IsDeleted == false && x.Isdel == false);
                if (checkEmp == null) return await Result<HrLoanPaymentDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                var newEntity = new HrLoanPayment
                {
                    EmpId = checkEmp.Id,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    PayAmount = entity.PayAmount,
                    VoucherDate = entity.VoucherDate,
                    VoucherNo = entity.VoucherNo,
                    Note = entity.Note,
                };
                var newLoanPaymentEntity = await hrRepositoryManager.HrLoanPaymentRepository.AddAndReturn(newEntity);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                ///////////////start of add Details///////////////

                foreach (var item in entity.Details)
                {
                    var newLoanInstallmentPayment = new HrLoanInstallmentPayment
                    {
                        CreatedBy = session.UserId,
                        CreatedOn = DateTime.Now,
                        AmountPaid = item.Amount,
                        PayrollDId = 0,
                        PayrollId = 0,
                        LoanPaymentId = newLoanPaymentEntity.Id,
                        LoanInstallmentId = item.LoanInstallmentId,
                    };
                    var newLoanInstallmentPaymentEntity = await hrRepositoryManager.HrLoanInstallmentPaymentRepository.AddAndReturn(newLoanInstallmentPayment);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                    var getForUpdate = await hrRepositoryManager.HrLoanInstallmentRepository.GetOne(x => x.Id == item.LoanInstallmentId);
                    getForUpdate.IsPaid = true;
                    getForUpdate.ModifiedOn = DateTime.Now;
                    getForUpdate.ModifiedBy = session.UserId;
                    getForUpdate.Amount = item.Amount;
                    getForUpdate.RecepitNo = entity.VoucherNo;
                    getForUpdate.RecepitDate = entity.VoucherDate;

                    hrRepositoryManager.HrLoanInstallmentRepository.Update(getForUpdate);
                    await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                }

                // here save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, newLoanPaymentEntity.Id, 157);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                return await Result<HrLoanPaymentDto>.SuccessAsync(_mapper.Map<HrLoanPaymentDto>(newLoanPaymentEntity),localization.GetResource1("AddSuccess"));

            }
            catch (Exception exp)
            {

                return await Result<HrLoanPaymentDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }
    }
}