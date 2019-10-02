using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerInterfaces;

namespace ServerPlugins.SqlServer.CommandResponders
{
    class TableList: ICommandResponder
    {
        public string ConnectionString { get; private set; }

        public TableList(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public async Task<Response> GetResponse()
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();
                using (var command = new SqlCommand("select name from sys.objects where type = 'U' and name != 'sysdiagrams'", cnn))
                {
                    using (var dr = await command.ExecuteReaderAsync())
                    {
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
        }
    }
}
