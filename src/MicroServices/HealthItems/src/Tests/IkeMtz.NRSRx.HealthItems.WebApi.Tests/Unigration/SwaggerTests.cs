using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Core.Unigration.Swagger;
using IkeMtz.NRSRx.HealthItems.Models;
using IkeMtz.NRSRx.HealthItems.WebApi;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace IkeMtz.NRSRx.HealthItems.Tests.Unigration.WebApi
{
    [TestClass]
    public class SwaggerTests : BaseUnigrationTests
    {
        [TestMethod]
        [TestCategory("Unigration")]
        public async Task GetSwaggerIndexPageTest()
        {
            using (var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>()))
            {
                await SwaggerUnitTests.TestHtmlPage(srv);
            }
        }

        [TestMethod]
        [TestCategory("Unigration")]
        public async Task GetSwaggerJsonTest()
        {
            using (var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>()))
            {
                var doc = await SwaggerUnitTests.TestJsonDoc(srv);
                Assert.AreEqual($"NRSRx {nameof(HealthItem)} {nameof(Api).ToUpper()} Microservice", doc.Info.Title);
            }
        }
    }
}
