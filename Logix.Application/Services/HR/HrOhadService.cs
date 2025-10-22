using AutoMapper;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Vml.Office;
using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System.Linq.Expressions;
using System.Security;

namespace Logix.Application.Services.HR
{
    public class HrOhadService : GenericQueryService<HrOhad, HrOhadDto, HrOhadVw>, IHrOhadService
    {
        private readonly IHrRepositoryManager hrRepositoryManager;
        private readonly IMainRepositoryManager mainRepositoryManager;
        private readonly IMapper _mapper;
        private readonly ICurrentData session;
        private readonly ILocalizationService localization;


        public HrOhadService(IQueryRepository<HrOhad> queryRepository, IHrRepositoryManager hrRepositoryManager, IMainRepositoryManager mainRepositoryManager, IMapper mapper, ICurrentData session, ILocalizationService localization) : base(queryRepository, mapper)
        {
            this.hrRepositoryManager = hrRepositoryManager;
            this.mainRepositoryManager = mainRepositoryManager;
            this._mapper = mapper;
            this.session = session;
            this.localization = localization;
        }

        public async Task<IResult<HrOhadDto>> Add(HrOhadDto entity, CancellationToken cancellationToken = default)
        {
            long? Code = 1;

            if (entity == null) return await Result<HrOhadDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var Employees = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == entity.EmpCode && e.IsDeleted == false);

                if (Employees == null)
                {
                    return await Result<HrOhadDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
                }
                var getcodeCount = await hrRepositoryManager.HrOhadRepository.GetAll(x => x.TransTypeId == 1);
                if (getcodeCount.Count() >= 1)
                    Code = getcodeCount.Count() + 1;

                var newHrOhad = new HrOhad
                {
                    EmpId = Employees.Id,
                    OhdaDate = entity.OhdaDate,
                    TransTypeId = 1,
                    IsDeleted = false,
                    CreatedBy = session.UserId,
                    CreatedOn = DateTime.Now,
                    Code = Code,
                    EmpIdRecipient = entity.EmpIdRecipient,
                    EmpIdTo = entity.EmpIdTo,
                    Note = entity.Note

                };


                var newEntity = await hrRepositoryManager.HrOhadRepository.AddAndReturn(newHrOhad);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                if (entity.OhadDetails.Any())
                {
                    foreach (var detail in entity.OhadDetails)
                    {
                        var newDetailsEntity = new HrOhadDetail
                        {
                            OhdaId = newEntity.OhdaId,
                            OrgnalId = newEntity.OhdaId,
                            ItemId = detail.ItemId,
                            ItemName = detail.ItemName,
                            ItemDetails = detail.ItemDetails,
                            QtyOut = 0,
                            QtyIn = detail.QtyIn,
                            ItemStateId = detail.ItemStateId,
                            ItemState = detail.ItemState,
                            Note = detail.Note,
                            CreatedBy = session.UserId,
                            CreatedOn = DateTime.Now,
                            IsDeleted = false,
                        };
                        await hrRepositoryManager.HrOhadDetailRepository.Add(newDetailsEntity);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }
                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, newEntity.OhdaId, 104);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }
                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
                var entityMap = _mapper.Map<HrOhadDto>(newEntity);

