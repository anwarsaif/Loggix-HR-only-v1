using Logix.Application.DTOs.HR;
using Logix.Application.Wrapper;
using Logix.Domain.HR;
using System.Linq.Expressions;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrOhadService : IGenericQueryService<HrOhadDto, HrOhadVw>, IGenericWriteService<HrOhadDto, HrOhadEditDto>
    {
        // Task<IResult<HrOhadDetailDto>> AddHrOhadDetail(HrOhadDetailDto entity, CancellationToken cancellationToken = default);
        Task<IResult<HrOhadDto>> AddDropOhad(HrOhadDto entity, CancellationToken cancellationToken = default);
        // Task<IResult<HrOhadEditDto>> EditDropOhad(HrOhadEditDto entity, CancellationToken cancellationToken = default);
        //Task<IResult<bool>> AddDropOhad2(List<HrOhadDetailAddDto> addDtos, CancellationToken cancellationToken = default);
        Task<IResult<HrOhadDto>> AddReturnOhad(HrOhadDto entity, CancellationToken cancellationToken = default);
        Task<IResult<HrOhadDto>> AddTransferOhad(HrOhadDto entity, CancellationToken cancellationToken = default);
    //Task<IResult<bool>> AddTransferOhad(List<HrOhadDetailAddDto> addDtos, CancellationToken cancellationToken = default);

    //Task<IResult<HrOhadDetailDto>> removeHrOhadDetail(int Id, CancellationToken cancellationToken = default);
    //Task<IResult<HrOhadDto>> removeHrDropingOhad(long Id, CancellationToken cancellationToken = default);
    //Task<IResult<HrOhadDto>> removeHrReturnOhad(long Id, CancellationToken cancellationToken = default);
    Task<IResult<List<HRRPOhadFilterDto>>> RPOhadSerach(HRRPOhadFilterDto filter, CancellationToken cancellationToken = default);


  }

}
