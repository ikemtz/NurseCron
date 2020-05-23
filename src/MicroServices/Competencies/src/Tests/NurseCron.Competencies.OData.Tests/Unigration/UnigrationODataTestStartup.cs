using NurseCron.Competencies.OData;
using NurseCron.Competencies.OData.Configuration;
using NurseCron.Competencies.OData.Data;
using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NurseCron.Competencies.Tests.Unigration
{
  public class UnigrationODataTestStartup
      : CoreODataUnigrationTestStartup<Startup, CompetencyConfiguration>
  {
    public UnigrationODataTestStartup(IConfiguration configuration) : base(new Startup(configuration))
    {
    }

    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      services.SetupTestDbContext<CompetenciesContext>();
    }
  }
}
