using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Rn.NetCore.Common.Extensions;
using Rn.NetCore.Common.Metrics.Builders;
using Rn.NetCore.Common.Metrics.Enums;
using Rn.NetCore.WebCommon.Models;

namespace Rn.NetCore.WebCommon.Builders
{
  public class ApiCallMetricBuilder
  {
    private readonly MetricBuilder _builder;

    private static class Tags
    {
      public const string Controller = "controller";
      public const string Action = "action";
      public const string RequestMethod = "request_method";
      public const string RequestContentType = "request_content_type";
      public const string ResponseCode = "response_code";
      public const string ResponseContentType = "response_content_type";
    }

    private static class Fields
    {
      public const string ActionTimeMs = "action_time";
      public const string ResultTimeMs = "result_time";
      public const string RequestContentLength = "request_content_length";
      public const string ResponseContentLength = "response_content_length";
    }

    // Builder methods
    public ApiCallMetricBuilder(string measurement = null)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.ApiCallMetricBuilder) Add tests
      _builder = new MetricBuilder(MetricSource.ApiCall, measurement)
        .WithTag(Tags.Controller, MetricPlaceholder.Unknown)
        .WithTag(Tags.Action, MetricPlaceholder.Unknown)
        .WithTag(Tags.RequestMethod, MetricPlaceholder.Unknown)
        .WithTag(Tags.ResponseCode, MetricPlaceholder.Unknown)
        .WithTag(Tags.RequestContentType, MetricPlaceholder.Unset)
        .WithTag(Tags.ResponseContentType, MetricPlaceholder.Unset)
        .WithField(Fields.RequestContentLength, (long)0)
        .WithField(Fields.ResponseContentLength, (long)0)
        .WithField(Fields.ActionTimeMs, (double)0)
        .WithField(Fields.ResultTimeMs, (double)0);
    }

    public ApiCallMetricBuilder(ResourceExecutedContext context, DateTime utcNow)
      : this()
    {
      // TODO: [TESTS] (ApiCallMetricBuilder) Add tests
      if (!(context.HttpContext.Items[WebKeys.RequestContextKey] is ApiMetricRequestContext proxyRequest))
        return;

      var exName = WorkExceptionName(proxyRequest);

      _builder
        // Tags
        .WithTag(Tags.Controller, proxyRequest.Controller, true)
        .WithTag(Tags.Action, proxyRequest.Action, true)
        .WithTag(Tags.RequestMethod, WorkRequestMethod(context), true)
        .WithTag(Tags.RequestContentType, WorkRequestContentType(context))
        .WithTag(Tags.ResponseCode, WorkResponseStatusCode(context))
        .WithTag(Tags.ResponseContentType, WorkResponseContentType(context))
        .WithTag(CoreMetricTag.ExceptionName, exName, true)
        .WithTag(CoreMetricTag.HasException, exName.Length > 0)
        // Fields
        .WithField(CoreMetricField.Value, WorkRequestRunTime(proxyRequest, utcNow))
        .WithField(Fields.ActionTimeMs, WorkActionRunTime(proxyRequest))
        .WithField(Fields.ResultTimeMs, WorkResultRunTime(proxyRequest))
        .WithField(Fields.RequestContentLength, context?.HttpContext?.Request?.ContentLength ?? 0)
        .WithField(Fields.ResponseContentLength, WorkResponseContentLength(context));
    }

    public MetricBuilder Build()
    {
      return _builder;
    }


    // Helper methods
    public static double WorkRequestRunTime(ApiMetricRequestContext request, DateTime utcNow)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WorkRequestRunTime) Add tests
      return request?.RequestStartTime == null
        ? 0
        : (utcNow - request.RequestStartTime.Value).Milliseconds;
    }

    public static double WorkActionRunTime(ApiMetricRequestContext request)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WorkActionRunTime) Add tests
      if (request?.ActionStartTime == null || !request.ActionEndTime.HasValue)
        return 0;

      var start = request.ActionStartTime.Value;
      var end = request.ActionEndTime.Value;

      return (end - start).TotalMilliseconds;
    }

    public static double WorkResultRunTime(ApiMetricRequestContext request)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WorkResultRunTime) Add tests
      if (request?.ResultsStartTime == null || !request.ResultsEndTime.HasValue)
        return 0;

      var start = request.ResultsStartTime.Value;
      var end = request.ResultsEndTime.Value;

      return (end - start).TotalMilliseconds;
    }

    private static string WorkResponseContentType(ResourceExecutedContext context)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WorkResponseContentType) Add tests
      return context.Exception != null
        ? "text/html; charset=utf-8"
        : context.HttpContext.Response.ContentType;
    }

    private static int WorkResponseStatusCode(ResourceExecutedContext context)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WorkResponseStatusCode) Add tests
      return context.Exception != null ? 500 : context.HttpContext.Response.StatusCode;
    }

    private static long WorkResponseContentLength(ResourceExecutedContext context)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WorkResponseContentLength) Add tests
      if (context.HttpContext.Response.ContentLength.HasValue)
        return context.HttpContext.Response.ContentLength.Value;

      var contentType = (context.HttpContext?.Response?.ContentType ?? "").LowerTrim();
      if (string.IsNullOrWhiteSpace(contentType))
        return 0;

      if (contentType.StartsWith("application/json"))
      {
        if (context.Result is ObjectResult bob)
        {
          return bob.Value?.ToString()?.Length ?? 0;
        }

        return 0;
      }

      if (!contentType.LowerTrim().StartsWith("text/"))
        return 0;

      if (context.Result is OkObjectResult actionResult)
      {
        return actionResult.Value?.ToString()?.Length ?? 0;
      }

      return 0;
    }

    private static string WorkRequestMethod(ActionContext context)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WorkRequestMethod) Add tests
      var requestMethod = context?.HttpContext?.Request?.Method ?? string.Empty;

      return string.IsNullOrWhiteSpace(requestMethod)
        ? MetricPlaceholder.Unknown
        : requestMethod.UpperTrim();
    }

    private static string WorkRequestContentType(ActionContext context)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WorkRequestContentType) Add tests
      return context?.HttpContext?.Request?.ContentType ?? MetricPlaceholder.None;
    }

    private static string WorkExceptionName(ApiMetricRequestContext request)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WorkExceptionName) Add tests
      return string.IsNullOrWhiteSpace(request.ExceptionName)
        ? string.Empty
        : request.ExceptionName;
    }
  }
}
