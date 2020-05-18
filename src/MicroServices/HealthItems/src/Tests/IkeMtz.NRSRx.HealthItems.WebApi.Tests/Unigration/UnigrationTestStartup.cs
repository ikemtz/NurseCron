using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Core.Unigration.WebApi;
using IkeMtz.NRSRx.HealthItems.Models;
using IkeMtz.NRSRx.HealthItems.WebApi;
using IkeMtz.NRSRx.HealthItems.WebApi.Data;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IkeMtz.NRSRx.HealthItems.Tests.Unigration
{
  public class UnigrationWebApiTestStartup : CoreWebApiUnigrationTestStartup<Startup>
  {
    public UnigrationWebApiTestStartup(IConfiguration configuration) : base(new Startup(configuration))
    {
    }

    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      services.SetupTestDbContext<HealthItemsContext>();
    }

    public override void SetupPublishers(IServiceCollection services)
    {
      new PublisherUnigrationTester<HealthItem, Message>().RegisterDependencies(services);
    }
  }
}
