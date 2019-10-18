using IkeMtz.NRSRx.Certifications.Abstraction.Models;
using IkeMtz.NRSRx.Certifications.WebApi;
using IkeMtz.NRSRx.Certifications.WebApi.Data;
using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Core.Unigration.WebApi;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IkeMtz.NRSRx.Certifications.Tests
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
            var pubTester = new PublisherIntegrationTester<Certification, Message>();
            pubTester.RegisterDependencies(services);
        }
    }
}
