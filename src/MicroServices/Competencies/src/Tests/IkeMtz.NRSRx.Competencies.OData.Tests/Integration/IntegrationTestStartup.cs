using IkeMtz.NRSRx.Competencies.OData;
using IkeMtz.NRSRx.Competencies.OData.Configuration;
using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.AspNetCore.Authentication;
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
    public override void SetupAuthentication(AuthenticationBuilder builder)
    {
      builder.SetupTestAuthentication(Configuration, TestContext);
    }
  }
}
