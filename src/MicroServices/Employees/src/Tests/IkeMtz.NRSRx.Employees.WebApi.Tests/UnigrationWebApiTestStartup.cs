using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Core.Unigration.WebApi;
using IkeMtz.NRSRx.Employees.Models;
using IkeMtz.NRSRx.Employees.WebApi;
using IkeMtz.NRSRx.Employees.WebApi.Data;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IkeMtz.NRSRx.Employees.Tests
{
    public class UnigrationWebApiTestStartup : CoreWebApiUnigrationTestStartup<Startup>
    {
        public UnigrationWebApiTestStartup(IConfiguration configuration) : base(new Startup(configuration))
        {
        }

        public override void SetupDatabase(IServiceCollection services, string connectionString)
        {
            services.SetupTestDbContext<EmployeesContext>();
        }

        public override void SetupPublishers(IServiceCollection services)
        {
            var pubTester = new PublisherIntegrationTester<Employee, Message>();
            pubTester.RegisterDependencies(services);
        }
    }
}
