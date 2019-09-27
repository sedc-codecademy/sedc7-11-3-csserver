using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerInterfaces;
using ServerPlugins.SqlServer.CommandResponders;

namespace ServerPlugins.SqlServer
{
    public class SqlServerResponseGenerator : IResponseGenerator
    {
        public int Count { get; }

        public string DatabaseName { get; private set; }
        public string ConnectionString { get; private set; }

        public SqlServerResponseGenerator(string databaseName, string connectionString)
        {
            DatabaseName = databaseName;
            ConnectionString = connectionString;
        }

        public async Task<Response> Generate(Request request, ILogger logger)
        {
            var path = request.Path.Split("/").Skip(2);
            var command = GetCommand(path);
            var response = await GetResponse(command);
            return response;
        }

        private async Task<Response> GetResponse(SqlResponseCommand command)
        {
            switch (command)
            {
                case SqlResponseCommand.GeneralInfo:
                    return await new GeneralInfo(ConnectionString).GetResponse();
                case SqlResponseCommand.TableList:
                    return new TableList(ConnectionString).GetResponse();
            }
            return Response.EmptyResponse;
        }

        private SqlResponseCommand GetCommand(IEnumerable<string> path)
        {
            if (path.Count() == 0)
            {
                return SqlResponseCommand.GeneralInfo;
            }
            if (path.Count() == 1)
            {
                if (path.First() == "tables")
                {
                    return SqlResponseCommand.TableList;
                }
            }
            return SqlResponseCommand.GeneralInfo;
        }

        public bool IsInterested(Request request, ILogger logger)
        {
            var path = $"sql/{DatabaseName}/";
            return request.Path.StartsWith(path, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
