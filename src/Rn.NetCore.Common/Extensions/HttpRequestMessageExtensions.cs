﻿using System;
using System.Net.Http;

namespace Rn.NetCore.Common.Extensions
{
  public static class HttpRequestMessageExtensions
  {
    private const string TimeoutPropertyKey = "RequestTimeout";

    public static TimeSpan? GetTimeout(this HttpRequestMessage request)
    {
      if (request == null) throw new ArgumentNullException(nameof(request));
      if (request.Properties.TryGetValue(TimeoutPropertyKey, out var value) && value is TimeSpan timeout)
        return timeout;
      return null;
    }

    public static void SetTimeout(this HttpRequestMessage request, TimeSpan? timeout)
    {
      if (request == null) throw new ArgumentNullException(nameof(request));
      request.Properties[TimeoutPropertyKey] = timeout;
    }
  }
}