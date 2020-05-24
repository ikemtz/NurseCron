using System.Linq;
using System.Threading.Tasks;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Core.Unigration.Swagger;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NurseCron.Services.Schedules.Abstraction;

namespace NurseCron.Services.Schedules.OData.Tests.Unigration
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
      Assert.IsTrue(doc.Components.Schemas.ContainsKey(nameof(Schedule)));
      Assert.IsTrue(doc.Components.Schemas.Any(a => a.Key.Contains(nameof(ODataEnvelope<Schedule>))));
      Assert.AreEqual($"{nameof(Schedule)} OData Microservice", doc.Info.Title);
    }
  }
}
