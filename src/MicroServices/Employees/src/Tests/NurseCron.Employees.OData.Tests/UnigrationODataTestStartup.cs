using IkeMtz.NRSRx.Core.Unigration;
using NurseCron.Employees.OData;
using NurseCron.Employees.OData.Configuration;
using NurseCron.Employees.OData.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NurseCron.Employees.Tests
{
  public class UnigrationODataTestStartup : CoreODataUnigrationTestStartup<Startup, EmployeeConfiguration>
  {
    public UnigrationODataTestStartup(IConfiguration configuration) : base(new Startup(configuration))
    {
    }

    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      services.SetupTestDbContext<EmployeesContext>();
    }
  }
}
