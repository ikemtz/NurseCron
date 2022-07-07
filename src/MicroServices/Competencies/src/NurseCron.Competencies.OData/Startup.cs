using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using IkeMtz.NRSRx.Core.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NurseCron.Competencies.Abstraction.Models;
using NurseCron.Competencies.OData.Configuration;
using NurseCron.Competencies.OData.Data;

namespace NurseCron.Competencies.OData
{
  public class Startup : CoreODataStartup
  {
    public override string MicroServiceTitle => $"NurseCRON {nameof(Competency)} OData Microservice";
    public override Assembly StartupAssembly => typeof(Startup).Assembly;
    public Startup(IConfiguration configuration) : base(configuration)
    {
    }
    public override BaseODataModelProvider ODataModelProvider => new ODataModelProvider();

    [ExcludeFromCodeCoverage]
    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      _ = services
       .AddDbContextPool<DatabaseContext>(x => x.UseSqlServer(connectionString))
       .AddEntityFrameworkSqlServer();
    }
  }
}
