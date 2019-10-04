using System;
using System.Collections.Generic;
using System.Text;
using ServerInterfaces;

namespace ServerPlugins
{
    public class PublicResponseGenerator : StaticResponseGenerator
    {
        public string UrlPath { get; private set; }

        public PublicResponseGenerator(string folderName, string urlPath) : base(folderName)
        {
            UrlPath = urlPath;
        }

        public override bool IsInterested(Request request, ILogger logger)
        {
            var path = $"public/{UrlPath}";
            return request.Path.StartsWith(path, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
