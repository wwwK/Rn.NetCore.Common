using System;
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

    // Constructor
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


    // Builders
    public ApiCallMetricBuilder ForTrackedRequest(ApiMetricRequestContext request, DateTime utcNow)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.ForTrackedRequest) Add tests

      _builder
        .WithTag(Tags.Controller, request.Controller, true)
        .WithTag(Tags.Action, request.Action, true)
        .WithTag(CoreMetricTag.ExceptionName, request.ExceptionName, true)
        .WithTag(CoreMetricTag.HasException, request.ExceptionName.Length > 0)
        .WithField(CoreMetricField.Value, WorkRequestRunTime(request, utcNow))
        .WithField(Fields.ActionTimeMs, WorkActionRunTime(request))
        .WithField(Fields.ResultTimeMs, WorkResultRunTime(request));

      return this;
    }

    public ApiCallMetricBuilder WithRequestMethod(string method)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithRequestMethod) Add tests
      _builder.WithTag(Tags.RequestMethod, method.UpperTrim(), true);
      return this;
    }

    public ApiCallMetricBuilder WithRequestContentType(string contentType)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithRequestContentType) Add tests
      _builder.WithTag(Tags.RequestContentType, contentType);
      return this;
    }

    public ApiCallMetricBuilder WithResponseCode(int code)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithResponseCode) Add tests
      _builder.WithTag(Tags.ResponseCode, code);
      return this;
    }

    public ApiCallMetricBuilder WithResponseContentType(string contentType)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithResponseContentType) Add tests
      _builder.WithTag(Tags.ResponseContentType, contentType);
      return this;
    }

    public ApiCallMetricBuilder WithRequestContentLength(long length)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithRequestContentLength) Add tests
      _builder.WithField(Fields.RequestContentLength, length);
      return this;
    }

    public ApiCallMetricBuilder WithResponseContentLength(long length)
    {
      // TODO: [TESTS] (ApiCallMetricBuilder.WithResponseContentLength) Add tests
      _builder.WithField(Fields.ResponseContentLength, length);
      return this;
    }


    // Build()
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
  }
}
