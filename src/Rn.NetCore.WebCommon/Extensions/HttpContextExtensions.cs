using Microsoft.AspNetCore.Http;
using Rn.NetCore.WebCommon.Models;

namespace Rn.NetCore.WebCommon.Extensions
{
  public static class HttpContextExtensions
  {
    public static bool HasApiRequestMetricContext(this HttpContext context)
    {
      // TODO: [TESTS] (HttpContextExtensions.HasApiRequestMetricContext) Add tests
      return context?.Items?.ContainsKey(WebKeys.RequestContextKey) ?? false;
    }

    public static ApiMetricRequestContext GetApiRequestMetricContext(this HttpContext context)
    {
      // TODO: [TESTS] (HttpContextExtensions.GetApiRequestMetricContext) Add tests
      if (!(context?.Items?.ContainsKey(WebKeys.RequestContextKey) ?? false))
        return null;

      if (!(context.Items[WebKeys.RequestContextKey] is ApiMetricRequestContext proxyRequest))
        return null;

      return proxyRequest;
    }
  }
}
