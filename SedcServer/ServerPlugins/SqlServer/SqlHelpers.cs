using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerPlugins.SqlServer
{
    public class SqlHelpers
    {
        public static string GenerateJsonData(List<Dictionary<string, string>> data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            sb.Append(string.Join(",", data.Select(item => GenerateJsonData(item))));
            sb.Append("]");
            return sb.ToString();
        }

        public static string GenerateJsonData(Dictionary<string, string> item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append(string.Join(",", item.Select(kvp => $"\"{kvp.Key}\": \"{kvp.Value}\"")));
            sb.Append("}");
            return sb.ToString();
        }
    }
}
