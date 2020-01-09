using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.HealthItems.OData;
using IkeMtz.NRSRx.HealthItems.OData.Configuration;
using Microsoft.AspNetCore.Authentication;
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
    public override void SetupAuthentication(AuthenticationBuilder builder)
    {
      builder.SetupTestAuthentication(Configuration, TestContext);
    }
  }
}
