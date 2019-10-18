using IkeMtz.NRSRx.Competencies.OData;
using IkeMtz.NRSRx.Competencies.OData.Configuration;
using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.Extensions.Configuration;

namespace IkeMtz.NRSRx.Competencies.Tests.Integration
{
    public class IntegrationTestStartup 
        : CoreODataIntegrationTestStartup<Startup, CompetencyConfiguration>
    {
        public IntegrationTestStartup(IConfiguration configuration)
            : base(new Startup(configuration))
        {
        }
    }
}
