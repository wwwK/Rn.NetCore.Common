using Microsoft.AspNetCore.Mvc.Filters;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Metrics;
using Rn.NetCore.WebCommon.Builders;
using Rn.NetCore.WebCommon.Models;

namespace Rn.NetCore.WebCommon.Filters
{
  public class MetricResourceFilter : IResourceFilter
  {
    private readonly IDateTimeAbstraction _date;
    private readonly IMetricService _metrics;

    public MetricResourceFilter(
      IDateTimeAbstraction dateTime,
      IMetricService metrics)
    {
      _date = dateTime;
      _metrics = metrics;
    }

    // Interface methods
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
      // TODO: [TESTS] (MetricResourceFilter.OnResourceExecuting) Add tests
      context.HttpContext.Items[WebKeys.RequestContextKey] = new ApiMetricRequestContext(_date.UtcNow);
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
      // TODO: [TESTS] (MetricResourceFilter.OnResourceExecuted) Add tests
      if (!context.HttpContext.Items.ContainsKey(WebKeys.RequestContextKey))
        return;

      _metrics.SubmitPoint(new ApiCallMetricBuilder(context, _date.UtcNow).Build());
    }
  }
}
