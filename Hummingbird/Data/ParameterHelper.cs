using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hummingbird.Data
{
    public static class ParameterHelper
    {
        public static Tuple<string, object[]>PrepareSprocArguments(this object parameters, string storedProcedure)
        {
            var parameterNames = new List<string>();
            var parameterValues = new List<object>();

            if (parameters != null)
            {
                foreach (PropertyInfo propertyInfo in parameters.GetType().GetProperties())
                {
                    string name = "@" + propertyInfo.Name;
                    object value = propertyInfo.GetValue(parameters, null);

                    parameterNames.Add(name);

                    parameterValues.Add(GetSqlParameter(name, value));
                }
            }

            if (parameterNames.Count > 0)
                storedProcedure += " " + string.Join(", ", parameterNames);

            return new Tuple<string, object[]>(storedProcedure, parameterValues.ToArray());
        }

        public static Tuple<string, object[]> PrepareSprocArguments(this Dictionary<string, object> parameters, string storedProcedure)
        {
            var parameterNames = new List<string>();
            var parameterValues = new List<object>();

            if (parameters != null)
            {
                foreach (var kvp in parameters)
                {
                    string name = "@" + kvp.Key;
                    object value = kvp.Value;

                    parameterNames.Add(name);

                    parameterValues.Add(GetSqlParameter(name, value));
                }
            }

            if (parameterNames.Count > 0)
                storedProcedure += " " + string.Join(", ", parameterNames);

            return new Tuple<string, object[]>(storedProcedure, parameterValues.ToArray());
        }

        private static SqlParameter GetSqlParameter(string name, object value)
        {
            if (value is SqlParameter)
                return value as SqlParameter;

            if (value is IEnumerable)
            {
                var list = value as IEnumerable;
                var dataTable = new DataTable();
                bool isFirst = true;
                string parameterTypeName = null;

                foreach (var listItem in list)
                {
                    if (isFirst)
                    {
                        var listItemType = listItem.GetType();
                        parameterTypeName = listItemType.Name.ToUpper() + "ListType";
                        dataTable.Columns.Add(listItemType.Name.ToUpper() + "Value", listItemType);
                        isFirst = false;
                    }
                    
                    dataTable.Rows.Add(listItem);
                }
                    

                return new SqlParameter
                {
                    ParameterName = name,
                    SqlDbType = SqlDbType.Structured,
                    Direction = ParameterDirection.Input,
                    TypeName = parameterTypeName,
                    Value = dataTable
                };
            }

            return new SqlParameter(name, value ?? DBNull.Value);
        }

    }
}
