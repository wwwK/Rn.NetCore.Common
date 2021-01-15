using System;

namespace Rn.NetCore.WebCommon.Models
{
  public class ApiMetricRequestContext
  {
    public DateTime? RequestStartTime { get; set; }
    public DateTime? ActionStartTime { get; set; }
    public DateTime? ActionEndTime { get; set; }
    public DateTime? ResultsStartTime { get; set; }
    public DateTime? ResultsEndTime { get; set; }
    public string Controller { get; set; }
    public string Action { get; set; }
    public string RequestGuid { get; set; }
    public string ExceptionName { get; set; }

    public ApiMetricRequestContext()
    {
      // TODO: [TESTS] (ApiMetricRequestContext) Add tests
      RequestStartTime = null;
      ActionStartTime = null;
      ActionEndTime = null;
      ResultsStartTime = null;
      ResultsEndTime = null;
      Controller = string.Empty;
      Action = string.Empty;
      RequestGuid = string.Empty;
      ExceptionName = string.Empty;
    }

    public ApiMetricRequestContext(DateTime requestStartTime)
      : base()
    {
      // TODO: [TESTS] (ApiMetricRequestContext) Add tests
      RequestStartTime = requestStartTime;
    }
  }
}