                return await Result<HrOhadDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
            }
            catch (Exception ex)
            {
                return await Result<HrOhadDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {ex.Message}");
            }
        }


		public async Task<IResult<HrOhadDto>> AddDropOhad(HrOhadDto entity, CancellationToken cancellationToken = default)
		{
			long? Code = 1;

			if (entity == null) return await Result<HrOhadDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

			try
			{
				await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

				var Employees = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == entity.EmpCode && e.IsDeleted == false);
				var Employee2 = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == entity.EmpCodeTo && e.IsDeleted == false);

				if (Employees == null || Employee2 == null)
				{
					return await Result<HrOhadDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
				}
				var getcodeCount = await hrRepositoryManager.HrOhadRepository.GetAll(x => x.TransTypeId == 2);
				if (getcodeCount.Count() >= 1)
					Code = getcodeCount.Count() + 1;

				var newHrOhad = new HrOhad
				{
					EmpId = Employees.Id,
					OhdaDate = entity.OhdaDate,
					TransTypeId = 2,
					IsDeleted = false,
					CreatedBy = session.UserId,
					CreatedOn = DateTime.Now,
					Code = Code,
					EmpIdRecipient = Employees.Id,
					EmpIdTo = Employee2.Id,
					Note = entity.Note

				};


				var newEntity = await hrRepositoryManager.HrOhadRepository.AddAndReturn(newHrOhad);
				await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


				if (entity.OhadDetails.Any())
				{
					foreach (var detail in entity.OhadDetails)
					{
						var newDetailsEntity = new HrOhadDetail
						{
							OhdaId = newEntity.OhdaId,
							OrgnalId = newEntity.OhdaId,
							ItemId = detail.ItemId,
							ItemName = detail.ItemName,
							ItemDetails = detail.ItemDetails,
							QtyOut = detail.QtyOut,
							QtyIn = 0,
							ItemStateId = detail.ItemStateId,
							ItemState = detail.ItemState,
							Note = detail.Note,
							CreatedBy = session.UserId,
							CreatedOn = DateTime.Now,
							IsDeleted = false,
						};
						await hrRepositoryManager.HrOhadDetailRepository.Add(newDetailsEntity);
						await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
					}
				}
				//save files
				if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
				{
					var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, newEntity.OhdaId, 104);
					await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
				}
				await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
				var entityMap = _mapper.Map<HrOhadDto>(newEntity);

				return await Result<HrOhadDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
			}
			catch (Exception ex)
			{
				return await Result<HrOhadDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {ex.Message}");
			}
		}

		public async Task<IResult<HrOhadDto>> AddReturnOhad(HrOhadDto entity, CancellationToken cancellationToken = default)
		{
			long? Code = 1;

			if (entity == null) return await Result<HrOhadDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

			try
			{
				await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

				var Employees = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == entity.EmpCode && e.IsDeleted == false);
				var Employee2 = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == entity.EmpCodeTo && e.IsDeleted == false);

				if (Employees == null || Employee2 == null)
				{
					return await Result<HrOhadDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));
				}
				var getcodeCount = await hrRepositoryManager.HrOhadRepository.GetAll(x => x.TransTypeId == 3);
				if (getcodeCount.Count() >= 1)
					Code = getcodeCount.Count() + 1;

				var newHrOhad = new HrOhad
				{
					EmpId = Employees.Id,
					OhdaDate = entity.OhdaDate,
					TransTypeId = 3,
					IsDeleted = false,
					CreatedBy = session.UserId,
					CreatedOn = DateTime.Now,
					Code = Code,
					EmpIdRecipient = Employees.Id,
					EmpIdTo = Employee2.Id,
					Note = entity.Note

				};


				var newEntity = await hrRepositoryManager.HrOhadRepository.AddAndReturn(newHrOhad);
				await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


				if (entity.OhadDetails.Any())
				{
					foreach (var detail in entity.OhadDetails)
					{
						var newDetailsEntity = new HrOhadDetail
						{
							OhdaId = newEntity.OhdaId,
							OrgnalId = newEntity.OhdaId,
							ItemId = detail.ItemId,
							ItemName = detail.ItemName,
							ItemDetails = detail.ItemDetails,
							QtyOut = detail.QtyOut,
							QtyIn = 0,
							ItemStateId = detail.ItemStateId,
							ItemState = detail.ItemState,
							Note = detail.Note,
							CreatedBy = session.UserId,
							CreatedOn = DateTime.Now,
							IsDeleted = false,
						};
						await hrRepositoryManager.HrOhadDetailRepository.Add(newDetailsEntity);
						await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
					}
				}
				//save files
				if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
				{
					var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, newEntity.OhdaId, 104);
					await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
				}
				await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
				var entityMap = _mapper.Map<HrOhadDto>(newEntity);

				return await Result<HrOhadDto>.SuccessAsync(entityMap, localization.GetResource1("AddSuccess"));
			}
			catch (Exception ex)
			{
				return await Result<HrOhadDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {ex.Message}");
			}
		}

    public async Task<IResult<HrOhadDto>> AddTransferOhad(HrOhadDto entity, CancellationToken cancellationToken = default)
    {
      if (entity == null)
        return await Result<HrOhadDto>.FailAsync(localization.GetMessagesResource("AddNullEntity"));

      try
      {
        await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

        // التحقق من الموظف المرسل
        var fromEmployee = await mainRepositoryManager.InvestEmployeeRepository
            .GetOne(e => e.EmpId == entity.EmpCode && e.IsDeleted == false);

        if (fromEmployee == null)
          return await Result<HrOhadDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

        // التحقق من الموظف المستلم
        var toEmployee = await mainRepositoryManager.InvestEmployeeRepository
            .GetOne(e => e.EmpId == entity.EmpCodeTo && e.IsDeleted == false);

        if (toEmployee == null)
          return await Result<HrOhadDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

        // كود المستند
        var getcodeCount = await hrRepositoryManager.HrOhadRepository.GetAll(x => x.TransTypeId == 4);
        long? code = getcodeCount.Count() + 1;

        // ---------------------- 1) إنشاء مستند تحويل (من → إلى) ----------------------
        var transferDocument = new HrOhad
        {
          EmpId = fromEmployee.Id,
          EmpIdTo = toEmployee.Id,
          Note = entity.Note,
          OhdaDate = entity.OhdaDate,
          TransTypeId = 4, // تحويل
          CreatedBy = session.UserId,
          CreatedOn = DateTime.Now,
          IsDeleted = false,
          Code = code
        };

        var newTransfer = await hrRepositoryManager.HrOhadRepository.AddAndReturn(transferDocument);
        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

        // ---------------------- 2) مستند إضافة للمستلم ----------------------
        var transferTo = new HrOhad
        {
          EmpId = toEmployee.Id,
          Note = entity.Note,
          OhdaDate = entity.OhdaDate,
          TransTypeId = 1, // إضافة عهدة
          CreatedBy = session.UserId,
          CreatedOn = DateTime.Now,
          IsDeleted = false
        };
        var newTransferTo = await hrRepositoryManager.HrOhadRepository.AddAndReturn(transferTo);
        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

        // ---------------------- 3) مستند إسقاط للمرسل ----------------------
        var transferFrom = new HrOhad
        {
          EmpId = fromEmployee.Id,
          Note = entity.Note,
          OhdaDate = entity.OhdaDate,
          TransTypeId = 2, // إسقاط عهدة
          CreatedBy = session.UserId,
          CreatedOn = DateTime.Now,
          IsDeleted = false
        };
        var newTransferFrom = await hrRepositoryManager.HrOhadRepository.AddAndReturn(transferFrom);
        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

        // ---------------------- 4) تفاصيل العهدة ----------------------
        if (entity.OhadDetails != null && entity.OhadDetails.Any())
        {
          foreach (var detail in entity.OhadDetails)
          {
            // تفاصيل مستند التحويل
            var newDetail = new HrOhadDetail
            {
              OhdaId = newTransfer.OhdaId,
              ItemId = detail.ItemId,
              ItemName = detail.ItemName,
              ItemDetails = detail.ItemDetails,
              ItemStateId = detail.ItemStateId,
              OrgnalId = detail.OrgnalId,
              QtyOut = detail.QtyOut,
              QtyIn = 0,
              Note = detail.Note,
              CreatedBy = session.UserId,
              CreatedOn = DateTime.Now,
              IsDeleted = false
            };
            await hrRepositoryManager.HrOhadDetailRepository.Add(newDetail);

            // تفاصيل إضافة للمستلم
            var newDetailTo = new HrOhadDetail
            {
              OhdaId = newTransferTo.OhdaId,
              ItemId = detail.ItemId,
              ItemName = detail.ItemName,
              ItemDetails = detail.ItemDetails,
              ItemStateId = detail.ItemStateId,
              OrgnalId = detail.OrgnalId,
              QtyIn = detail.QtyOut,
              Note = detail.Note,
              CreatedBy = session.UserId,
              CreatedOn = DateTime.Now,
              IsDeleted = false
            };
            await hrRepositoryManager.HrOhadDetailRepository.Add(newDetailTo);

            // تفاصيل إسقاط من المرسل
            var newDetailFrom = new HrOhadDetail
            {
              OhdaId = newTransferFrom.OhdaId,
              ItemId = detail.ItemId,
              ItemName = detail.ItemName,
              ItemDetails = detail.ItemDetails,
              ItemStateId = detail.ItemStateId,
              OrgnalId = detail.OrgnalId,
              QtyIn = 0,
              Note = detail.Note,
              CreatedBy = session.UserId,
              CreatedOn = DateTime.Now,
              IsDeleted = false
            };
            await hrRepositoryManager.HrOhadDetailRepository.Add(newDetailFrom);
          }
          await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
        }

        // ---------------------- 5) الملفات (اختياري) ----------------------
        if (entity.fileDtos != null && entity.fileDtos.Any())
        {
          var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, newTransfer.OhdaId, 104);
          await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
        }

        await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

        var mappedEntity = _mapper.Map<HrOhadDto>(newTransfer);
        return await Result<HrOhadDto>.SuccessAsync(mappedEntity, localization.GetResource1("AddSuccess"));
      }
      catch (Exception ex)
      {
        await hrRepositoryManager.UnitOfWork.RollbackTransactionAsync(cancellationToken);
        return await Result<HrOhadDto>.FailAsync($"EXP in {this.GetType()}, Message: {ex.Message}");
      }
    }



    //public async Task<IResult<HrOhadDetailDto>> AddHrOhadDetail(HrOhadDetailDto entity, CancellationToken cancellationToken = default)
    //{
    //    if (entity == null) return await Result<HrOhadDetailDto>.FailAsync($"Error in Add of: {this.GetType()}, the passed entity is NULL !!!.");
    //    if (entity.OhdaId == null || entity.OhdaId <= 0) return await Result<HrOhadDetailDto>.FailAsync($"العهدة غير موجودة");
    //    var OhdaId = await hrRepositoryManager.HrOhadDetailRepository.GetAll(e => e.OhdaId == entity.OhdaId && e.IsDeleted == false);
    //    if (OhdaId.Any())
    //    {
    //        return await Result<HrOhadDetailDto>.FailAsync("رقم العهده موجود مسبقا");
    //    }
    //    try
    //    {
    //        entity.CreatedBy = session.UserId;
    //        entity.CreatedOn = DateTime.Now;

    //        var item = _mapper.Map<HrOhadDetail>(entity);
    //        var newEntity = await hrRepositoryManager.HrOhadDetailRepository.AddAndReturn(item);

    //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

    //        var entityMap = _mapper.Map<HrOhadDetailDto>(newEntity);


    //        return await Result<HrOhadDetailDto>.SuccessAsync(entityMap, "item added successfully");
    //    }
    //    catch (Exception exc)
    //    {

    //        return await Result<HrOhadDetailDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
    //    }
    //}

    //public async Task<IResult<HrOhadDto>> HrTransferOhad(long Id, CancellationToken cancellationToken = default)
    //{
    //    var item = await hrRepositoryManager.HrOhadRepository.GetById(Id);
    //    if (item == null) return Result<HrOhadDto>.Fail($"--- there is no Data with this id: {Id}---");
    //    item.IsDeleted = true;
    //    item.ModifiedOn = DateTime.Now;
    //    item.ModifiedBy = (int)session.UserId;
    //    hrRepositoryManager.HrOhadRepository.Update(item);
    //    try
    //    {
    //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

    //        return await Result<HrOhadDto>.SuccessAsync(_mapper.Map<HrOhadDto>(item), " record removed");
    //    }
    //    catch (Exception exp)
    //    {
    //        return await Result<HrOhadDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
    //    }
    //}

    public async Task<IResult> Remove(long Id, CancellationToken cancellationToken = default)
        {
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = await hrRepositoryManager.HrOhadRepository.GetOne(x => x.OhdaId == Id);
                if (item == null) return Result<HrOhadDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");

                item.IsDeleted = true;
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrOhadRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var getAllOhadDetails = await hrRepositoryManager.HrOhadDetailRepository.GetAll(x => x.IsDeleted == false && x.OhdaId == Id);
                if (getAllOhadDetails != null)
                {
                    foreach (var singleDetails in getAllOhadDetails)
                    {
                        singleDetails.IsDeleted = true;
                        singleDetails.ModifiedBy = session.UserId;
                        singleDetails.ModifiedOn = DateTime.Now;
                        hrRepositoryManager.HrOhadDetailRepository.Update(singleDetails);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }


                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrOhadDto>.SuccessAsync(_mapper.Map<HrOhadDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrOhadDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult> Remove(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var item = await hrRepositoryManager.HrOhadRepository.GetOne(x => x.OhdaId == Id);
                if (item == null) return Result<HrOhadDto>.Fail($"{localization.GetMessagesResource("NoItemFoundToDelete")}");

                item.IsDeleted = true;
                item.ModifiedBy = session.UserId;
                item.ModifiedOn = DateTime.Now;
                hrRepositoryManager.HrOhadRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                var getAllOhadDetails = await hrRepositoryManager.HrOhadDetailRepository.GetAll(x => x.IsDeleted == false && x.OhdaId == Id);
                if (getAllOhadDetails != null)
                {
                    foreach (var singleDetails in getAllOhadDetails)
                    {
                        singleDetails.IsDeleted = true;
                        singleDetails.ModifiedBy = session.UserId;
                        singleDetails.ModifiedOn = DateTime.Now;
                        hrRepositoryManager.HrOhadDetailRepository.Update(singleDetails);
                        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                    }
                }


                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrOhadDto>.SuccessAsync(_mapper.Map<HrOhadDto>(item), localization.GetResource1("DeleteSuccess"));
            }
            catch (Exception exp)
            {
                return await Result<HrOhadDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        public async Task<IResult<HrOhadDto>> removeHrDropingOhad(long Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrOhadRepository.GetById(Id);
            if (item == null) return Result<HrOhadDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrOhadRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrOhadDto>.SuccessAsync(_mapper.Map<HrOhadDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrOhadDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrOhadDetailDto>> removeHrOhadDetail(int Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrOhadDetailRepository.GetById(Id);
            if (item == null) return Result<HrOhadDetailDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrOhadDetailRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrOhadDetailDto>.SuccessAsync(_mapper.Map<HrOhadDetailDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrOhadDetailDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

        public async Task<IResult<HrOhadDto>> removeHrReturnOhad(long Id, CancellationToken cancellationToken = default)
        {
            var item = await hrRepositoryManager.HrOhadRepository.GetById(Id);
            if (item == null) return Result<HrOhadDto>.Fail($"--- there is no Data with this id: {Id}---");
            item.IsDeleted = true;
            item.ModifiedOn = DateTime.Now;
            item.ModifiedBy = (int)session.UserId;
            hrRepositoryManager.HrOhadRepository.Update(item);
            try
            {
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                return await Result<HrOhadDto>.SuccessAsync(_mapper.Map<HrOhadDto>(item), " record removed");
            }
            catch (Exception exp)
            {
                return await Result<HrOhadDto>.FailAsync($"EXP in Remove at ( {this.GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }
        }

    public async Task<IResult<List<HRRPOhadFilterDto>>> RPOhadSerach(HRRPOhadFilterDto filter, CancellationToken cancellationToken = default)

    {
      List<HRRPOhadFilterDto> results = new List<HRRPOhadFilterDto>();
      try
      {
        filter.Branch ??= 0;
        var BranchesList = session.Branches.Split(',');

        var GetData = await hrRepositoryManager.HrOhadRepository.GetAllVw(e => e.IsDeleted == false
        && BranchesList.Contains(e.BranchId.ToString())
        && (string.IsNullOrEmpty(filter.EmpName) || e.EmpName.ToLower().Contains(filter.EmpName.ToLower()))
        && (string.IsNullOrEmpty(filter.EmpCode) || e.EmpCode == filter.EmpCode)

        );
        var filteredResult = GetData.Where(e => filter.Branch == 0 || filter.Branch == e.BranchId);

        foreach (var item in filteredResult)
        {

          var OhadDto = new HRRPOhadFilterDto
          {
            OhadId = item.OhdaId,
            OhdaDate = item.OhdaDate,
            EmpCode = item.EmpCode,
            EmpName = session.Language == 1 ? item.EmpName : item.EmpName2,
            StatusName = session.Language == 1 ? item.StatusName : item.StatusName2,
            ItemNo = item.TransTypeId,
            ItemName = item.TransTypeName,
            Note = item.Note,
          };

          results.Add(OhadDto);
        }
        if (results.Count > 0) return await Result<List<HRRPOhadFilterDto>>.SuccessAsync(results);
        return await Result<List<HRRPOhadFilterDto>>.SuccessAsync(results, localization.GetResource1("NosearchResult"));


      }
      catch (Exception ex)
      {
        return await Result<List<HRRPOhadFilterDto>>.FailAsync(ex.Message);
      }
    }

    public async Task<IResult<HrOhadEditDto>> Update(HrOhadEditDto entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) return await Result<HrOhadEditDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");
            try
            {

                await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

                var Employees = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == entity.EmpCode && e.IsDeleted == false);

                if (Employees == null)
                    return await Result<HrOhadEditDto>.FailAsync(localization.GetResource1("EmployeeNotFound"));

                var item = await hrRepositoryManager.HrOhadRepository.GetById(entity.OhdaId);

                if (item == null) return await Result<HrOhadEditDto>.FailAsync(localization.GetMessagesResource("NoItemFoundToEdit"));
                item.ModifiedOn = DateTime.Now;
                item.ModifiedBy = (int)session.UserId;
                item.Note = entity.Note;
                item.EmpId = Employees.Id;
                item.TransTypeId = 1;
                item.IsDeleted = false;
                item.OhdaDate = entity.OhdaDate;
                hrRepositoryManager.HrOhadRepository.Update(item);
                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


                if (entity.OhadDetails.Any())
                {
                    var getAllDetails = await hrRepositoryManager.HrOhadDetailRepository.GetAll(x => x.IsDeleted == false);

                    foreach (var singleItem in entity.OhadDetails)
                    {
                        if (singleItem.IsDeleted == true && singleItem.OhadDetId > 0)
                        {
                            var CheckIfRecordExist = getAllDetails.Where(x => x.OhadDetId == singleItem.OhadDetId).FirstOrDefault();
                            if (CheckIfRecordExist == null) return await Result<HrOhadEditDto>.FailAsync($"---  تأكد من وجود البيانات سابقا للتفصيل  {singleItem.ItemName} ---");
                            CheckIfRecordExist.IsDeleted = true;
                            CheckIfRecordExist.ModifiedBy = session.UserId;
                            CheckIfRecordExist.ModifiedOn = DateTime.Now;
                            hrRepositoryManager.HrOhadDetailRepository.Update(CheckIfRecordExist);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                        }

                        else if (singleItem.IsDeleted == false && singleItem.OhadDetId == 0)
                        {
                            //  بمعنى انه حقل جديد
                            var newDetailsEntity = new HrOhadDetail
                            {
                                OhdaId = item.OhdaId,
                                OrgnalId = item.OhdaId,
                                ItemId = singleItem.ItemId,
                                ItemName = singleItem.ItemName,
                                ItemDetails = singleItem.ItemDetails,
                                QtyOut = 0,
                                QtyIn = singleItem.QtyIn,
                                ItemStateId = singleItem.ItemStateId,
                                ItemState = singleItem.ItemState,
                                Note = singleItem.Note,
                                CreatedBy = session.UserId,
                                CreatedOn = DateTime.Now,
                                IsDeleted = false,
                            };
                            await hrRepositoryManager.HrOhadDetailRepository.Add(newDetailsEntity);
                            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

                        }
                    }
                }
                //save files
                if (entity.fileDtos != null && entity.fileDtos.Count() > 0)
                {
                    var addFiles = await mainRepositoryManager.SysFileRepository.SaveFiles(entity.fileDtos, item.OhdaId, 104);
                    await mainRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
                }

                await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

                return await Result<HrOhadEditDto>.SuccessAsync(_mapper.Map<HrOhadEditDto>(item), "Ohad updated successfully");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
                return await Result<HrOhadEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
            }

        }

        //public async Task<IResult<HrOhadEditDto>> EditDropOhad(HrOhadEditDto entity, CancellationToken cancellationToken = default)
        //{
        //    if (entity == null) return await Result<HrOhadEditDto>.FailAsync($"{localization.GetMessagesResource("لم يتم اختيار اي عهده لإسقاطها")}");

        //    var Employees = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == entity.EmpId.ToString() && e.IsDeleted == false);
        //    var EmployeesTo = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == entity.EmpIdTo.ToString() && e.IsDeleted == false);

        //    if (Employees == null)
        //    {
        //        return await Result<HrOhadEditDto>.FailAsync("  الموظف غير موجود في قائمة الموظفين");
        //    } 
        //    if (EmployeesTo == null)
        //    {
        //        return await Result<HrOhadEditDto>.FailAsync("  المستلم غير موجود في قائمة الموظفين");
        //    }
        //    var item = await hrRepositoryManager.HrOhadRepository.GetById(entity.OhdaId);

        //       if (item == null) return await Result<HrOhadEditDto>.FailAsync($"--- there is no Data with this id: {entity.OhdaId}---");
        //    item.OhdaId = entity.OhdaId;
        //    item.ModifiedOn = DateTime.Now;
        //    item.ModifiedBy = (int)session.UserId;
        //    item.Note = entity.Note;
        //    item.Code = entity.Code;
        //    item.EmpId = Employees.Id;
        //    item.EmpIdRecipient = EmployeesTo.Id;
        //    item.TransTypeId = 2;
        //    item.IsDeleted = false;
        //    item.OhdaDate = entity.OhdaDate;
        //    if (entity.hrDetails.Any())
        //    {
        //        foreach (var item2 in entity.hrDetails)
        //        {
        //            var newDetailsEntity = new HrOhadDetail
        //            {
        //                OhdaId = item.OhdaId,
        //                ItemId = item2.ItemId,
        //                ItemName = item2.ItemName,
        //                ItemDetails = item2.ItemDetails,
        //                QtyIn = 0,
        //                QtyOut = item2.OtyWantsDroping,
        //                ItemStateId = item2.ItemStateId,
        //                Note = item2.Note,
        //                CreatedBy = session.UserId,
        //            };
        //            await hrRepositoryManager.HrOhadDetailRepository.Add(newDetailsEntity);
        //            await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
        //        }

        //    }

        //    _mapper.Map(entity, item);

        //    hrRepositoryManager.HrOhadRepository.Update(item);

        //    try
        //    {
        //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

        //        return await Result<HrOhadEditDto>.SuccessAsync(_mapper.Map<HrOhadEditDto>(item), "Ohad updated successfully");
        //    }
        //    catch (Exception exp)
        //    {
        //        Console.WriteLine($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
        //        return await Result<HrOhadEditDto>.FailAsync($"EXP in Update at ( {GetType()} ) , Message: {exp.Message} --- {(exp.InnerException != null ? "InnerExp: " + exp.InnerException.Message : "no inner")}");
        //    }

        //}


        //public async Task<IResult<HrOhadDto>> AddDropOhad(HrOhadDto entity, CancellationToken cancellationToken = default)
        //{
        //    if (entity == null) return await Result<HrOhadDto>.FailAsync($"{localization.GetMessagesResource("AddNullEntity")}");

        //    try
        //    {
        //        List<HrOhadDetailVM> OhadDetaiList = new List<HrOhadDetailVM>();

        //        var Employees = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == entity.EmpId.ToString() && e.IsDeleted == false);
        //        var EmployeesTo = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == entity.EmpIdTo.ToString() && e.IsDeleted == false);

        //        if (Employees == null)
        //        {
        //            return await Result<HrOhadDto>.FailAsync("  الموظف غير موجود في قائمة الموظفين");
        //        }
        //        if (EmployeesTo == null)
        //        {
        //            return await Result<HrOhadDto>.FailAsync("  المستلم  غير موجود في قائمة الموظفين");
        //        }

        //        var newHrOhad = new HrOhad
        //        {
        //            EmpId = Employees.Id,
        //            EmpIdRecipient = entity.EmpIdRecipient,
        //            Note = entity.Note,
        //            OhdaDate = entity.OhdaDate,
        //            TransTypeId = 2,

        //            IsDeleted = false,
        //            CreatedBy = session.UserId,
        //            CreatedOn = DateTime.Now,
        //            EmpIdTo = entity.EmpIdTo,

        //        };

        //        await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

        //        var newEntity = await hrRepositoryManager.HrOhadRepository.AddAndReturn(newHrOhad);
        //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

        //        var entityMap = _mapper.Map<HrOhadDto>(newEntity);


        //        if (entity.OhadDetails.Any())

        //        {
        //            foreach (var item2 in entity.OhadDetails)
        //            {
        //                var newDetailsEntity = new HrOhadDetail
        //                {
        //                    OhdaId = entityMap.OhdaId,
        //                    OrgnalId = entityMap.OhdaId,
        //                    ItemId = item2.ItemId,
        //                    ItemName = item2.ItemName,
        //                    ItemDetails = item2.ItemDetails,
        //                    QtyOut = item2.QtyOut,
        //                    QtyIn = 0,
        //                    ItemStateId = item2.ItemStateId,
        //                    ItemState = item2.ItemState,
        //                    Note = item2.Note,
        //                    CreatedBy = session.UserId,

        //                };
        //                await hrRepositoryManager.HrOhadDetailRepository.Add(newDetailsEntity);
        //                await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
        //            }

        //        }
        //        else
        //        {

        //        }



        //        await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);

        //        return await Result<HrOhadDto>.SuccessAsync(entityMap, "Ohad added successfully");
        //    }
        //    catch (Exception exc)
        //    {
        //        return await Result<HrOhadDto>.FailAsync($"EXP in {this.GetType()}, Meesage: {exc.Message}");
        //    }
        //}

        //public async Task<IResult<bool>> AddDropOhad2(List<HrOhadDetailAddDto> addDtos, CancellationToken cancellationToken = default)
        //{
        //    int? code = 0;
        //    if (!addDtos.Any()) return await Result<bool>.FailAsync($"{localization.GetMessagesResource("لم يتم اختيار اي عهده لإسقاطها")}");
        //    await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

        //    foreach (var item in addDtos)
        //    {
        //        var Employees = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == item.EmpId.ToString() && e.IsDeleted == false);
        //        var EmployeesTo = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == item.EmpIdTo.ToString() && e.IsDeleted == false);

        //        if (Employees == null)
        //        {
        //            return await Result<bool>.FailAsync("  الموظف غير موجود في قائمة الموظفين");
        //        }
        //        if (EmployeesTo == null)
        //        {
        //            return await Result<bool>.FailAsync("  المستلم  غير موجود في قائمة الموظفين");
        //        }
        //        if(item.OtyWantsDroping > item.RemainingQuantity )
        //        {
        //            return await Result<bool>.FailAsync("الكمية المراد اسقاطها يجب ان تقل عن الكمية المستلمة");

        //        }if(item.OtyWantsDroping<=0 )
        //        {
        //            return await Result<bool>.FailAsync("يجب ادخال الكمية المراد اسقاطه");
        //        }
        //       var getcodeCount = await hrRepositoryManager.HrOhadRepository.GetAll(x => x.TransTypeId == 2);
        //        if (getcodeCount.Count() >= 1)
        //        {

        //            code = getcodeCount.Count() + 1;
        //        }
        //        var newHrOhad = new HrOhad
        //        {
        //            EmpId = Employees.Id,
        //            EmpIdRecipient = EmployeesTo.Id,
        //            EmpIdTo = EmployeesTo.Id,
        //            Note = item.Note,
        //            OhdaDate = item.OhdaDate,
        //            TransTypeId = 2,
        //            IsDeleted = false,
        //            CreatedBy = session.UserId,
        //            CreatedOn = DateTime.Now,


        //        };



        //        var newEntity = await hrRepositoryManager.HrOhadRepository.AddAndReturn(newHrOhad);
        //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

        //        var newDetailsEntity = new HrOhadDetail
        //        {


        //            OhdaId = newEntity.OhdaId,
        //            ItemId = item.ItemId,
        //            ItemName = item.ItemName,
        //            ItemDetails = item.ItemDetails,
        //            QtyOut = item.OtyWantsDroping,
        //            QtyIn = 0,
        //            ItemStateId = item.ItemStateId,
        //            ItemState = item.ItemState,
        //            Note = item.Note,
        //            CreatedBy = session.UserId,




        //        };
        //        await hrRepositoryManager.HrOhadDetailRepository.Add(newDetailsEntity);
        //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
        //    }
        //    await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
        //    return await Result<bool>.SuccessAsync("Ohad added successfully");

        //}

        //public async Task<IResult<bool>> AddReturnOhad(List<HrOhadDetailAddDto> addDtos, CancellationToken cancellationToken = default)
        //{
        //    int? code = 0;
        //    if (!addDtos.Any()) return await Result<bool>.FailAsync($"{localization.GetMessagesResource("لم يتم اختيار اي عهده لإسقاطها")}");
        //    await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

        //    foreach (var item in addDtos)
        //    {
        //        var Employees = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == item.EmpId.ToString() && e.IsDeleted == false);
        //        var EmployeesTo = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == item.EmpIdTo.ToString() && e.IsDeleted == false);

        //        if (Employees == null)
        //        {
        //            return await Result<bool>.FailAsync("  الموظف غير موجود في قائمة الموظفين");
        //        }
        //        if (EmployeesTo == null)
        //        {
        //            return await Result<bool>.FailAsync("  المستلم  غير موجود في قائمة الموظفين");
        //        }
        //        if (item.QuantityReturned > item.RemainingQuantity)
        //        {
        //            return await Result<bool>.FailAsync("الكمية المراد ارجاعها يجب ان تقل عن الكمية المستلمة");

        //        }
        //        if (item.QuantityReturned <= 0)
        //        {
        //            return await Result<bool>.FailAsync("يجب ادخال الكمية المراد  ارجاعها");
        //        }
        //        var getcodeCount = await hrRepositoryManager.HrOhadRepository.GetAll(x => x.TransTypeId == 3);
        //        if (getcodeCount.Count() >= 1)
        //        {

        //            code = getcodeCount.Count() + 1;
        //        }
        //        var newHrOhad = new HrOhad
        //        {
        //            EmpId = Employees.Id,
        //            EmpIdRecipient = EmployeesTo.Id,
        //            EmpIdTo = EmployeesTo.Id,
        //            Note = item.Note,
        //            Code = code,
        //            OhdaDate = item.OhdaDate,
        //            TransTypeId = 3,
        //            IsDeleted = false,
        //            CreatedBy = session.UserId,
        //            CreatedOn = DateTime.Now,


        //        };



        //        var newEntity = await hrRepositoryManager.HrOhadRepository.AddAndReturn(newHrOhad);
        //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

        //        var newDetailsEntity = new HrOhadDetail
        //        {


        //            OhdaId = newEntity.OhdaId,
        //            ItemId = item.ItemId,
        //            ItemName = item.ItemName,
        //            ItemDetails = item.ItemDetails,
        //            QtyOut = item.QuantityReturned,
        //            QtyIn = 0,
        //            ItemStateId = item.ItemStateId,
        //            ItemState = item.ItemState,
        //            Note = item.Note,
        //            CreatedBy = session.UserId,
        //            OrgnalId= newEntity.OhdaId



        //        };
        //        await hrRepositoryManager.HrOhadDetailRepository.Add(newDetailsEntity);
        //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
        //    }
        //    await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
        //    return await Result<bool>.SuccessAsync(" Return Ohad added successfully");
        //}
        //public async Task<IResult<bool>> AddTransferOhad(List<HrOhadDetailAddDto> addDtos, CancellationToken cancellationToken = default)
        //{
        //    int? code = 0;
        //    int? code2 = 0;
        //    int? code3 = 0;
        //    decimal? qtyOut = 0;
        //    decimal? qtyIn = 0;
        //    if (!addDtos.Any()) return await Result<bool>.FailAsync($"{localization.GetMessagesResource("لم يتم اختيار اي عهده لإسقاطها")}");
        //    await hrRepositoryManager.UnitOfWork.BeginTransactionAsync(cancellationToken);

        //    foreach (var item in addDtos)
        //    {
        //        var Employees = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == item.EmpId.ToString() && e.IsDeleted == false);
        //        var EmployeesTo = await mainRepositoryManager.InvestEmployeeRepository.GetOne(e => e.EmpId == item.EmpIdTo.ToString() && e.IsDeleted == false);

        //        if (Employees == null)
        //        {
        //            return await Result<bool>.FailAsync("  الموظف غير موجود في قائمة الموظفين");
        //        }
        //        if (EmployeesTo == null)
        //        {
        //            return await Result<bool>.FailAsync("  المستلم  غير موجود في قائمة الموظفين");
        //        }
        //        if (item.QuantityReturned > item.RemainingQuantity)
        //        {
        //            return await Result<bool>.FailAsync("الكمية المراد نقلها  يجب ان تقل عن الكمية المستلمة");

        //        }
        //        //if (item.QuantityReturned <= 0)
        //        //{
        //        //    return await Result<bool>.FailAsync("يجب ادخال الكمية المراد نقلها");
        //        //}
        //        var getcodeCount = await hrRepositoryManager.HrOhadRepository.GetAll(x => x.TransTypeId == 4);
        //        if (getcodeCount.Count() >= 1)
        //        {

        //            code = getcodeCount.Count() + 1;
        //        } 
        //        var getcodeCount2 = await hrRepositoryManager.HrOhadRepository.GetAll(x => x.TransTypeId == 1);
        //        if (getcodeCount2.Count() >= 1)
        //        {

        //            code2 = getcodeCount2.Count() + 1;
        //        } 
        //        var getcodeCount3 = await hrRepositoryManager.HrOhadRepository.GetAll(x => x.TransTypeId == 2);
        //        if (getcodeCount2.Count() >= 1)
        //        {

        //            code3 = getcodeCount2.Count() + 1;
        //        }
        //        var newHrOhad = new HrOhadDto   /* إضافة عهدة*/
        //        {
        //            //OhdaId = OhadId,
        //            EmpId = EmployeesTo.Id,
        //            OhdaDate = item.OhdaDate,
        //            TransTypeId = 1,
        //            IsDeleted = false,
        //            CreatedBy = session.UserId,
        //            CreatedOn = DateTime.Now,
        //            Code = code2,

        //        };
        //        var item2 = _mapper.Map<HrOhad>(newHrOhad);

        //        var newEntity = await hrRepositoryManager.HrOhadRepository.AddAndReturn(item2);
        //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


        //        var entityMap = _mapper.Map<HrOhadDto>(newEntity);

        //        var newHrOhadFroDroping = new HrOhad   /* اسقاط عهدة*/
        //        {
        //            EmpId = Employees.Id,
        //            EmpIdTo = EmployeesTo.Id,
        //            Note = item.Note,
        //            OhdaDate = item.OhdaDate,
        //            TransTypeId = 2,
        //            IsDeleted = false,
        //            CreatedBy = session.UserId,
        //            CreatedOn = DateTime.Now,
        //            Code=code3, 


        //        };
        //        var newEntity3 = await hrRepositoryManager.HrOhadRepository.AddAndReturn(newHrOhadFroDroping);  /* اسقاط عهدة*/
        //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);


        //        var newHrOhadForTransfer = new HrOhad  /*مستند تحويل*/
        //        {
        //            EmpId = Employees.Id,
        //            EmpIdRecipient = EmployeesTo.Id,
        //            EmpIdTo = EmployeesTo.Id,
        //            Note = item.Note,
        //            Code = code,
        //            OhdaDate = item.OhdaDate,
        //            TransTypeId = 4,
        //            IsDeleted = false,
        //            CreatedBy = session.UserId,
        //            CreatedOn = DateTime.Now,


        //        };




        //        var newEntity2 = await hrRepositoryManager.HrOhadRepository.AddAndReturn(newHrOhadForTransfer);  /*مستند تحويل*/
        //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
        //        if (item.QuantityReturned <= 0)
        //        {
        //            qtyOut = 0;
        //        }
        //        else
        //        {
        //            qtyOut = item.QuantityReturned;
        //        }
        //        var newDetailsEntity = new HrOhadDetail
        //        {


        //            OhdaId = newEntity2.OhdaId,  /*مستند تحويل*/
        //            ItemId = item.ItemId,
        //            ItemName = item.ItemName,
        //            ItemDetails = item.ItemDetails,

        //           QtyOut = qtyOut,
        //            QtyIn = item.QuantityReturned,
        //            ItemStateId = item.ItemStateId,
        //            ItemState = item.ItemState,
        //            Note = item.Note,
        //            CreatedBy = session.UserId,




        //        };
        //        await hrRepositoryManager.HrOhadDetailRepository.Add(newDetailsEntity);
        //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);
        //        if (item.QuantityReturned <= 0)
        //        {
        //            qtyIn = 0;
        //        }
        //        else
        //        {
        //            qtyIn = item.QuantityReturned;
        //        }
        //        var newDetailsEntity2 = new HrOhadDetail
        //        {


        //            OhdaId = newEntity.OhdaId,  /* إضافة  عهدة*/
        //            ItemId = item.ItemId,
        //            ItemName = item.ItemName,
        //            ItemDetails = item.ItemDetails,
        //            QtyOut = 0,
        //            QtyIn = qtyIn,
        //            ItemStateId = item.ItemStateId,
        //            ItemState = item.ItemState,
        //            Note = item.Note,
        //            CreatedBy = session.UserId,




        //        };
        //        await hrRepositoryManager.HrOhadDetailRepository.Add(newDetailsEntity2);
        //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

        //         var newDetailsEntity3 = new HrOhadDetail
        //        {


        //            OhdaId = newEntity3.OhdaId,  /* اسقاط  عهدة*/
        //            ItemId = item.ItemId,
        //            ItemName = item.ItemName,
        //            ItemDetails = item.ItemDetails,
        //            QtyOut = 0,
        //            QtyIn = 0,
        //            ItemStateId = item.ItemStateId,
        //            ItemState = item.ItemState,
        //            Note = item.Note,
        //            CreatedBy = session.UserId,




        //        };
        //        await hrRepositoryManager.HrOhadDetailRepository.Add(newDetailsEntity3);
        //        await hrRepositoryManager.UnitOfWork.CompleteAsync(cancellationToken);

        //    }
        //    await hrRepositoryManager.UnitOfWork.CommitTransactionAsync(cancellationToken);
        //    return await Result<bool>.SuccessAsync("Return Ohad added successfully");
        //}

    }
}
