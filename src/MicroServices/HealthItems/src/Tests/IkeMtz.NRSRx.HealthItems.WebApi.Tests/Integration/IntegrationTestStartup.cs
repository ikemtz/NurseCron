using IkeMtz.NRSRx.Core.Unigration.WebApi;
using IkeMtz.NRSRx.HealthItems.WebApi;
using Microsoft.Extensions.Configuration;

namespace IkeMtz.NRSRx.HealthItems.Tests.Integration
{
    public class IntegrationTestStartup : CoreWebApiIntegrationTestStartup<Startup>
    {
        public IntegrationTestStartup(IConfiguration configuration)
            : base(new Startup(configuration))
        {
        }
    }
}
