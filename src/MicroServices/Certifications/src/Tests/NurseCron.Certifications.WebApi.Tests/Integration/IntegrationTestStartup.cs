using NurseCron.Certifications.WebApi;
using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Core.Unigration.WebApi; 
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;

namespace NurseCron.Certifications.Tests.Integration
{
  public class IntegrationWebApiTestStartup : CoreWebApiIntegrationTestStartup<Startup>
  {
    public IntegrationWebApiTestStartup(IConfiguration configuration)
        : base(new Startup(configuration))
    {
    }
    public override void SetupAuthentication(AuthenticationBuilder builder)
    {
      builder.SetupTestAuthentication(Configuration, TestContext);
    }
  }
}
