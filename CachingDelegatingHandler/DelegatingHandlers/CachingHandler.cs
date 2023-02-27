using Microsoft.Extensions.Caching.Memory;

namespace CachingDelegatingHandler.DelegatingHandlers
{
    public class CachingHandler : DelegatingHandler
    {
        private readonly IMemoryCache memoryCache;

        public CachingHandler(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
           HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Get)
            {
                HttpResponseMessage result = this.memoryCache.Get<HttpResponseMessage>(request.RequestUri);

                if (result is null)
                {
                    var response = await base.SendAsync(request, cancellationToken);
                    if (response.IsSuccessStatusCode)
                    {
                        this.memoryCache.Set<HttpResponseMessage>(request.RequestUri, response, TimeSpan.FromHours(1));
                    }

                    return response;
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}




