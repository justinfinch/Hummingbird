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
        IEnumerable<dynamic> Execute(string sprocName, dynamic parameters);
        void ExecuteNonQuery(string sprocName, dynamic parameters);
        IEnumerable<dynamic> ExecuteCommand(string command, dynamic parameters);
    }
}
