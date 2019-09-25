using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServerInterfaces;

namespace ServerCore.Responses
{
    internal class ResponseFactory
    {
        private List<IResponseGenerator> registeredGenerators = new List<IResponseGenerator>();
        private List<IResponsePostProcessor> registeredPostProcessors = new List<IResponsePostProcessor>();

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

        internal IEnumerable<IResponsePostProcessor> GetPostProcessors(Response response, ILogger logger)
        {
            return registeredPostProcessors.Where(processor => processor.IsInterested(response, logger));
        }

        internal async Task<Response> RunPostProcessors(Response response, ILogger logger)
        {
            logger.Info("Running post processors");
            var postProcessors = GetPostProcessors(response, logger);
            var processedResponse = response;
            foreach (var pp in postProcessors)
            {
                logger.Info($"Running post processor {pp.GetType().FullName}");
                processedResponse = await pp.Process(processedResponse, logger);
            }
            return processedResponse;
        }

        internal void RegisterPostProcessor(IResponsePostProcessor postProcessor)
        {
            registeredPostProcessors.Insert(0, postProcessor);
        }
    }
}