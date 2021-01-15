using Microsoft.AspNetCore.Mvc.Filters;
using Rn.NetCore.WebCommon.Extensions;

namespace Rn.NetCore.WebCommon.Filters
{
  public class MetricExceptionFilter : IExceptionFilter
  {
    public void OnException(ExceptionContext context)
    {
      // TODO: [TESTS] (MetricExceptionFilter.OnException) Add tests
      var requestMetricContext = context.HttpContext.GetApiRequestMetricContext();
      if(requestMetricContext == null)
        return;

      requestMetricContext.ExceptionName = context.Exception.GetType().Name;
    }
  }
}
