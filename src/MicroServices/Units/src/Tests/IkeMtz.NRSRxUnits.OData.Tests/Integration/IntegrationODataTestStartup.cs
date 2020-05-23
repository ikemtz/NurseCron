using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Units.OData;
using IkeMtz.NRSRx.Units.OData.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;

namespace IkeMtz.NRSRx.Units.Tests.Integration.OData
{
  public class IntegrationODataTestStartup
      : CoreODataIntegrationTestStartup<Startup, ModelConfiguration>
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
