using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using IkeMtz.NRSRx.Core.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NurseCron.Employees.Models;
using NurseCron.Employees.OData.Configuration;
using NurseCron.Employees.OData.Data;

namespace NurseCron.Employees.OData
{
  public class Startup : CoreODataStartup
  {
    public override string MicroServiceTitle => $"NurseCRON {nameof(Employee)} OData Microservice";
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
