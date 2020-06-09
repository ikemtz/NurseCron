using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using IkeMtz.NRSRx.Core.OData;
using NurseCron.Employees.Models;
using NurseCron.Employees.OData.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NurseCron.Employees.OData
{
  public class Startup : CoreODataStartup
  {
    public override string MicroServiceTitle => $"NurseCRON {nameof(Employee)} OData Microservice";
    public override Assembly StartupAssembly => typeof(Startup).Assembly;

    public Startup(IConfiguration configuration) : base(configuration)
    {
    }

    public override void SetupMiscDependencies(IServiceCollection services)
    {
      services.AddScoped<IEmployeesContext, EmployeesContext>();
    }

    [ExcludeFromCodeCoverage]
    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      services
      .AddDbContextPool<EmployeesContext>(x => x.UseSqlServer(connectionString))
      .AddEntityFrameworkSqlServer();
    }
  }
}
