using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Extensions;
using Rn.NetCore.WebCommon.Models;

namespace Rn.NetCore.WebCommon.Filters
{
  public class MetricActionFilter : IActionFilter
  {
    private readonly IDateTimeAbstraction _dateTime;

    public MetricActionFilter(IDateTimeAbstraction dateTime)
    {
      _dateTime = dateTime;
    }

    // Interface methods
    public void OnActionExecuting(ActionExecutingContext context)
    {
      // TODO: [TESTS] (MetricActionFilter.OnActionExecuting) Add tests
      if (!context.HttpContext.Items.ContainsKey(WebKeys.RequestContextKey))
        return;

      if (!(context.HttpContext.Items[WebKeys.RequestContextKey] is ApiMetricRequestContext proxyRequest))
        return;

      proxyRequest.Controller = GetControllerName(context);
      proxyRequest.Action = GetActionName(context);
      proxyRequest.ActionStartTime = _dateTime.UtcNow;
      proxyRequest.RequestGuid = Guid.NewGuid().ToString("D").UpperTrim();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
      // TODO: [TESTS] (MetricActionFilter.OnActionExecuted) Add tests
      if (!context.HttpContext.Items.ContainsKey(WebKeys.RequestContextKey))
        return;

      if (!(context.HttpContext.Items[WebKeys.RequestContextKey] is ApiMetricRequestContext proxyRequest))
        return;

      proxyRequest.ActionEndTime = _dateTime.UtcNow;
    }


    // Internal methods
    private static string GetControllerName(ActionExecutingContext context)
    {
      // TODO: [TESTS] (MetricActionFilter.GetControllerName) Add tests
      var controllerName = context.Controller?.GetType().Name;

      if (string.IsNullOrWhiteSpace(controllerName))
      {
        if (context.RouteData.Values.ContainsKey("controller"))
        {
          controllerName = context.RouteData.Values["controller"].ToString();
        }
      }

      if (string.IsNullOrWhiteSpace(controllerName))
        controllerName = string.Empty;

      return controllerName;
    }

    private static string GetActionName(ActionContext context)
    {
      // TODO: [TESTS] (MetricActionFilter.GetActionName) Add tests
      if (context == null || !context.RouteData.Values.ContainsKey("action"))
        return string.Empty;

      return context.RouteData.Values["action"].ToString();
    }
  }
}
