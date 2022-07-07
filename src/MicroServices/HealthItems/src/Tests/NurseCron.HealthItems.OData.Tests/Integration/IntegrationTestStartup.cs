using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using NurseCron.HealthItems.OData;

namespace NurseCron.HealthItems.Tests.Integration
{
  public class IntegrationTestStartup
      : CoreODataIntegrationTestStartup<Startup>
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
