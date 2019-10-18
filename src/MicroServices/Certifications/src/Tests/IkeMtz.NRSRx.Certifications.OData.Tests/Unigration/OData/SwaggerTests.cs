using IkeMtz.NRSRx.Certifications.Abstraction.Models;
using IkeMtz.NRSRx.Certifications.OData;
using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Core.Unigration.Swagger;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace IkeMtz.NRSRx.Certifications.Tests.Unigration.OData
{
    [TestClass]
    public class SwaggerTests : BaseUnigrationTests
    {
        [TestMethod]
        [TestCategory("Unigration")]
        public async Task GetSwaggerIndexPageTest()
        {
            using (var srv = new TestServer(TestHostBuilder<Startup, UnigrationODataTestStartup>()))
            {
                await SwaggerUnitTests.TestHtmlPage(srv);
            }
        }

        [TestMethod]
        [TestCategory("Unigration")]
        public async Task GetSwaggerJsonTest()
        {
            using (var srv = new TestServer(TestHostBuilder<Startup, UnigrationODataTestStartup>()))
            {
                var doc = await SwaggerUnitTests.TestJsonDoc(srv);
                Assert.AreEqual($"NRSRx {nameof(Certification)} {nameof(OData)} Microservice", doc.Info.Title);
            }
        }
    }
}
