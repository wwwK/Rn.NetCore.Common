using Microsoft.AspNetCore.Mvc.Filters;
using Rn.NetCore.WebCommon.Models;

namespace Rn.NetCore.WebCommon.Filters
{
  public class MetricExceptionFilter : IExceptionFilter
  {
    public void OnException(ExceptionContext context)
    {
      // TODO: [TESTS] (MetricExceptionFilter.OnException) Add tests
      if (!context.HttpContext.Items.ContainsKey(WebKeys.RequestContextKey))
        return;

      if (!(context.HttpContext.Items[WebKeys.RequestContextKey] is ApiMetricRequestContext proxyRequest))
        return;

      proxyRequest.ExceptionName = context.Exception.GetType().Name;
    }
  }
}
