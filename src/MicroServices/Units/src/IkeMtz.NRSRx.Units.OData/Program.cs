using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace IkeMtz.NRSRx.Units.OData
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              _ = webBuilder.UseStartup<Startup>();
            });
  }
}
