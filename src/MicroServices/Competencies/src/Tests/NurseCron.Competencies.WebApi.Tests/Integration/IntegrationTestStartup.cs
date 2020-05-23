using NurseCron.Competencies.WebApi;
using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Core.Unigration.WebApi;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;

namespace NurseCron.Competencies.Tests.Integration
{
  public class IntegrationTestStartup : CoreWebApiIntegrationTestStartup<Startup>
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
