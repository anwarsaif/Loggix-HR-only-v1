using Logix.Application.DTOs.HR;
using Logix.Domain.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.Interfaces.IServices.HR
{
    public interface IHrStructureService : IGenericQueryService<HrStructureDto, HrStructureVw>, IGenericWriteService<HrStructureDto, HrStructureEditDto>
    {
    }
}
