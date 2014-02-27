using Massive;
using System.Collections.Generic;

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
            var arguments = parameters.PrepareSprocArguments(sprocName);
            var results = dynamicModel.Query(arguments.Item1, arguments.Item2);
            return results;
        }

        public void ExecuteNonQuery(string sprocName, object parameters)
        {
            var dynamicModel = new DynamicModel(_connectionString);
            var arguments = parameters.PrepareSprocArguments(sprocName);
            dynamicModel.Query(arguments.Item1, arguments.Item2);
        }


        public IEnumerable<dynamic> ExecuteCommand(string command, object parameters)
        {
            var dynamicModel = new DynamicModel(_connectionString);
            var arguments = parameters.PrepareSqlCommandArguments(command);
            var results = dynamicModel.Query(arguments.Item1, arguments.Item2);
            return results;
        }
    }
}
