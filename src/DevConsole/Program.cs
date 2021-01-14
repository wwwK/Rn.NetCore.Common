﻿using System;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Encryption;
using Rn.NetCore.Common.Helpers;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.Common.Metrics;
using Rn.NetCore.Common.Metrics.Builders;
using Rn.NetCore.Common.Metrics.Interfaces;
using Rn.NetCore.Common.Metrics.Outputs;
using Rn.NetCore.Metrics.Rabbit;

namespace DevConsole
{
  class Program
  {
    private static IServiceProvider _serviceProvider;
    private static ILoggerAdapter<Program> _logger;

    static void Main(string[] args)
    {
      ConfigureDI();

      var builder = new ServiceMetricBuilder("Service", "Method")
        .WithCategory("Category", "SubCategory")
        .WithCustomTag1(true)
        .WithCustomInt1(10);

      IMetricService metrics;
      using (builder.WithTiming())
      {
        metrics = _serviceProvider.GetRequiredService<IMetricService>();
      }

      for (var i = 0; i < 200; i++)
      {
        metrics.SubmitPoint(builder.Build());
      }

      _logger.Info("All Done!");
    }


    // DI related methods
    private static void ConfigureDI()
    {
      var services = new ServiceCollection();

      var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .Build();

      ConfigureDI_Configuration(services, config);
      ConfigureDI_Core(services, config);
      ConfigureDI_Metrics(services);

      _serviceProvider = services.BuildServiceProvider();
      _logger = _serviceProvider.GetService<ILoggerAdapter<Program>>();
    }

    private static void ConfigureDI_Core(IServiceCollection services, IConfiguration config)
    {
      services
        .AddSingleton(config)
        .AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>))
        .AddSingleton<IEncryptionService, EncryptionService>()
        .AddSingleton<IEncryptionUtils, EncryptionUtils>()
        .AddSingleton<IDateTimeAbstraction, DateTimeAbstraction>()
        .AddSingleton<IJsonHelper, JsonHelper>()
        .AddSingleton<IEnvironmentAbstraction, EnvironmentAbstraction>()
        .AddSingleton<IDirectoryAbstraction, DirectoryAbstraction>()
        .AddSingleton<IFileAbstraction, FileAbstraction>()
        .AddSingleton<IPathAbstraction, PathAbstraction>()
        .AddLogging(loggingBuilder =>
        {
          // configure Logging with NLog
          loggingBuilder.ClearProviders();
          loggingBuilder.SetMinimumLevel(LogLevel.Trace);
          loggingBuilder.AddNLog(config);
        });
    }

    private static void ConfigureDI_Configuration(IServiceCollection services, IConfiguration config)
    {
      // Complete
    }

    private static void ConfigureDI_Metrics(IServiceCollection services)
    {
      services
        .AddSingleton<IMetricService, MetricService>()
        .AddSingleton<IMetricOutput, RabbitMetricOutput>()
        .AddSingleton<IRabbitConnection, RabbitConnection>()
        .AddSingleton<IRabbitFactory, RabbitFactory>()
        .AddSingleton<IMetricOutput, CsvMetricOutput>();
    }
  }
}
