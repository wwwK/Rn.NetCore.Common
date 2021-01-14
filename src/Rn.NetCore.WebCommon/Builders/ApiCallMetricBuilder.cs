using System;
using Rn.NetCore.Common.Metrics.Builders;
using Rn.NetCore.WebCommon.Models;

namespace Rn.NetCore.WebCommon.Builders
{
  public class ApiCallMetricBuilder
  {
    // TODO: [RENAME] (ApiCallMetricBuilder) RENAME
    private readonly MetricBuilder _builder;

    // Constructor
    public ApiCallMetricBuilder(string measurement = null)
    {
      // if (string.IsNullOrWhiteSpace(measurement))
      //   measurement = $"resolve:{MetricSource.ApiCall:G}";
      //
      // _builder = new MetricPointBuilder(MetricSource.ApiCall, measurement)
      //   .WithTag("controller", string.Empty)
      //   .WithTag("action", string.Empty)
      //   .WithTag("ex", string.Empty)
      //   .WithTag("request_method", string.Empty)
      //   .WithTag("request_content_type", string.Empty)
      //   .WithTag("response_code", 0)
      //   .WithTag("response_content_type", string.Empty)
      //   .WithTag("has_exception", false)
      //   .WithField(CoreMetricField.Value, (long)0)
      //   .WithField(CoreMetricField.CallCount, 1)
      //   .WithField("action_time_ms", (double)0)
      //   .WithField("result_time_ms", (double)0)
      //   .WithField("request_content_length", (long)0)
      //   .WithField("response_content_length", (long)0);
    }


    // Builders
    public ApiCallMetricBuilder ForTrackedRequest(ApiMetricRequestContext request, DateTime utcNow)
    {
      // _builder
      //   .WithTag("controller", request.Controller, true)
      //   .WithTag("action", request.Action, true)
      //   .WithTag("ex", request.ExceptionName, true)
      //   .WithTag("has_exception", request.ExceptionName.Length > 0)
      //   .WithField(CoreMetricField.Value, WorkRequestRunTime(request, utcNow))
      //   .WithField("action_time_ms", WorkActionRunTime(request))
      //   .WithField("result_time_ms", WorkResultRunTime(request));

      return this;
    }

    public ApiCallMetricBuilder WithRequestMethod(string method)
    {
      //_builder.WithTag("request_method", method.TrimAndUpper(), true);
      return this;
    }

    public ApiCallMetricBuilder WithRequestContentType(string contentType)
    {
      //_builder.WithTag("request_content_type", contentType);
      return this;
    }

    public ApiCallMetricBuilder WithResponseCode(int code)
    {
      //_builder.WithTag("response_code", code);
      return this;
    }

    public ApiCallMetricBuilder WithResponseContentType(string contentType)
    {
      //_builder.WithTag("response_content_type", contentType);
      return this;
    }

    public ApiCallMetricBuilder WithRequestContentLength(long length)
    {
      //_builder.WithField("request_content_length", length);
      return this;
    }

    public ApiCallMetricBuilder WithResponseContentLength(long length)
    {
      //_builder.WithField("response_content_length", length);
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
      return request?.RequestStartTime == null
        ? 0
        : (utcNow - request.RequestStartTime.Value).Milliseconds;
    }

    public static double WorkActionRunTime(ApiMetricRequestContext request)
    {
      if (request?.ActionStartTime == null || !request.ActionEndTime.HasValue)
        return 0;

      var start = request.ActionStartTime.Value;
      var end = request.ActionEndTime.Value;

      return (end - start).TotalMilliseconds;
    }

    public static double WorkResultRunTime(ApiMetricRequestContext request)
    {
      if (request?.ResultsStartTime == null || !request.ResultsEndTime.HasValue)
        return 0;

      var start = request.ResultsStartTime.Value;
      var end = request.ResultsEndTime.Value;

      return (end - start).TotalMilliseconds;
    }
  }
}
