using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Extensions;
using Rn.NetCore.Common.Metrics;
using Rn.NetCore.WebCommon.Builders;
using Rn.NetCore.WebCommon.Models;

namespace Rn.NetCore.WebCommon.Filters
{
  public class MetricResourceFilter : IResourceFilter
  {
    private readonly IDateTimeAbstraction _dateTime;
    private readonly IMetricService _metrics;

    public MetricResourceFilter(IDateTimeAbstraction dateTime, IMetricService metrics)
    {
      _dateTime = dateTime;
      _metrics = metrics;
    }

    // Interface methods
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
      // TODO: [TESTS] (MetricResourceFilter.OnResourceExecuting) Add tests
      context.HttpContext.Items[WebKeys.RequestContextKey] = new ApiMetricRequestContext
      {
        RequestStartTime = _dateTime.UtcNow
      };
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
      // TODO: [TESTS] (MetricResourceFilter.OnResourceExecuted) Add tests
      if (!context.HttpContext.Items.ContainsKey(WebKeys.RequestContextKey))
        return;

      if (!(context.HttpContext.Items[WebKeys.RequestContextKey] is ApiMetricRequestContext proxyRequest))
        return;

      var httpRequest = context.HttpContext.Request;

      var builder = new ApiCallMetricBuilder()
        .ForTrackedRequest(proxyRequest, _dateTime.UtcNow)
        .WithRequestMethod(httpRequest.Method)
        .WithRequestContentType(WorkRequestContentType(context))
        .WithResponseCode(WorkResponseStatusCode(context))
        .WithResponseContentType(WorkResponseContentType(context))
        .WithRequestContentLength(httpRequest.ContentLength ?? 0)
        .WithResponseContentLength(WorkResponseContentLength(context));

      _metrics.SubmitPoint(builder.Build());
    }


    // Helper methods
    private static long WorkResponseContentLength(ResourceExecutedContext context)
    {
      // TODO: [TESTS] (MetricResourceFilter.WorkResponseContentLength) Add tests
      if (context.HttpContext.Response.ContentLength.HasValue)
        return context.HttpContext.Response.ContentLength.Value;

      var contentType = context.HttpContext.Response.ContentType;
      if (string.IsNullOrWhiteSpace(contentType))
        return 0;

      if (!contentType.LowerTrim().StartsWith("text/"))
        return 0;

      if (context.Result is OkObjectResult actionResult)
      {
        return actionResult.Value?.ToString()?.Length ?? 0;
      }

      return 0;
    }

    private static string WorkRequestContentType(ActionContext context)
    {
      // TODO: [TESTS] (MetricResourceFilter.WorkRequestContentType) Add tests
      var contentType = context.HttpContext.Request.ContentType;
      return string.IsNullOrWhiteSpace(contentType) ? "(none)" : contentType;
    }

    private static int WorkResponseStatusCode(ResourceExecutedContext context)
    {
      // TODO: [TESTS] (MetricResourceFilter.WorkResponseStatusCode) Add tests
      return context.Exception != null ? 500 : context.HttpContext.Response.StatusCode;
    }

    private static string WorkResponseContentType(ResourceExecutedContext context)
    {
      // TODO: [TESTS] (MetricResourceFilter.WorkResponseContentType) Add tests
      // ReSharper disable once ConvertIfStatementToReturnStatement
      if (context.Exception != null)
        return "text/html; charset=utf-8";

      return context.HttpContext.Response.ContentType;
    }
  }
}
