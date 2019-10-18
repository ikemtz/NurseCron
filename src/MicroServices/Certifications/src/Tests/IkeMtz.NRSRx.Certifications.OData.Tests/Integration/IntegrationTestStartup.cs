using IkeMtz.NRSRx.Certifications.OData;
using IkeMtz.NRSRx.Certifications.OData.Configuration;
using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.Extensions.Configuration;

namespace IkeMtz.NRSRx.Certifications.Tests.Integration
{
    public class IntegrationODataTestStartup
        : CoreODataIntegrationTestStartup<Startup, CertificationConfiguration>
    {
        public IntegrationODataTestStartup(IConfiguration configuration)
            : base(new Startup(configuration))
        {
        }

    }
}
