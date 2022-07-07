using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NurseCron.Employees.OData;
using NurseCron.Employees.OData.Data;

namespace NurseCron.Employees.Tests
{
  public class UnigrationODataTestStartup : CoreODataUnigrationTestStartup<Startup>
  {
    public UnigrationODataTestStartup(IConfiguration configuration) : base(new Startup(configuration))
    {
    }

    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      services.SetupTestDbContext<DatabaseContext>();
    }
  }
}
