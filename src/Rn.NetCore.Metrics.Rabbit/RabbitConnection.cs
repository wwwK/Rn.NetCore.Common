using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.Common.Metrics.Models;
using Rn.NetCore.Metrics.Rabbit.Config;

namespace Rn.NetCore.Metrics.Rabbit
{
  public interface IRabbitConnection
  {
    void Configure(RabbitOutputConfig config);
    Task SubmitPoint(LineProtocolPoint point);
    Task SubmitPoints(List<LineProtocolPoint> points);
  }

  public class RabbitConnection : IRabbitConnection
  {
    private readonly ILoggerAdapter<RabbitConnection> _logger;
    private readonly IRabbitFactory _rabbitFactory;

    private RabbitOutputConfig _config;
    private IConnectionFactory _connectionFactory;
    private IConnection _connection;
    private IModel _channel;

    public RabbitConnection(
      ILoggerAdapter<RabbitConnection> logger,
      IRabbitFactory rabbitFactory)
    {
      // TODO: [TESTS] (RabbitConnection) Add tests
      _logger = logger;
      _rabbitFactory = rabbitFactory;
      _config = new RabbitOutputConfig();
    }


    // Interface methods
    public void Configure(RabbitOutputConfig config)
    {
      // TODO: [TESTS] (RabbitConnection.Configure) Add tests
      _config = config;

      // Ensure that we are enabled
      if (!_config.Enabled)
        return;

      CreateConnectionFactory();
      RecreateConnection();
    }

    public async Task SubmitPoint(LineProtocolPoint point)
    {
      // TODO: [TESTS] (RabbitConnection.SubmitPoint) Add tests
      await SubmitPoints(new List<LineProtocolPoint> { point });
    }

    public async Task SubmitPoints(List<LineProtocolPoint> points)
    {
      // TODO: [TESTS] (RabbitConnection.SubmitPoints) Add tests
      if (!CanSubmitPoints())
        return;

      PublishPoints(points);
      await Task.CompletedTask;
    }


    // Connection related methods
    private void TearDownConnection()
    {
      // TODO: [TESTS] (RabbitConnection.TearDownConnection) Add tests
      if (_channel == null && _connection == null)
        return;

      _logger.Trace("Tearing down RabbitMQ connection");

      try
      {
        _channel?.Close();
        _connection?.Close();

        _channel?.Dispose();
        _connection?.Dispose();

        _channel = null;
        _connection = null;
      }
      catch (Exception ex)
      {
        _logger.LogUnexpectedException(ex);
      }
    }

    private void CreateConnectionFactory()
    {
      // TODO: [TESTS] (RabbitConnection.CreateConnectionFactory) Add tests
      _logger.Trace("Creating new connection factory");
      _connectionFactory = _rabbitFactory.CreateConnectionFactory(_config);
    }

    private void RecreateConnection()
    {
      // TODO: [TESTS] (RabbitConnection.RecreateConnection) Add tests
      TearDownConnection();

      try
      {
        _connection = _connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
      }
      catch (Exception ex)
      {
        _logger.LogUnexpectedException(ex);
      }
    }

    private bool CurrentlyConnected()
    {
      // TODO: [TESTS] (RabbitConnection.CurrentlyConnected) Add tests
      if (!(_connection?.IsOpen ?? false))
        return false;

      return _channel?.IsOpen ?? false;
    }


    // Point submitting related methods
    private bool CanSubmitPoints()
    {
      // TODO: [TESTS] (RabbitConnection.CanSubmitPoints) Add tests
      if (_config.Enabled)
        return false;

      if (CurrentlyConnected())
        return true;

      RecreateConnection();
      return CurrentlyConnected();
    }

    private static string GeneratePayload(IEnumerable<LineProtocolPoint> points)
    {
      // TODO: [TESTS] (RabbitConnection.GeneratePayload) Add tests
      using var sw = new StringWriter();
      foreach (var point in points)
      {
        point.Format(sw);
        sw.Write("\n");
      }

      return sw.ToString().Trim();
    }

    private void PublishPoints(IReadOnlyCollection<LineProtocolPoint> points)
    {
      // TODO: [TESTS] (RabbitConnection.PublishPoints) Add tests
      if (points.Count == 0)
        return;

      try
      {
        _channel.BasicPublish(
          exchange: _config.Exchange,
          routingKey: _config.RoutingKey,
          basicProperties: null,
          body: Encoding.UTF8.GetBytes(GeneratePayload(points))
        );
      }
      catch (Exception ex)
      {
        _logger.LogUnexpectedException(ex);
      }
    }
  }
}
