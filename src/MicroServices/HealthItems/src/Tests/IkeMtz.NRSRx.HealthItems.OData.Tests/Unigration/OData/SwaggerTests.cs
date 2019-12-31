using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Core.Unigration.Swagger;
using IkeMtz.NRSRx.HealthItems.Models;
using IkeMtz.NRSRx.HealthItems.OData;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace IkeMtz.NRSRx.HealthItems.Tests.Unigration.OData
{
  [TestClass]
  public class SwaggerTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Unigration")]
    public async Task GetSwaggerIndexPageTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationODataTestStartup>());
      var html = await SwaggerUnitTests.TestHtmlPageAsync(srv);
      Assert.IsNotNull(html);
    }

    [TestMethod]
    [TestCategory("Unigration")]
    public async Task GetSwaggerJsonTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationODataTestStartup>());
      var doc = await SwaggerUnitTests.TestJsonDocAsync(srv);
      Assert.IsTrue(doc.Components.Schemas.ContainsKey(nameof(HealthItem)));
      Assert.IsTrue(doc.Components.Schemas.Any(a => a.Key.Contains(nameof(ODataEnvelope<HealthItem>))));
      Assert.AreEqual($"NRSRx {nameof(HealthItem)} {nameof(OData)} Microservice", doc.Info.Title);
    }
  }
}
