using System;
using System.Collections.Generic;
using System.Text;
using ServerInterfaces;

namespace ServerPlugins.SqlServer.CommandResponders
{
    class TableList
    {
        private string connectionString;

        public TableList(string connectionString)
        {
            this.connectionString = connectionString;
        }

        internal Response GetResponse()
        {
            throw new NotImplementedException();
        }
    }
}
