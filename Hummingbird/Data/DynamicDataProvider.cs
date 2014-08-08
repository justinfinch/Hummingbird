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

        public IEnumerable<dynamic> ExecuteSproc(string sprocName, object parameters)
        {
            return Execute(parameters.PrepareSprocArguments(sprocName));
        }

        public IEnumerable<dynamic> ExecuteSproc(string sprocName, Dictionary<string, object> parameters)
        {
            return Execute(parameters.PrepareSprocArguments(sprocName));
        }
        
        public IEnumerable<dynamic> ExecuteCommand(string command, params object[] parameters)
        {
            return Execute(new Tuple<string, object[]>(command, parameters));
        }
        
        private IEnumerable<dynamic> Execute(Tuple<string, object[]> arguments)
        {
            var dynamicModel = new DynamicModel(_connectionString);
            var results = dynamicModel.Query(arguments.Item1, arguments.Item2);
            return results;
        }

    }
}
