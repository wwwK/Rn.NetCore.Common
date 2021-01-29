using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.Extensions.Configuration;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Extensions;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.Common.Metrics.Configuration;
using Rn.NetCore.Common.Metrics.Interfaces;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.Common.Metrics.Outputs
{
  public class CsvMetricOutput : IMetricOutput
  {
    public bool Enabled { get; private set; }
    public string Name { get; }
    public const string ConfigKey = "RnCore:Metrics:CsvFile";

    private readonly ILoggerAdapter<CsvMetricOutput> _logger;
    private readonly IDirectoryAbstraction _directory;
    private readonly IEnvironmentAbstraction _environment;
    private readonly IDateTimeAbstraction _dateTime;
    private readonly IPathAbstraction _path;
    private readonly IFileAbstraction _file;
    private readonly CsvMetricOutputConfig _config;
    private string _csvFileMask;

    public CsvMetricOutput(
      ILoggerAdapter<CsvMetricOutput> logger,
      IDirectoryAbstraction directory,
      IEnvironmentAbstraction environment,
      IDateTimeAbstraction dateTime,
      IPathAbstraction path,
      IFileAbstraction file,
      IConfiguration configuration)
    {
      // TODO: [TESTS] (CsvMetricOutput) Add tests
      _logger = logger;
      _directory = directory;
      _environment = environment;
      _dateTime = dateTime;
      _path = path;
      _file = file;
      _config = BindConfiguration(configuration);

      Name = nameof(CsvMetricOutput);
      Enabled = _config.Enabled;

      ProcessConfig();
    }


    // Interface methods
    public async Task SubmitPoint(LineProtocolPoint point)
    {
      await SubmitPoints(new List<LineProtocolPoint> {point});
    }

    public async Task SubmitPoints(List<LineProtocolPoint> points)
    {
      await WritePoints(points);
    }


    // Output methods
    private async Task WritePoints(IEnumerable<LineProtocolPoint> points)
    {
      // TODO: [TESTS] (CsvMetricOutput.WritePoints) Add tests
      var filePath = GenerateCsvFilePath();
      var fileDirectory = _path.GetDirectoryName(filePath);

      if (!_directory.Exists(fileDirectory))
        _directory.CreateDirectory(fileDirectory);

      var fileExists = _file.Exists(filePath);

      await using var writer = new StreamWriter(filePath, true, Encoding.UTF8);
      await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
      csv.Configuration.RegisterClassMap<LineProtocolPointMap>();
      csv.Configuration.ShouldQuote = (s, context) => true;

      if (!fileExists)
      {
        csv.WriteHeader<LineProtocolPoint>();
        await csv.NextRecordAsync();
      }

      foreach (var point in points)
      {
        csv.WriteRecord(point);
        await csv.NextRecordAsync();
      }
    }


    // Configuration methods
    private CsvMetricOutputConfig BindConfiguration(IConfiguration configuration)
    {
      // TODO: [TESTS] (CsvMetricOutput.BindConfiguration) Add tests
      var boundConfig = new CsvMetricOutputConfig();
      var section = configuration.GetSection(ConfigKey);
      
      if (!section.Exists())
      {
        _logger.Warning("Unable to find config '{s}' - using defaults", ConfigKey);
        return boundConfig;
      }

      section.Bind(boundConfig);
      return boundConfig;
    }

    private string GenerateCsvFilePath()
    {
      // TODO: [TESTS] (CsvMetricOutput.GenerateCsvFilePath) Add tests
      var now = _dateTime.Now;
      var min = now.Minute.ToString("D").PadLeft(2, '0');

      return _csvFileMask
        .Replace("{yyyy}", now.Year.ToString("D"))
        .Replace("{mm}", now.Month.ToString("D").PadLeft(2, '0'))
        .Replace("{dd}", now.Day.ToString("D").PadLeft(2, '0'))
        .Replace("{hh}", now.Hour.ToString("D").PadLeft(2, '0'))
        .Replace("{mod-min}", min.Substring(0, 1))
        .Replace("{MM}", min);
    }

    private void ProcessConfig()
    {
      // TODO: [TESTS] (CsvMetricOutput.ProcessConfig) Add tests
      var rootDir = _environment.CurrentDirectory.AppendIfMissing("\\");
      
      _config.OutputDir = _config.OutputDir
        .Replace("./", rootDir)
        .Replace(".\\", rootDir)
        .Replace("/", "\\")
        .AppendIfMissing("\\");

      _logger.Debug("Output directory set to: {path}", _config.OutputDir);

      if (!_directory.Exists(_config.OutputDir))
        _directory.CreateDirectory(_config.OutputDir);

      if (!_directory.Exists(_config.OutputDir))
      {
        _logger.Error("Unable to create output directory ({dir}) - disabling output",
          _config.OutputDir
        );

        Enabled = false;
        return;
      }

      _csvFileMask = $"{_config.OutputDir}{{yyyy}}\\{{mm}}\\{{dd}}\\";
      if (_config.UseHourlyFolders) _csvFileMask += "{hh}\\";
      _csvFileMask += "{yyyy}-{mm}-{dd} {hh}-{mod-min}0.csv";
    }
  }

  public sealed class LineProtocolPointMap : ClassMap<LineProtocolPoint>
  {
    public LineProtocolPointMap()
    {
      AutoMap(CultureInfo.InvariantCulture);
      Map(x => x.Fields).TypeConverter<IEnumerableGenericConverter>();
      Map(x => x.Tags).TypeConverter<IEnumerableGenericConverter>();
    }
  }
}
