﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using ServerInterfaces;

namespace ServerCore.Responses
{
    class ImageResponseGenerator : IResponseGenerator
    {
        private Dictionary<string, byte[]> cache = new Dictionary<string, byte[]>();

        public int Count { get; private set; } = 0;

        public Response Generate(Request request, ILogger logger)
        {
            Count += 1;
            logger.Debug($"Start generating response #{Count}");

            if (!cache.ContainsKey(request.Path))
            {
                logger.Debug($"Loading image {request.Path} to cache");
                // delay 10s to simulate long load times
                Thread.Sleep(10000);
                var bytes = File.ReadAllBytes("ubava_slika.jpg");
                cache.Add(request.Path, bytes);
            }

            logger.Debug($"Reading image {request.Path} from cache");
            return new Response
            {
                Bytes = cache[request.Path],
                Type = ResponseType.Binary
            };
        }
    }
}
