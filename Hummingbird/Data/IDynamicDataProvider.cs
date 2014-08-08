using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hummingbird.Data
{
    public interface IDynamicDataProvider
    {
        IEnumerable<dynamic> ExecuteSproc(string sprocName, object parameters);
        IEnumerable<dynamic> ExecuteSproc(string sprocName, Dictionary<string, object> parameters);
        IEnumerable<dynamic> ExecuteCommand(string command, params object[] parameters);
        
    }
}
