using Microsoft.AspNetCore.Mvc.Filters;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.WebCommon.Extensions;

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
      context.HttpContext.SetAndGetApiRequestMetricContext(_dateTime.UtcNow)
        ?.WithResourceExecutingContext(context);
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
      // TODO: [TESTS] (MetricResourceFilter.OnResourceExecuted) Add tests
      context.HttpContext.GetApiRequestMetricContext()
        ?.WithResourceExecutedContext(context, _dateTime.UtcNow);
    }
  }
}
