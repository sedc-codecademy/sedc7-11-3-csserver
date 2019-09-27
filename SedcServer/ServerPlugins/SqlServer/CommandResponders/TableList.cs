using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerInterfaces;

namespace ServerPlugins.SqlServer.CommandResponders
{
    class TableList
    {
        private string ConnectionString { get; set; }

        public TableList(string connectionString)
        {
            ConnectionString = connectionString;
        }

        internal async Task<Response> GetResponse()
        {
            var cnn = new SqlConnection(ConnectionString);
            cnn.Open();
            var command = new SqlCommand("select name from sys.objects where type = 'U' and name != 'sysdiagrams'", cnn);
            var dr = await command.ExecuteReaderAsync();
            var tableNames = new List<string>();
            while (dr.Read())
            {
                tableNames.Add(dr.GetString(0));
            }
            var tableNamesStr = string.Join(", ", tableNames.Select(tn => $"\"{tn}\""));
            var body = $@"{{""tableNames"":[{tableNamesStr}]}}";

            return new Response
            {
                ContentType = ContentTypes.JsonApplication,
                ResponseCode = ResponseCode.Ok,
                Type = ResponseType.Text,
                Body = body
            };
        }
    }
}
