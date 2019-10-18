using IkeMtz.NRSRx.Certifications.WebApi;
using IkeMtz.NRSRx.Core.Unigration.WebApi;
using Microsoft.Extensions.Configuration;

namespace IkeMtz.NRSRx.Certifications.Tests.Integration
{
    public class IntegrationWebApiTestStartup : CoreWebApiIntegrationTestStartup<Startup>
    {
        public IntegrationWebApiTestStartup(IConfiguration configuration)
            : base(new Startup(configuration))
        {
        }
    }
}
