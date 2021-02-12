using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Rn.NetCore.Common.Helpers;
using Rn.NetCore.Common.Logging;

namespace Rn.NetCore.Common.Services
{
  public interface IBasicHttpService
  {
    Task<HttpResponseMessage> GetUrlAsync(string url);
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, int timeoutMs = 10000);
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, CancellationToken stoppingToken, int timeoutMs = 10000);
    Task<byte[]> GetByteArrayAsync(string requestUri);
  }

  public class BasicHttpService : IBasicHttpService
  {
    private readonly ILoggerAdapter<BasicHttpService> _logger;
    private readonly IJsonHelper _jsonHelper;
    private readonly HttpClient _httpClient;

    public BasicHttpService(
      ILoggerAdapter<BasicHttpService> logger,
      IJsonHelper jsonHelper)
    {
      // TODO: [TESTS] (HttpClientService.HttpClientService) Add tests
      _logger = logger;
      _jsonHelper = jsonHelper;

      var handler = new TimeoutHandler
      {
        DefaultTimeout = TimeSpan.FromSeconds(5),
        InnerHandler = new HttpClientHandler()
      };

      _httpClient = new HttpClient(handler)
      {
        Timeout = Timeout.InfiniteTimeSpan
      };
    }

    // Interface methods
    public async Task<HttpResponseMessage> GetUrlAsync(string url)
    {
      // TODO: [TESTS] (HttpClientService.GetUrlAsync) Add tests
      var request = new HttpRequestMessage(HttpMethod.Get, url);
      return await _httpClient.SendAsync(request);
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, int timeoutMs = 0)
    {
      // TODO: [TESTS] (HttpClientService.SendAsync) Add tests
      if (timeoutMs > 0)
        requestMessage.SetTimeout(TimeSpan.FromMilliseconds(timeoutMs));

      return await _httpClient.SendAsync(requestMessage);
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, CancellationToken stoppingToken, int timeoutMs = 10000)
    {
      // TODO: [TESTS] (BasicHttpService.SendAsync) Add tests
      if (timeoutMs > 0)
        requestMessage.SetTimeout(TimeSpan.FromMilliseconds(timeoutMs));

      return await _httpClient.SendAsync(requestMessage, stoppingToken);
    }

    public async Task<byte[]> GetByteArrayAsync(string requestUri)
    {
      // TODO: [TESTS] (BasicHttpService.GetByteArrayAsync) Add tests
      return await _httpClient.GetByteArrayAsync(requestUri);
    }
  }

  public static class HttpRequestMessageExtensions
  {
    private const string TimeoutPropertyKey = "RequestTimeout";

    public static TimeSpan? GetTimeout(this HttpRequestMessage request)
    {
      if (request == null) throw new ArgumentNullException(nameof(request));
      if (request.Properties.TryGetValue(TimeoutPropertyKey, out var value) && value is TimeSpan timeout)
        return timeout;
      return null;
    }

    public static void SetTimeout(this HttpRequestMessage request, TimeSpan? timeout)
    {
      if (request == null) throw new ArgumentNullException(nameof(request));
      request.Properties[TimeoutPropertyKey] = timeout;
    }
  }

  public class TimeoutHandler : DelegatingHandler
  {
    // https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
    public TimeSpan DefaultTimeout { get; set; } = TimeSpan.FromSeconds(100);

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      // ReSharper disable once ConvertToUsingDeclaration
      using (var cts = GetCancellationTokenSource(request, cancellationToken))
      {
        try
        {
          return await base.SendAsync(request, cts?.Token ?? cancellationToken);
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
          throw new TimeoutException();
        }
      }
    }

    private CancellationTokenSource GetCancellationTokenSource(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      var timeout = request.GetTimeout() ?? DefaultTimeout;

      if (timeout == Timeout.InfiniteTimeSpan)
      {
        // No need to create a CTS if there's no timeout
        return null;
      }

      var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
      cts.CancelAfter(timeout);
      return cts;
    }
  }
}
