using Massive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hummingbird.Data
{
    public class DynamicDataProvider : IDynamicDataProvider
    {
        private readonly string _connectionString;

        public DynamicDataProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<dynamic> Execute(string sprocName, object parameters)
        {
            var dynamicModel = new DynamicModel(_connectionString);
            var arguments = parameters.PrepareArguments(sprocName);
            var results = dynamicModel.Query(arguments.Item1, arguments.Item2);
            return results;
        }

        public void ExecuteNonQuery(string sprocName, dynamic parameters)
        {
            var dynamicModel = new DynamicModel(_connectionString);
            var arguments = parameters.PrepareArguments(sprocName);
            dynamicModel.Query(arguments.Item1, arguments.Item2);
        }
    }
}
