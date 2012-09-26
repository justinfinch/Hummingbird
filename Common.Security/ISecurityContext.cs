using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Security
{
    public interface ISecurityContext
    {
        string CurrentUser { get; }
    }
}
