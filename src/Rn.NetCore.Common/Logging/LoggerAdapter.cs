using System;
using Microsoft.Extensions.Logging;

namespace Rn.NetCore.Common.Logging
{
  public interface ILoggerAdapter<T>
  {
    void Trace(string message, params object[] args);
    void Debug(string message, params object[] args);
    void Info(string message, params object[] args);
    void Error(string message, params object[] args);
    void Error(Exception ex, string message, params object[] args);
  }

  public class LoggerAdapter<T> : ILoggerAdapter<T>
  {
    private readonly ILogger<T> _logger;

    public LoggerAdapter(ILogger<T> logger)
    {
      _logger = logger;
    }

    public void Trace(string message, params object[] args)
    {
      _logger.LogTrace(message, args);
    }

    public void Debug(string message, params object[] args)
    {
      _logger.LogDebug(message, args);
    }

    public void Info(string message, params object[] args)
    {
      _logger.LogInformation(message, args);
    }

    public void Error(string message, params object[] args)
    {
      _logger.LogError(message, args);
    }

    public void Error(Exception ex, string message, params object[] args)
    {
      _logger.LogError(ex, message, args);
    }
  }
}
