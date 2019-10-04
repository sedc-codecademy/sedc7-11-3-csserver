using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerInterfaces;
using ServerPlugins.SqlServer.CommandResponders;

namespace ServerPlugins.SqlServer
{
    public class SqlServerResponseGenerator : IResponseGenerator, IDisposable
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
            if (request == null)
            {
                request = Request.EmptyRequest;
            }
            var response = await GetResponse(request, logger);
            return response;
        }

        private async Task<Response> GetResponse(Request request, ILogger logger)
        {
            //var responder = command switch {
            //    SqlResponseCommand.GeneralInfo => new GeneralInfo(ConnectionString),
            //    SqlResponseCommand.TableList => new TableList(ConnectionString),
            //    _ => new Error()
            //};
            //return responder.GetResponse();

            var path = request.Path.Split("/", StringSplitOptions.RemoveEmptyEntries).Skip(2);
            var command = GetCommand(path);
            ICommandResponder responder;
            switch (command)
            {
                case SqlResponseCommand.GeneralInfo:
                    responder = new GeneralInfo(ConnectionString);
                    break;
                case SqlResponseCommand.TableList:
                    responder = new TableList(ConnectionString);
                    break;
                case SqlResponseCommand.TableData:
                    {
                        var tableName = path.Skip(1).First();
                        responder = new TableData(ConnectionString, tableName, request.Query);
                        break;
                    }
                case SqlResponseCommand.TableSchema:
                    {
                        var tableName = path.Skip(1).First();
                        responder = new TableSchema(ConnectionString, tableName);
                        break;
                    }
                default:
                    responder = new Error();
                    break;
            }

            return await responder.GetResponse();
        }

        private SqlResponseCommand GetCommand(IEnumerable<string> path)
        {
            // this code could be better, but it doesn't have to
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
            if (path.Count() == 2)
            {
                if (path.First() == "tables")
                {
                    return SqlResponseCommand.TableData;
                }
            }
            if (path.Count() == 3)
            {
                if ((path.First() == "tables") && (path.Skip(2).First() == "schema"))
                {
                    return SqlResponseCommand.TableSchema;
                }
            }
            return SqlResponseCommand.Error;
        }

        public bool IsInterested(Request request, ILogger logger)
        {
            var path = $"sql/{DatabaseName}/";
            return request.Path.StartsWith(path, StringComparison.InvariantCultureIgnoreCase);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    Console.WriteLine("Disposing of connections");
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SqlServerResponseGenerator() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
