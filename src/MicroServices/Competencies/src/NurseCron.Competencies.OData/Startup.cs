using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NurseCron.Competencies.Abstraction.Models;
using NurseCron.Competencies.OData.Data;
using IkeMtz.NRSRx.Core.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NurseCron.Competencies.OData
{
  public class Startup : CoreODataStartup
  {
    public override string MicroServiceTitle => $"NurseCRON {nameof(Competency)} OData Microservice";
    public override Assembly StartupAssembly => typeof(Startup).Assembly;
    public Startup(IConfiguration configuration) : base(configuration)
    {
    }

    [ExcludeFromCodeCoverage]
    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      services
      .AddDbContextPool<CompetenciesContext>(x => x.UseSqlServer(connectionString))
      .AddEntityFrameworkSqlServer();
    }

    public override void SetupMiscDependencies(IServiceCollection services)
    {
      services.AddScoped<ICompetenciesContext, CompetenciesContext>();
    }
  }
}
