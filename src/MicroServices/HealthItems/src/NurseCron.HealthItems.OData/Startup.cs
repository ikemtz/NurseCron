using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using IkeMtz.NRSRx.Core.OData;
using NurseCron.HealthItems.Models;
using NurseCron.HealthItems.OData.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NurseCron.HealthItems.OData.Configuration;

namespace NurseCron.HealthItems.OData
{
  public class Startup : CoreODataStartup
  {
    public override string MicroServiceTitle => $"NurseCRON {nameof(HealthItem)} OData Microservice";
    public override Assembly StartupAssembly => typeof(Startup).Assembly;
    public override BaseODataModelProvider ODataModelProvider => new ODataModelProvider();

    public Startup(IConfiguration configuration) : base(configuration)
    {
    }

    [ExcludeFromCodeCoverage]
    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      _ = services
         .AddDbContextPool<DatabaseContext>(x => x.UseSqlServer(connectionString))
         .AddEntityFrameworkSqlServer();
    }
  }
}
