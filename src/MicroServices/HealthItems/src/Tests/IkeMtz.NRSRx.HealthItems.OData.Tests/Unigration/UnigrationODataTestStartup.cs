using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.HealthItems.OData;
using IkeMtz.NRSRx.HealthItems.OData.Configuration;
using IkeMtz.NRSRx.HealthItems.OData.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IkeMtz.NRSRx.HealthItems.Tests.Unigration
{
    public class UnigrationODataTestStartup
        : CoreODataUnigrationTestStartup<Startup, HealthItemConfiguration>
    {
        public UnigrationODataTestStartup(IConfiguration configuration) : base(new Startup(configuration))
        {
        }

        public override void SetupDatabase(IServiceCollection services, string connectionString)
        {
            services.SetupTestDbContext<HealthItemsContext>();
        }
    }
}
