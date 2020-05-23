using NurseCron.Competencies.Abstraction.Models;
using NurseCron.Competencies.WebApi;
using NurseCron.Competencies.WebApi.Data;
using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Core.Unigration.WebApi;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NurseCron.Competencies.Tests.Unigration
{
  public class UnigrationWebApiTestStartup : CoreWebApiUnigrationTestStartup<Startup>
  {
    public UnigrationWebApiTestStartup(IConfiguration configuration) : base(new Startup(configuration))
    {
    }

    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      services.SetupTestDbContext<CompetenciesContext>();
    }

    public override void SetupPublishers(IServiceCollection services)
    {
      var pubTester = new PublisherUnigrationTester<Competency, Message>();
      pubTester.RegisterDependencies(services);
    }
  }
}
