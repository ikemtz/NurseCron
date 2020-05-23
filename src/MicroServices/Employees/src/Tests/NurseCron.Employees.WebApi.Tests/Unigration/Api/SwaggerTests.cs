using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Core.Unigration.Swagger;
using NurseCron.Employees.Models;
using NurseCron.Employees.WebApi;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace NurseCron.Employees.Tests.Unigration.WebApi
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
      Assert.AreEqual($"NRSRx {nameof(Employee)} {nameof(Api).ToUpper()} Microservice", doc.Info.Title);
    }
  }
}
