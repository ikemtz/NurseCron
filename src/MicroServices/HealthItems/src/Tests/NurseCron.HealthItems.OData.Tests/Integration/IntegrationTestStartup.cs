using IkeMtz.NRSRx.Core.Unigration;
using NurseCron.HealthItems.OData;
using NurseCron.HealthItems.OData.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;

namespace NurseCron.HealthItems.Tests.Integration
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
