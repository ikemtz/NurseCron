using IkeMtz.NRSRx.Core.Unigration;
using NurseCron.Employees.OData;
using NurseCron.Employees.OData.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;

namespace NurseCron.Employees.Tests.Integration
{
  public class IntegrationTestStartup : CoreODataIntegrationTestStartup<Startup, EmployeeConfiguration>
  {
    public IntegrationTestStartup(IConfiguration configuration)
        : base(new Startup(configuration))
    {
    }
    public override void SetupAuthentication(AuthenticationBuilder builder)
    {
      builder.SetupTestAuthentication(Configuration, TestContext);
    }
  }
}
