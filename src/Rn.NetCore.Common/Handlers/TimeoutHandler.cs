using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Rn.NetCore.Common.Extensions;

namespace Rn.NetCore.Common.Handlers
{
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