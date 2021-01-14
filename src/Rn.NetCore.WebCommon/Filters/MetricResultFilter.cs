using Microsoft.AspNetCore.Mvc.Filters;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.WebCommon.Models;

namespace Rn.NetCore.WebCommon.Filters
{
  public class MetricResultFilter : IResultFilter
  {
    private readonly IDateTimeAbstraction _dateTime;

    public MetricResultFilter(IDateTimeAbstraction dateTime)
    {
      _dateTime = dateTime;
    }

    public void OnResultExecuting(ResultExecutingContext context)
    {
      // TODO: [TESTS] (MetricResultFilter.OnResultExecuting) Add tests
      if (!context.HttpContext.Items.ContainsKey(WebKeys.RequestContextKey))
        return;

      if (!(context.HttpContext.Items[WebKeys.RequestContextKey] is ApiMetricRequestContext proxyRequest))
        return;

      proxyRequest.ResultsStartTime = _dateTime.UtcNow;
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
      // TODO: [TESTS] (MetricResultFilter.OnResultExecuted) Add tests
      if (!context.HttpContext.Items.ContainsKey(WebKeys.RequestContextKey))
        return;

      if (!(context.HttpContext.Items[WebKeys.RequestContextKey] is ApiMetricRequestContext proxyRequest))
        return;

      proxyRequest.ResultsEndTime = _dateTime.UtcNow;
    }
  }
}
