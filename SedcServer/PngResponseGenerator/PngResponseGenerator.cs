using System;
using System.IO;
using System.Threading.Tasks;
using ServerInterfaces;

namespace PngResponseGeneratorLib
{
    public class PngResponseGenerator : IResponseGenerator
    {
        public int Count { get; private set; } = 0;

        public async Task<Response> Generate(Request request, ILogger logger)
        {
            Count += 1;
            logger.Debug($"Start generating response #{Count}");

            var bytes = await File.ReadAllBytesAsync("ubava_slika.jpg");

            logger.Debug($"Read image {request.Path} from disk");
            return new Response
            {
                Bytes = bytes,
                Type = ResponseType.Binary,
                ContentType = "image/png"
            };
        }

        public bool IsInterested(Request request, ILogger logger)
        {
            logger.Info("PngResponseGenerator determining interest");

            return request.Path.EndsWith(".png");
        }
    }
}
