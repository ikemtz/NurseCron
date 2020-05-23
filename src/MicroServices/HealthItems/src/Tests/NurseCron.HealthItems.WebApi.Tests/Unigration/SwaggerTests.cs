using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Core.Unigration.Swagger;
using NurseCron.HealthItems.Models;
using NurseCron.HealthItems.WebApi;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace NurseCron.HealthItems.Tests.Unigration.WebApi
{
  [TestClass]
  public class SwaggerTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Unigration")]
    public async Task GetSwaggerIndexPageTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>());
      var html = await SwaggerUnitTests.TestHtmlPageAsync(srv);
      Assert.IsNotNull(html);
    }

    [TestMethod]
    [TestCategory("Unigration")]
    public async Task GetSwaggerJsonTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>());
      var doc = await SwaggerUnitTests.TestJsonDocAsync(srv);
      Assert.AreEqual($"NRSRx {nameof(HealthItem)} {nameof(Api).ToUpper()} Microservice", doc.Info.Title);
    }
  }
}
