using IkeMtz.NRSRx.Competencies.Abstraction.Models;
using IkeMtz.NRSRx.Competencies.WebApi;
using IkeMtz.NRSRx.Competencies.WebApi.Data;
using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Core.Unigration.WebApi;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IkeMtz.NRSRx.Competencies.Tests.Unigration
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
            var pubTester = new PublisherIntegrationTester<Competency, Message>();
            pubTester.RegisterDependencies(services);
        }
    }
}
