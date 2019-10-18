using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Employees.OData;
using IkeMtz.NRSRx.Employees.OData.Configuration;
using Microsoft.Extensions.Configuration;

namespace IkeMtz.NRSRx.Employees.Tests.Integration
{
    public class IntegrationTestStartup : CoreODataIntegrationTestStartup<Startup, EmployeeConfiguration>
    {
        public IntegrationTestStartup(IConfiguration configuration)
            : base(new Startup(configuration))
        {
        }
    }
}
