using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NurseCron.Services.Schedules.OData.Configuration;
using NurseCron.Services.Schedules.OData.Data;

namespace NurseCron.Services.Schedules.OData.Tests.Unigration
{
  public class UnigrationODataTestStartup
      : CoreODataUnigrationTestStartup<Startup, ModelConfiguration>
  {
    public UnigrationODataTestStartup(IConfiguration configuration) : base(new Startup(configuration))
    {
    }

    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      services.SetupTestDbContext<DatabaseContext>();
    }
  }
}
