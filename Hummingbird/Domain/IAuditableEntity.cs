using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hummingbird.Domain
{
    public interface IAuditableEntity : IEntity
    {
        DateTime CreatedDate { get; }
        DateTime LastModifiedDate { get; }
        string CreatedBy { get; }
        string LastModifiedBy { get; }
    }
}
