using Microsoft.AspNetCore.Mvc.Filters;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.WebCommon.Extensions;
using Rn.NetCore.WebCommon.Models;

namespace Rn.NetCore.WebCommon.Filters
{
  public class MetricResourceFilter : IResourceFilter
  {
    private readonly IDateTimeAbstraction _dateTime;

    public MetricResourceFilter(IDateTimeAbstraction dateTime)
    {
      _dateTime = dateTime;
    }

    // Interface methods
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
      // TODO: [TESTS] (MetricResourceFilter.OnResourceExecuting) Add tests
      context.HttpContext.Items[WebKeys.RequestContextKey] = new ApiMetricRequestContext(_dateTime.UtcNow);
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
      // TODO: [TESTS] (MetricResourceFilter.OnResourceExecuted) Add tests
      var requestMetricContext = context.HttpContext.GetApiRequestMetricContext();
      if (requestMetricContext == null)
        return;

      requestMetricContext.RequestEndTime = _dateTime.UtcNow;
    }
  }
}
