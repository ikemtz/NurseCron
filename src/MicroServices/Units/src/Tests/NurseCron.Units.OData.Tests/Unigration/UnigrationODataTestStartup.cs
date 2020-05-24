using IkeMtz.NRSRx.Core.Unigration;
using NurseCron.Units.OData;
using NurseCron.Units.OData.Configuration;
using NurseCron.Units.OData.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NurseCron.Units.Tests.Unigration.OData
{
  public class UnigrationODataTestStartup
      : CoreODataUnigrationTestStartup<Startup, ModelConfiguration>
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
