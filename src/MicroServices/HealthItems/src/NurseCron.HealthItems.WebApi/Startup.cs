using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using IkeMtz.NRSRx.Core.WebApi;
using NurseCron.HealthItems.Models;
using NurseCron.HealthItems.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NurseCron.HealthItems.WebApi
{
  public class Startup : CoreWebApiStartup
  {
    public override string MicroServiceTitle => $"NurseCRON {nameof(HealthItem)} API Microservice";
    public override Assembly StartupAssembly => typeof(Startup).Assembly;

    public Startup(IConfiguration configuration) : base(configuration)
    {
    }

    [ExcludeFromCodeCoverage]
    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      services
      .AddDbContext<DatabaseContext>(x => x.UseSqlServer(connectionString))
      .AddEntityFrameworkSqlServer();
    }

    public override void SetupMiscDependencies(IServiceCollection services)
    {
      services.AddScoped<DatabaseContext, DatabaseContext>();
    }

    [ExcludeFromCodeCoverage]
    public override void SetupPublishers(IServiceCollection services)
    {
      services.AddServiceBusQueuePublishers<HealthItem>();
    }
  }
}
