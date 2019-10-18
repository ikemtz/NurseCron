﻿using IkeMtz.NRSRx.Competencies.Abstraction.Models;
using IkeMtz.NRSRx.Competencies.OData;
using IkeMtz.NRSRx.Competencies.Tests.Unigration;
using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Core.Unigration.Swagger;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                Assert.AreEqual($"NRSRx {nameof(Competency)} {nameof(OData)} Microservice", doc.Info.Title);
            }
        }
    }
}
