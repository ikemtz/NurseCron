using IkeMtz.NRSRx.Core.WebApi;
using IkeMtz.NRSRx.Employees.Models;
using IkeMtz.NRSRx.Employees.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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

    public override void SetupPublishers(IServiceCollection services)
    {
      services.AddServiceBusQueuePublishers<Employee>();
    }

    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      services
      .AddDbContext<EmployeesContext>(x => x.UseSqlServer(connectionString))
      .AddEntityFrameworkSqlServer();
    }
  }
}
