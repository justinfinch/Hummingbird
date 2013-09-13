using Hummingbird.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hummingbird.Domain
{
    public interface IEntity : IObjectWithState
    {
        object GetKey();
    }
}
