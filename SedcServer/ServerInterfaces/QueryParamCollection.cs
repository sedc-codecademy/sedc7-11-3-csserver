using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerInterfaces
{
    public class QueryParamCollection
    {
        private readonly Dictionary<string, string> data = new Dictionary<string, string>();

        public QueryParamCollection(string queryString)
        {
            var parameters = queryString.Split("&", StringSplitOptions.RemoveEmptyEntries);
            foreach (var parameter in parameters)
            {
                var eq = parameter.IndexOf("=");
                var key = parameter.Substring(0, eq);
                var value = parameter.Substring(eq + 1);
                data.Add(key, value);
            }
        }

        public static QueryParamCollection Empty
        {
            get
            {
                return new QueryParamCollection(string.Empty);
            }
        }

        public bool HasParam(string paramName)
        {
            return data.ContainsKey(paramName);
        }

        public string GetParam(string paramName)
        {
            if (!data.ContainsKey(paramName))
            {
                return string.Empty;
            }
            return data[paramName];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var kvp in data)
            {
                sb.AppendLine($"{kvp.Key} = {kvp.Value}");
            }
            return sb.ToString();
        }
    }
}