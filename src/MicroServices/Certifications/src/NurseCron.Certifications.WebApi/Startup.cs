using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NurseCron.Certifications.Abstraction.Models;
using NurseCron.Certifications.WebApi.Data;
using IkeMtz.NRSRx.Core.WebApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NurseCron.Certifications.WebApi
{
  public class Startup : CoreWebApiStartup
  {
    public override string MicroServiceTitle => $"NurseCRON {nameof(Certification)} API Microservice";
    public override Assembly StartupAssembly => typeof(Startup).Assembly;

    public Startup(IConfiguration configuration) : base(configuration)
    {
    }

    [ExcludeFromCodeCoverage]
    public override void SetupPublishers(IServiceCollection services)
    {
      services.AddServiceBusQueuePublishers<Certification>();
    }

    [ExcludeFromCodeCoverage]
    public override void SetupDatabase(IServiceCollection services, string connectionString)
    {
      _ = services
       .AddDbContext<CertificationsContext>(x => x.UseSqlServer(connectionString))
       .AddEntityFrameworkSqlServer();
    }
  }
}
