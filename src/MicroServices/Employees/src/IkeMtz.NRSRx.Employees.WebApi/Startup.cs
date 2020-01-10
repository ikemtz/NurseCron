using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using IkeMtz.NRSRx.Core.WebApi;
using IkeMtz.NRSRx.Employees.Models;
using IkeMtz.NRSRx.Employees.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IkeMtz.NRSRx.Employees.WebApi
{
  public class Startup : CoreWebApiStartup
  {
    public override string MicroServiceTitle => $"NRSRx {nameof(Employee)} API Microservice";
    public override Assembly StartupAssembly => typeof(Startup).Assembly;

    public Startup(IConfiguration configuration) : base(configuration)
    {
    }

    public override void SetupMiscDependencies(IServiceCollection services)
    {
      services.AddScoped<IEmployeesContext, EmployeesContext>();
    }

    [ExcludeFromCodeCoverage]
    public override void SetupPublishers(IServiceCollection services)
    {
      services.AddServiceBusQueuePublishers<Employee>();
    }

    [ExcludeFromCodeCoverage]
    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      services
      .AddDbContext<EmployeesContext>(x => x.UseSqlServer(connectionString))
      .AddEntityFrameworkSqlServer();
    }
  }
}
