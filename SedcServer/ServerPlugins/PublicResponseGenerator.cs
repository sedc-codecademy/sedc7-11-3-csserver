using System;
using System.Collections.Generic;
using System.Text;
using ServerInterfaces;

namespace ServerPlugins
{
    public class PublicResponseGenerator : StaticResponseGenerator
    {
        public PublicResponseGenerator(string folderName) : base(folderName)
        {
        }

        public override bool IsInterested(Request request, ILogger logger)
        {
            var path = $"public/{FolderName}";
            return request.Path.StartsWith(path, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
