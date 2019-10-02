using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ServerInterfaces;
using ServerPlugins.SqlServer.CommandResponders;

namespace ServerPlugins.SqlServer.CommandResponders
{
    internal class TableSchema : ICommandResponder
    {
        public string ConnectionString { get; private set; }
        public string TableName { get; private set; }

        public TableSchema(string connectionString, string tableName)
        {
            ConnectionString = connectionString;
            TableName = tableName;
        }

        public async Task<Response> GetResponseWithInformationSchema()
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();
                using (var command = new SqlCommand($@"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tableName", cnn))
                {
                    //SqlParameter tableNameParam = new SqlParameter("@tableName", System.Data.SqlDbType.NVarChar);
                    //tableNameParam.Value = TableName;

                    //command.Parameters.Add(tableNameParam);

                    command.Parameters.AddWithValue("@tableName", TableName);

                    using (var dr = await command.ExecuteReaderAsync())
                    {
                        var columnNames = new List<string>();
                        while (dr.Read())
                        {
                            columnNames.Add(dr.GetString(0));
                        }
                        var columnNamesStr = string.Join(", ", columnNames.Select(tn => $"\"{tn}\""));
                        var body = $@"{{""columnNames"":[{columnNamesStr}]}}";

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

        public async Task<Response> GetResponse()
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();
                // fully open to SQL Injection, need to use some dynamic sql library
                using (var command = new SqlCommand($@"Select * from {TableName} where 1=0", cnn))
                {
                    using (var dr = await command.ExecuteReaderAsync())
                    {
                        var schema = dr.GetColumnSchema();
                        var columnNames = schema.Select(cs => cs.ColumnName);
                        var columnNamesStr = string.Join(", ", columnNames.Select(tn => $"\"{tn}\""));
                        var body = $@"{{""columnNames"":[{columnNamesStr}]}}";

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