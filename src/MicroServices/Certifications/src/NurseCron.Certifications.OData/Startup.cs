using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NurseCron.Certifications.Abstraction.Models;
using NurseCron.Certifications.OData.Data;
using IkeMtz.NRSRx.Core.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NurseCron.Certifications.OData.Configuration;

namespace NurseCron.Certifications.OData
{
  public class Startup : CoreODataStartup
  {
    public override string MicroServiceTitle => $"NurseCRON {nameof(Certification)} OData Microservice";
    public override Assembly StartupAssembly => typeof(Startup).Assembly;
    public override BaseODataModelProvider ODataModelProvider => new ODataModelProvider();

    public Startup(IConfiguration configuration) : base(configuration)
    {
    }


    [ExcludeFromCodeCoverage]
    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      _ = services
      .AddDbContextPool<CertificationsContext>(x => x.UseSqlServer(connectionString))
      .AddEntityFrameworkSqlServer();
    }
  }
}
