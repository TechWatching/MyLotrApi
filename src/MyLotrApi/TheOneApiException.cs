using System;
using System.Net;

namespace MyLotrApi
{
    [Serializable]
    internal class TheOneApiException : Exception
    {
        public TheOneApiException(HttpStatusCode statusCode, string? reasonPhrase)
        {
            StatusCode = statusCode;
            ReasonPhrase = reasonPhrase;
        }

        public HttpStatusCode StatusCode { get; }
        public string? ReasonPhrase { get; }
    }
}