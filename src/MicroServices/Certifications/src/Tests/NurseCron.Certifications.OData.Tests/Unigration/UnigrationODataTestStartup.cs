using NurseCron.Certifications.OData;
using NurseCron.Certifications.OData.Configuration;
using NurseCron.Certifications.OData.Data;
using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NurseCron.Certifications.Tests
{
  public class UnigrationODataTestStartup
      : CoreODataUnigrationTestStartup<Startup, CertificationConfiguration>
  {
    public UnigrationODataTestStartup(IConfiguration configuration) : base(new Startup(configuration))
    {
    }

    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      services.SetupTestDbContext<CertificationsContext>();
    }
  }
}
