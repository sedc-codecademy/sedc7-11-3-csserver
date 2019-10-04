using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using PngResponseGeneratorLib;
using ServerCore;
using ServerInterfaces;
using ServerPlugins;
using ServerPlugins.SqlServer;

namespace ServerRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var server = new WebServer())
            {
                server
                    .UseResponseGenerator<PngResponseGenerator>()
                    .UseResponseGenerator<PostMethodResponseGenerator>()
                    .UseResponseGenerator(new StaticResponseGenerator(@"C:\Users\Weko\OneDrive\Memes"))
                    .UseResponseGenerator(new PublicResponseGenerator(@"C:\Source\SEDC\sedc7-04-ajs\g2\Workshop\Game\Code", "code"))
                    .UseResponseGenerator(new PublicResponseGenerator(@"C:\Source\SEDC\sedc7-11-3-csserver\SedcServer\DatabaseBrowserSite", "db-browser"))
                    .UseResponsePostProcessor<NotFoundPostProcessor>()
                    .UseResponseGenerator(new SqlServerResponseGenerator("Books", "Server=.;Database=Books-2;Trusted_Connection=True;"))
                    .UseResponseGenerator(new SqlServerResponseGenerator("DSDS", "Server=.;Database=OneCrew.QA.DSDS;Trusted_Connection=True;"))
                    .UseResponseGenerator(new SqlServerResponseGenerator("invalid", string.Empty));


                var result = server.Run();
                result.Wait();
            }
        }
    }
}
