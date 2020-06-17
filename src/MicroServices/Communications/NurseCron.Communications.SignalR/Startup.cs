using IkeMtz.NRSRx.Core.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using NurseCron.Communications.SignalR.Hubs;

namespace NurseCron.Communications.SignalR
{
  public class Startup : CoreSignalrStartup
  {
    public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration) : base(configuration) { }

    public override void MapHubs(IEndpointRouteBuilder endpoints)
    {
      _ = endpoints.MapHub<NotificationHub>("/notificationHub");
    }
  }
}
