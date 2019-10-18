using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.HealthItems.OData;
using IkeMtz.NRSRx.HealthItems.OData.Configuration;
using Microsoft.Extensions.Configuration;

namespace IkeMtz.NRSRx.HealthItems.Tests.Integration
{
    public class IntegrationTestStartup
        : CoreODataIntegrationTestStartup<Startup, HealthItemConfiguration>
    {
        public IntegrationTestStartup(IConfiguration configuration)
            : base(new Startup(configuration))
        {
        }
    }
}
