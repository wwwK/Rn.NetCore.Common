using System;

namespace Rn.NetCore.Common.Extensions
{
  public static class DateExtensions
  {
    public static string AsWebUtcString(this DateTime date, bool replaceMinAndSecs = false)
    {
      // TODO: [TESTS] (DateExtensions.AsWebUtcString) Add tests

      var formattingMask = replaceMinAndSecs
        ? "yyyy'-'MM'-'dd'T'HH':00:00.000Z'"
        : "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'";

      return date.ToUniversalTime().ToString(formattingMask);
    }

    public static string AsUtcShortDate(this DateTime date)
    {
      // TODO: [TESTS] (DateExtensions.AsUtcShortDate) Add tests

      var uDate = date.ToUniversalTime();

      return $"{uDate.Year}-" +
             $"{uDate.Month.ToString("D").PadLeft(2, '0')}-" +
             $"{uDate.Day.ToString("D").PadLeft(2, '0')}";
    }

    public static string LogPrintDate(this DateTime? date)
    {
      // TODO: [TESTS] (DateExtensions.LogPrintDate) Add tests

      return !date.HasValue ? "(null)" : date.Value.ToString("s");
    }
  }
}
