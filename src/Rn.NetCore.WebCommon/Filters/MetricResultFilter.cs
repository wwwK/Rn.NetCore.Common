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
      var requestMetricContext = context.HttpContext.GetApiRequestMetricContext();
      if(requestMetricContext == null)
        return;

      requestMetricContext.ResultsStartTime = _dateTime.UtcNow;
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
      // TODO: [TESTS] (MetricResultFilter.OnResultExecuted) Add tests
      var requestMetricContext = context.HttpContext.GetApiRequestMetricContext();
      if(requestMetricContext == null)
        return;

      requestMetricContext.ResultsEndTime = _dateTime.UtcNow;
    }
  }
}
