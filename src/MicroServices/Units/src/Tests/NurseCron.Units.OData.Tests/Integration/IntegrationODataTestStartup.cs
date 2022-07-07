using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using NurseCron.Units.OData;

namespace NurseCron.Units.Tests.Integration.OData
{
  public class IntegrationODataTestStartup
      : CoreODataIntegrationTestStartup<Startup>
  {
    public IntegrationODataTestStartup(IConfiguration configuration)
        : base(new Startup(configuration))
    {
    }
    public override void SetupAuthentication(AuthenticationBuilder builder)
    {
      builder.SetupTestAuthentication(Configuration, TestContext);
    }
  }
}
