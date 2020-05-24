using System.Diagnostics.CodeAnalysis;
using IkeMtz.NRSRx.Core.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace NurseCron.HealthItems.WebApi
{
  [ExcludeFromCodeCoverage] //This is part of the asp dotnet core and (TYPICALLY) should not be unit tested 
  public static class Program
  {
    public static void Main()
    {
      CoreWebStartup.CreateDefaultHostBuilder<Startup>().Build().Run();
    }
  }
}
