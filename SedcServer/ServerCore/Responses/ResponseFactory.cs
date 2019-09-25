using System;
using System.Collections.Generic;
using System.Linq;
using ServerInterfaces;

namespace ServerCore.Responses
{
    internal class ResponseFactory
    {
        private List<IResponseGenerator> registeredGenerators = new List<IResponseGenerator>();

        public ResponseFactory ()
        {
            registeredGenerators.Add(new BaseResponseGenerator(HeaderOptions.ShowHeaders));
            registeredGenerators.Add(new ImageResponseGenerator());
            registeredGenerators.Add(new BaseResponseGenerator());
        }

        internal IResponseGenerator GetGenerator(Request request, ILogger logger)
        {
            return registeredGenerators.First(generator => generator.IsInterested(request, logger));
        }

        internal void RegisterGenerator(IResponseGenerator generator)
        {
            registeredGenerators.Insert(0, generator);
        }
    }
}