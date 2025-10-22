using Logix.Application.DTOs.HR;
using Logix.Domain.HR;

namespace Logix.Application.Interfaces.IServices.HR
{
	public interface IHrJobCategoryService : IGenericQueryService<HrJobCategoryDto, HrJobCategoriesVw>, IGenericWriteService<HrJobCategoryDto, HrJobCategoryEditDto>
    {

    }


}
