using Rn.NetCore.Common.Logging;
using Rn.NetCore.Common.Metrics;

namespace Rn.NetCore.Common.Services
{
  public abstract class BaseService<TService>
  {
    public ILoggerAdapter<TService> Logger { get; }
    public IMetricService Metrics { get; }
    public string ServiceName { get; }


    // Constructor
    protected BaseService(
      ILoggerAdapter<TService> logger,
      IMetricService metrics,
      string serviceName)
    {
      Logger = logger;
      Metrics = metrics;
      ServiceName = serviceName;
    }


    // Public methods
    //public async Task<T> TimeServiceCall<T>(string methodName, Func<Task<T>> predicate)
    //{
    //  // TODO: [TESTS] (BaseService) Add tests

    //  Logger.LogTrace(
    //    "Running: {service}.{method}()",
    //    ServiceName,
    //    methodName
    //  );

    //  using (var metric = CreateTimingMetric(methodName))
    //  {
    //    try
    //    {
    //      var response = await predicate.Invoke();
    //      metric.SetTag("success", "true");
    //      return response;
    //    }
    //    catch (Exception ex)
    //    {
    //      Logger.LogError(ex,
    //        "Error running service call. {trace}",
    //        ex.HumanStackTrace()
    //      );

    //      metric.SetTag("exception", ex.GetType().Name, false);
    //      return default;
    //    }
    //  }
    //}

    //public T TimeServiceCall<T>(string methodName, Func<T> predicate)
    //{
    //  // TODO: [TESTS] (BaseService) Add tests

    //  Logger.LogTrace(
    //    "Running: {service}.{method}()",
    //    ServiceName,
    //    methodName
    //  );

    //  using (var metric = CreateTimingMetric(methodName))
    //  {
    //    try
    //    {
    //      var response = predicate.Invoke();
    //      metric.SetTag("success", "true");
    //      return response;
    //    }
    //    catch (Exception ex)
    //    {
    //      Logger.LogError(ex,
    //        "Error running service call. {trace}",
    //        ex.HumanStackTrace()
    //      );

    //      metric.SetTag("exception", ex.GetType().Name, false);

    //      // TODO: [REVISE] (BaseService) Revise impact of this change
    //      throw;
    //    }
    //  }
    //}


    // Private methods
    //private ICodeTimer CreateTimingMetric(string methodName)
    //{
    //  // TODO: [TESTS] (BaseService) Add tests
    //  // TODO: [LOGGING] (BaseService) Add logging

    //  // Metrics are not enabled, return "NullCodeTimer"
    //  if (!Metrics.MetricsEnabled)
    //    return new NullCodeTimer();

    //  // Generate expected tags
    //  var tags = new Dictionary<string, string>
    //  {
    //    ["service"] = ServiceName.Trim(),
    //    ["method"] = methodName.Trim(),
    //    ["exception"] = "",
    //    ["success"] = "false"
    //  };

    //  // Generate expected fields
    //  var fields = new Dictionary<string, object>
    //  {
    //    ["value"] = 0,
    //    ["call_count"] = 1
    //  };

    //  // Return the generated CodeTimer
    //  return Metrics.LogTiming("service_call", tags, fields);
    //}
  }
}
