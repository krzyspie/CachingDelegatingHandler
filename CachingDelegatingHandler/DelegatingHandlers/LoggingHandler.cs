namespace CachingDelegatingHandler.DelegatingHandlers
{
    public class LoggingHandler : DelegatingHandler
    {
        private readonly ILogger<LoggingHandler> logger;

        public LoggingHandler(ILogger<LoggingHandler> logger)
        {
            this.logger = logger;
        }
        protected override async Task<HttpResponseMessage> SendAsync(
           HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            this.logger.LogInformation($"Making {request.Method} request.");

             var response = await base.SendAsync(request, cancellationToken);

            this.logger.LogInformation($"Response status: {response.StatusCode}");
            
            return response;
        }
    }
}
