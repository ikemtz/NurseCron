using IkeMtz.NRSRx.Core.OData;
using IkeMtz.NRSRx.Employees.Models;
using IkeMtz.NRSRx.Employees.OData.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IkeMtz.NRSRx.Employees.OData
{
  public class Startup : CoreODataStartup
  {
    public override string MicroServiceTitle => $"NRSRx {nameof(Employee)} OData Microservice";
    public override Assembly StartupAssembly => typeof(Startup).Assembly;

    public Startup(IConfiguration configuration) : base(configuration)
    {
    }

    public override void SetupMiscDependencies(IServiceCollection services)
    {
      services.AddScoped<IEmployeesContext, EmployeesContext>();
    }

    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      services
      .AddDbContext<EmployeesContext>(x => x.UseSqlServer(connectionString))
      .AddEntityFrameworkSqlServer();
    }
  }
}
