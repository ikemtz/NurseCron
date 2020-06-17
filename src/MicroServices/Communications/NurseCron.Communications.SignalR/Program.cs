using System.Diagnostics.CodeAnalysis;
using IkeMtz.NRSRx.Core.Web;
using Microsoft.Extensions.Hosting;

namespace NurseCron.Communications.SignalR
{
  public static class Program
  {
    [ExcludeFromCodeCoverage]
    public static void Main() =>
      CoreWebStartup.CreateDefaultHostBuilder<Startup>().Build().Run();
  }
}
