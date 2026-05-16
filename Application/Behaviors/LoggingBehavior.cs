using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            var timer = new Stopwatch();

            timer.Start();
            
            var response = await next();
            
            timer.Stop();

            var timeInMs = timer.ElapsedMilliseconds;

            if (timeInMs > 2000)
                logger.LogWarning("[Performance] the request {RequestName} took {TimeInMs}ms",
                    typeof(TRequest).Name, timeInMs);
            
            return response;
        }
    }
}
