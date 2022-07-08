using NurseCron.Certifications.Abstraction.Models;
using NurseCron.Certifications.WebApi;
using NurseCron.Certifications.WebApi.Data;
using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Core.Unigration.WebApi;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NurseCron.Certifications.Tests
{
  public class UnigrationWebApiTestStartup : CoreWebApiUnigrationTestStartup<Startup>
  {
    public UnigrationWebApiTestStartup(IConfiguration configuration) : base(new Startup(configuration))
    {
    }

    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      services.SetupTestDbContext<CertificationsContext>();
    }

    public override void SetupPublishers(IServiceCollection services)
    {
      var pubTester = new PublisherUnigrationTester<Certification, ServiceBusMessage>();
      pubTester.RegisterDependencies(services);
    }
  }
}
