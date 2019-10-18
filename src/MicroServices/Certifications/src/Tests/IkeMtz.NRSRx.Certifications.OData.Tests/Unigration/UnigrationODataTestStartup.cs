using IkeMtz.NRSRx.Certifications.OData;
using IkeMtz.NRSRx.Certifications.OData.Configuration;
using IkeMtz.NRSRx.Certifications.OData.Data;
using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IkeMtz.NRSRx.Certifications.Tests
{
    public class UnigrationODataTestStartup
        : CoreODataUnigrationTestStartup<Startup, CertificationConfiguration>
    {
        public UnigrationODataTestStartup(IConfiguration configuration) : base(new Startup(configuration))
        {
        }

        public override void SetupDatabase(IServiceCollection services, string connectionString)
        {
            services.SetupTestDbContext<CertificationsContext>();
        }
    }
}
