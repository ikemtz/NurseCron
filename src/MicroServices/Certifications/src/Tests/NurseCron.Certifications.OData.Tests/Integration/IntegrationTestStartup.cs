using NurseCron.Certifications.OData;
using NurseCron.Certifications.OData.Configuration;
using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;

namespace NurseCron.Certifications.Tests.Integration
{
  public class IntegrationODataTestStartup
      : CoreODataIntegrationTestStartup<Startup, CertificationConfiguration>
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
