using IkeMtz.NRSRx.Core.Unigration;
using NurseCron.HealthItems.OData;
using NurseCron.HealthItems.OData.Configuration;
using NurseCron.HealthItems.OData.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NurseCron.HealthItems.Tests.Unigration
{
  public class UnigrationODataTestStartup
      : CoreODataUnigrationTestStartup<Startup, HealthItemConfiguration>
  {
    public UnigrationODataTestStartup(IConfiguration configuration) : base(new Startup(configuration))
    {
    }

    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      services.SetupTestDbContext<HealthItemsContext>();
    }
  }
}
