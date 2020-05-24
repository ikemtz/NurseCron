using IkeMtz.NRSRx.Core.Unigration;
using NurseCron.Units.OData;
using NurseCron.Units.OData.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;

namespace NurseCron.Units.Tests.Integration.OData
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
