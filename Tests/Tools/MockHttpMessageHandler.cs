using System.Net;

namespace HanaTests.Tools
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _sendAsync;

        public MockHttpMessageHandler()
        {
            _sendAsync = (request, cancellationToken) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        public MockHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsync)
        {
            _sendAsync = sendAsync ?? throw new ArgumentNullException(nameof(sendAsync));
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _sendAsync(request, cancellationToken);
        }

        public MockHttpMessageHandler When(HttpMethod method, string url, Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> response)
        {
            _sendAsync = (request, cancellationToken) =>
            {
                if (request.Method == method && request.RequestUri.ToString() == url)
                {
                    return response(request, cancellationToken);
                }

                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
            };

            return this;
        }

        public MockHttpMessageHandler When(HttpMethod method, string url)
        {
            return When(method, url, (request, cancellationToken) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));
        }

        public MockHttpMessageHandler Respond(HttpStatusCode statusCode, HttpContent content)
        {
            _sendAsync = (request, cancellationToken) => Task.FromResult(new HttpResponseMessage(statusCode) { Content = content });
            return this;
        }
    }
}
