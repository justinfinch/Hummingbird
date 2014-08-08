using System;
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
            return Execute(parameters.PrepareSprocArguments(sprocName));
        }

        public void ExecuteNonQuery(string sprocName, object parameters)
        {
            ExecuteNonQuery(parameters.PrepareSprocArguments(sprocName));
        }
        
        public IEnumerable<dynamic> ExecuteCommand(string command, object parameters)
        {
            return Execute(parameters.PrepareSqlCommandArguments(command));
        }
        
        public IEnumerable<dynamic> Execute(string sprocName, Dictionary<string, object> parameters)
        {
            return Execute(parameters.PrepareSprocArguments(sprocName));
        }

        public void ExecuteNonQuery(string sprocName, Dictionary<string, object> parameters)
        {
            ExecuteNonQuery(parameters.PrepareSprocArguments(sprocName));
        }

        public IEnumerable<dynamic> ExecuteCommand(string command, Dictionary<string, object> parameters)
        {
            return Execute(parameters.PrepareSqlCommandArguments(command));
        }

        private IEnumerable<dynamic> Execute(Tuple<string, object[]> arguments)
        {
            var dynamicModel = new DynamicModel(_connectionString);
            var results = dynamicModel.Query(arguments.Item1, arguments.Item2);
            return results;
        }

        private void ExecuteNonQuery(Tuple<string, object[]> arguments)
        {
            var dynamicModel = new DynamicModel(_connectionString);
            dynamicModel.Query(arguments.Item1, arguments.Item2);
        }

    }
}
