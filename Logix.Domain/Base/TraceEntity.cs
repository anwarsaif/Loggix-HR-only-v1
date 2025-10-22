using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.Base
{
    public abstract class TraceEntity
    {
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }

        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
