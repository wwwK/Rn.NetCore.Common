using Microsoft.AspNetCore.Mvc.Filters;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.WebCommon.Extensions;

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
      context.HttpContext.GetApiRequestMetricContext()
        ?.WithResultExecutingContext(context, _dateTime.UtcNow);
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
      // TODO: [TESTS] (MetricResultFilter.OnResultExecuted) Add tests
      context.HttpContext.GetApiRequestMetricContext()
        ?.WithResultExecutedContext(context, _dateTime.UtcNow);
    }
  }
}
