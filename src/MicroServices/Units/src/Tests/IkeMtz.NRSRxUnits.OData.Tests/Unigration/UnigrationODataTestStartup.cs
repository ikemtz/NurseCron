using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Units.OData;
using IkeMtz.NRSRx.Units.OData.Configuration;
using IkeMtz.NRSRx.Units.OData.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IkeMtz.NRSRx.Units.Tests.Unigration.OData
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
