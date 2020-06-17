using IkeMtz.NRSRx.Core.Unigration.SignalR;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using NurseCron.Communications.SignalR;

namespace NurseCron.Communication.SignalR.Tests
{
  public class UnigrationTestStartup : CoreSignalrUnigrationTestStartup<Startup>
  {
    public UnigrationTestStartup(IConfiguration configuration) : base(new Startup(configuration))
    {
    }
    public override void MapHubs(IEndpointRouteBuilder endpoints)
    {
      Startup.MapHubs(endpoints);
    }
  }
}
