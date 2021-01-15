using Microsoft.AspNetCore.Http;
using Rn.NetCore.Common.Extensions;
using Rn.NetCore.Common.Metrics.Builders;
using Rn.NetCore.Common.Metrics.Enums;
using Rn.NetCore.WebCommon.Extensions;
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

    // Constructors
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

    public ApiCallMetricBuilder(HttpContext httpContext)
      : this()
    {
      // TODO: [TESTS] (ApiCallMetricBuilder) Add tests
      WithHttpContext(httpContext);

      var requestMetricContext = httpContext.GetApiRequestMetricContext();
      if (requestMetricContext == null)
        return;

      WithApiMetricRequestContext(requestMetricContext);
    }


    // Builder methods
    public ApiCallMetricBuilder WithApiMetricRequestContext(ApiMetricRequestContext context)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithApiMetricRequestContext) Add tests
      var exName = WorkExceptionName(context);

      _builder
        .WithTag(CoreMetricTag.ExceptionName, exName, true)
        .WithTag(CoreMetricTag.HasException, exName.Length > 0)
        .WithTag(Tags.Controller, context.Controller, true)
        .WithTag(Tags.Action, context.Action, true)
        .WithField(CoreMetricField.Value, WorkRequestRunTime(context))
        .WithField(Fields.ActionTimeMs, WorkActionRunTime(context))
        .WithField(Fields.ResultTimeMs, WorkResultRunTime(context));

      return this;
    }

    public ApiCallMetricBuilder WithHttpContext(HttpContext httpContext)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithHttpContext) Add tests
      _builder
        // Request related
        .WithTag(Tags.RequestMethod, GetRequestMethod(httpContext), true)
        .WithTag(Tags.RequestContentType, GetRequestContentType(httpContext))
        .WithField(Fields.RequestContentLength, GetRequestContentLength(httpContext))
        // Response Related
        .WithTag(Tags.ResponseCode, GetResponseStatusCode(httpContext))
        .WithTag(Tags.ResponseContentType, GetResponseContentType(httpContext))
        .WithField(Fields.ResponseContentLength, GetResponseContentLength(httpContext));

      return this;
    }

    public MetricBuilder Build()
    {
      return _builder;
    }


    // Helper methods
    public static double WorkRequestRunTime(ApiMetricRequestContext request)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WorkRequestRunTime) Add tests
      if (!request.RequestStartTime.HasValue || !request.RequestEndTime.HasValue)
        return 0;

      if (request.RequestStartTime.Value > request.RequestEndTime.Value)
        return 0;

      return (request.RequestEndTime.Value - request.RequestStartTime.Value).TotalMilliseconds;
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

    private static string GetResponseContentType(HttpContext httpContext)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.GetResponseContentType) Add tests
      return httpContext?.Response?.ContentType ?? MetricPlaceholder.Unset;
    }

    private static int GetResponseStatusCode(HttpContext httpContext)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.GetResponseStatusCode) Add tests
      return httpContext?.Response?.StatusCode ?? 0;
    }

    private static long GetResponseContentLength(HttpContext httpContext)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.GetResponseContentLength) Add tests
      var contentLength = httpContext?.Response?.ContentLength ?? 0;
      if (contentLength > 0)
        return contentLength;

      var bodyLength = httpContext?.Response?.Body?.Length ?? 0;
      if (bodyLength > 0)
        return bodyLength;

      return 0;
    }

    private static string GetRequestMethod(HttpContext httpContext)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.GetRequestMethod) Add tests
      var requestMethod = httpContext?.Request?.Method ?? string.Empty;

      return string.IsNullOrWhiteSpace(requestMethod)
        ? MetricPlaceholder.Unknown
        : requestMethod.UpperTrim();
    }

    private static string GetRequestContentType(HttpContext httpContext)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WorkRequestContentType) Add tests
      return httpContext?.Request?.ContentType ?? MetricPlaceholder.None;
    }

    private static long GetRequestContentLength(HttpContext httpContext)
    {
      return httpContext?.Request?.ContentLength ?? 0;
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
