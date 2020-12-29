using System;
using System.Diagnostics;
using System.Text;

namespace Rn.NetCore.Common.Logging
{
  public static class LoggerExtensions
  {
    public static void LogUnexpectedException<T>(this ILoggerAdapter<T> logger, Exception ex)
    {
      logger.Error("An unexpected exception of type {exType} was thrown in {method}. {exMessage}. | {exStack}",
        ex.GetType().Name,
        GetFullMethodName(2),
        ex.Message,
        ex.HumanStackTrace()
      );
    }

    public static string HumanStackTrace(this Exception ex)
    {
      // TODO: [TESTS] (ExceptionExtensions.HumanStackTrace) Add tests

      var sb = new StringBuilder();
      WalkException(sb, ex, 1);
      return sb.ToString();
    }


    // Internal methods
    private static void WalkException(StringBuilder sb, Exception ex, int level)
    {
      // TODO: [TESTS] (ExceptionExtensions.WalkException) Add tests

      sb.Append(level == 1 ? "" : "    ")
        .Append(level)
        .Append($" ({ex.GetType().FullName}) ")
        .Append(ex.Message)
        .AppendLine();

      if (ex.InnerException != null)
      {
        WalkException(sb, ex.InnerException, level + 1);
      }
    }

    private static string GetFullMethodName(int frameIndex)
    {
      try
      {
        var methodBase = new StackTrace().GetFrame(frameIndex)?.GetMethod();
        var methodName = methodBase.Name;
        var className = methodBase.ReflectedType?.FullName;
        return string.IsNullOrWhiteSpace(className) ? methodName : $"{className}.{methodName}";
      }
      catch
      {
        // Swallow
        return "unknown";
      }
    }
  }
}
