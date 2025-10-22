using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.Base
{
    public interface IBaseEntity<T>
    {
        T Id { get; set; }
    }
}
