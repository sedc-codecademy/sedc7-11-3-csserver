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
            var path = request.Path.Split("/", StringSplitOptions.RemoveEmptyEntries).Skip(2);
            var command = GetCommand(path);
            var response = await GetResponse(command);
            return response;
        }

        private async Task<Response> GetResponse(SqlResponseCommand command)
        {
            //var responder = command switch {
            //    SqlResponseCommand.GeneralInfo => new GeneralInfo(ConnectionString),
            //    SqlResponseCommand.TableList => new TableList(ConnectionString),
            //    _ => new Error()
            //};
            //return responder.GetResponse();

            ICommandResponder responder;
            switch (command)
            {
                case SqlResponseCommand.GeneralInfo:
                    responder = new GeneralInfo(ConnectionString);
                    break;
                case SqlResponseCommand.TableList:
                    responder = new TableList(ConnectionString);
                    break;
                default:
                    responder = new Error();
                    break;
            }

            return await responder.GetResponse();
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
            return SqlResponseCommand.Error;
        }

        public bool IsInterested(Request request, ILogger logger)
        {
            var path = $"sql/{DatabaseName}/";
            return request.Path.StartsWith(path, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
