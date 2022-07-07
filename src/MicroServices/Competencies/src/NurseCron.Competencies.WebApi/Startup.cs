using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NurseCron.Competencies.Abstraction.Models;
using NurseCron.Competencies.WebApi.Data;
using IkeMtz.NRSRx.Core.WebApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NurseCron.Competencies.WebApi
{
  public class Startup : CoreWebApiStartup
  {
    public override string MicroServiceTitle => $"NurseCRON {nameof(Competency)} API Microservice";
    public override Assembly StartupAssembly => typeof(Startup).Assembly;
    public Startup(IConfiguration configuration) : base(configuration)
    {
    }

    [ExcludeFromCodeCoverage]
    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      _ = services
      .AddDbContext<DatabaseContext>(x => x.UseSqlServer(connectionString))
      .AddEntityFrameworkSqlServer();
    }

    [ExcludeFromCodeCoverage]
    public override void SetupPublishers(IServiceCollection services)
    {
      services.AddServiceBusQueuePublishers<Competency>();
    }
  }
}
