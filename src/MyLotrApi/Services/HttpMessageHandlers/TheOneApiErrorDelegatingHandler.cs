using MyLotrApi.Exceptions;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MyLotrApi.Services.HttpMessageHandlers
{
    public class TheOneApiErrorDelegatingHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new TheOneApiException(response.StatusCode, response.ReasonPhrase);
            }

            return response;
        }
    }
}
