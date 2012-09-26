using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Security
{
    public class DummySecurityContext : ISecurityContext
    {
        public string CurrentUser
        {
            get { return "Dummy User"; }
        }
    }
}
