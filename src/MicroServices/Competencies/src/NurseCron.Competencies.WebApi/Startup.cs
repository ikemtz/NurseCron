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
    public override string MicroServiceTitle => $"NRSRx {nameof(Competency)} API Microservice";
    public override Assembly StartupAssembly => typeof(Startup).Assembly;
    public Startup(IConfiguration configuration) : base(configuration)
    {
    }

    [ExcludeFromCodeCoverage]
    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      services
      .AddDbContext<CompetenciesContext>(x => x.UseSqlServer(connectionString))
      .AddEntityFrameworkSqlServer();
    }

    public override void SetupMiscDependencies(IServiceCollection services)
    {
      services.AddScoped<ICompetenciesContext, CompetenciesContext>();
    }

    [ExcludeFromCodeCoverage]
    public override void SetupPublishers(IServiceCollection services)
    {
      services.AddServiceBusQueuePublishers<Competency>();
    }
  }
}
