using System;
using System.Text;

namespace Rn.NetCore.Common.Logging
{
  public static class ExceptionExtensions
  {
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
  }
}
