using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hummingbird.Domain
{
    public interface IVersionedEntity : IEntity
    {
        byte[] RowVersion { get; }
    }
}
