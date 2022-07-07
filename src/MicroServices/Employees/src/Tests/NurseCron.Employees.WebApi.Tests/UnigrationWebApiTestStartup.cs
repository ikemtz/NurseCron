using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Core.Unigration.WebApi;
using NurseCron.Employees.Models;
using NurseCron.Employees.WebApi;
using NurseCron.Employees.WebApi.Data;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NurseCron.Employees.Tests
{
  public class UnigrationWebApiTestStartup : CoreWebApiUnigrationTestStartup<Startup>
  {
    public UnigrationWebApiTestStartup(IConfiguration configuration) : base(new Startup(configuration))
    {
    }

    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      services.SetupTestDbContext<DatabaseContext>();
    }

    public override void SetupPublishers(IServiceCollection services)
    {
      var pubTester = new PublisherUnigrationTester<Employee, ServiceBusMessage>();
      pubTester.RegisterDependencies(services);
    }
  }
}
