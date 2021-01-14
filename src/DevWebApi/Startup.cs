using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.Common.Metrics;
using Rn.NetCore.WebCommon.Filters;

namespace DevWebApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }


    public void ConfigureServices(IServiceCollection services)
    {
      ConfigureServices_RnNetCore_Common(services);
      ConfigureServices_RnNetCore_WebCommon(services);

      services.AddControllers(options =>
      {
        options.Filters.Add<MetricActionFilter>();
        options.Filters.Add<MetricExceptionFilter>();
        options.Filters.Add<MetricResultFilter>();
        options.Filters.Add<MetricResourceFilter>();
      });

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevWebApi", Version = "v1" });
      });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevWebApi v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }


    // ConfigureServices methods
    private static void ConfigureServices_RnNetCore_Common(IServiceCollection services)
    {
      services
        // Abstractions
        .AddSingleton<IDateTimeAbstraction, DateTimeAbstraction>()
        // Metrics
        .AddSingleton<IMetricService, MetricService>()
        // Logging
        .AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
    }

    private static void ConfigureServices_RnNetCore_WebCommon(IServiceCollection services)
    {

    }
  }
}
