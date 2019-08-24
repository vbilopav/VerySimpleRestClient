using System.Net;
using System.Net.Http.Headers;


namespace VerySimpleRestClient
{
    public class SimpleResponse
    {
        public HttpResponseHeaders Headers { get; internal set; }
        public string ContentType { get; internal set; }
        public HttpStatusCode StatusCode { get; internal set; }
        public string ReasonPhrase { get; internal set; }
        public bool IsSuccessStatusCode { get; internal set; }
    }
}
