using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ServerInterfaces;
using ServerPlugins.SqlServer.CommandResponders;

namespace ServerPlugins.SqlServer.CommandResponders
{
    internal class TableData : ICommandResponder
    {
        public string ConnectionString { get; private set; }
        public string TableName { get; private set; }
        public QueryParamCollection QueryParams { get; set; }

        public TableData(string connectionString, string tableName, QueryParamCollection queryParams)
        {
            ConnectionString = connectionString;
            TableName = tableName;
            QueryParams = queryParams;
        }

        public async Task<Response> GetResponse()
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();
                // !!!AGAIN, THIS IS WIDE OPEN TO SQL INJECTION, USE A DYNAMIC SQL LIBRARY IF YOU REALLY NEED THIS KIND OF CODE!!!
                var sql = $@"Select * from {TableName}";
                var filterClauses = QueryParams.GetAllParams().Select(param => $"{param.Key} like '{param.Value}%'");
                var whereClause = string.Join(" and ", filterClauses);
                if (!string.IsNullOrEmpty(whereClause))
                {
                    sql = $"{sql} where {whereClause}";
                }

                using (var command = new SqlCommand(sql, cnn))
                {

                    using (var dr = await command.ExecuteReaderAsync())
                    {
                        var schema = dr.GetColumnSchema();
                        var columnNames = schema.Select(cs => cs.ColumnName);
                        object[] values = new object[columnNames.Count()];

                        var results = new List<Dictionary<string, string>>();

                        while (dr.Read())
                        {
                            var columnCount = dr.GetValues(values);
                            var objectDictionary = new Dictionary<string, string>();
                            foreach (var column in schema)
                            {
                                objectDictionary.Add(column.ColumnName, dr.GetValue(column.ColumnOrdinal.Value).ToString());
                            }
                            results.Add(objectDictionary);
                        }

                        var body = SqlHelpers.GenerateJsonData(results);

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