using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.Common.Metrics;
using Rn.NetCore.WebCommon.Builders;

namespace Rn.NetCore.WebCommon.Middleware
{
  // https://exceptionnotfound.net/using-middleware-to-log-requests-and-responses-in-asp-net-core/
  public class RequestResponseLoggingMiddleware
  {
    private readonly ILoggerAdapter<RequestResponseLoggingMiddleware> _logger;
    private readonly IMetricService _metrics;
    private readonly RequestDelegate _next;

    public RequestResponseLoggingMiddleware(
      ILoggerAdapter<RequestResponseLoggingMiddleware> logger,
      IMetricService metrics,
      RequestDelegate next)
    {
      _logger = logger;
      _metrics = metrics;
      _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      // Copy a pointer to the original response body stream
      var originalBodyStream = context.Response.Body;

      using (var responseBody = new MemoryStream())
      {
        //...and use that for the temporary response body
        context.Response.Body = responseBody;

        //Continue down the Middleware pipeline, eventually returning to this class
        await _next(context);

        await LogApiResponseMetric(context);
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
        await responseBody.CopyToAsync(originalBodyStream);
      }
    }

    private async Task LogApiResponseMetric(HttpContext httpContext)
    {
      // TODO: [TESTS] (RequestResponseLoggingMiddleware.LogApiResponseMetric) Add tests
      try
      {
        await _metrics.SubmitPointAsync(new ApiCallMetricBuilder(httpContext).Build());
      }
      catch (Exception ex)
      {
        _logger.LogUnexpectedException(ex);
      }
    }
  }
}
