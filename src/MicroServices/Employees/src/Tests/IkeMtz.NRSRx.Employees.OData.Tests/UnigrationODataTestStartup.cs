using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Employees.OData;
using IkeMtz.NRSRx.Employees.OData.Configuration;
using IkeMtz.NRSRx.Employees.OData.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IkeMtz.NRSRx.Employees.Tests
{
    public class UnigrationODataTestStartup : CoreODataUnigrationTestStartup<Startup, EmployeeConfiguration>
    {
        public UnigrationODataTestStartup(IConfiguration configuration) : base(new Startup(configuration))
        {
        }

        public override void SetupDatabase(IServiceCollection services, string connectionString)
        {
            services.SetupTestDbContext<EmployeesContext>();
        }
    }
}
