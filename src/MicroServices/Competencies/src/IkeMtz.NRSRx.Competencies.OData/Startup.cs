using IkeMtz.NRSRx.Competencies.Abstraction.Models;
using IkeMtz.NRSRx.Competencies.OData.Data;
using IkeMtz.NRSRx.Core.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IkeMtz.NRSRx.Competencies.OData
{
  public class Startup : CoreODataStartup
  {
    public override string MicroServiceTitle => $"NRSRx {nameof(Competency)} OData Microservice";
    public override Assembly StartupAssembly => typeof(Startup).Assembly;
    public Startup(IConfiguration configuration) : base(configuration)
    {
    }

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
  }
}
