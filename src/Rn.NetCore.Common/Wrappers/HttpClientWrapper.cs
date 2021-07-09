using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rn.NetCore.Common.Wrappers
{
  public interface IHttpClient
  {
    HttpRequestHeaders DefaultRequestHeaders { get; }

    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
  }

  public class HttpClientWrapper : IHttpClient
  {
    public HttpRequestHeaders DefaultRequestHeaders
      => _httpClient.DefaultRequestHeaders;

    private readonly HttpClient _httpClient;

    // Constructors
    public HttpClientWrapper()
    {
      _httpClient = new HttpClient();
    }

    public HttpClientWrapper(HttpMessageHandler timeoutHandler)
    {
      _httpClient = new HttpClient(timeoutHandler)
      {
        Timeout = Timeout.InfiniteTimeSpan
      };
    }

    // Exposed methods
    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
      => await _httpClient.SendAsync(request);
  }
}
