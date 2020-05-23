using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Core.Unigration.WebApi;
using NurseCron.HealthItems.Models;
using NurseCron.HealthItems.WebApi;
using NurseCron.HealthItems.WebApi.Data;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NurseCron.HealthItems.Tests.Unigration
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
