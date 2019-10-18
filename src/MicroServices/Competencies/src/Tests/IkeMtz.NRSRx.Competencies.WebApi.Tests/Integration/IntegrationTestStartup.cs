using IkeMtz.NRSRx.Competencies.WebApi;
using IkeMtz.NRSRx.Core.Unigration.WebApi;
using Microsoft.Extensions.Configuration;

namespace IkeMtz.NRSRx.Competencies.Tests.Integration
{
    public class IntegrationTestStartup : CoreWebApiIntegrationTestStartup<Startup>
    {
        public IntegrationTestStartup(IConfiguration configuration)
            : base(new Startup(configuration))
        {
        }
    }
}
