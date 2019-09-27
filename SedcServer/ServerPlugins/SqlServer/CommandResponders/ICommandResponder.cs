using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ServerInterfaces;

namespace ServerPlugins.SqlServer.CommandResponders
{
    interface ICommandResponder
    {
        Task<Response> GetResponse();
    }
}
