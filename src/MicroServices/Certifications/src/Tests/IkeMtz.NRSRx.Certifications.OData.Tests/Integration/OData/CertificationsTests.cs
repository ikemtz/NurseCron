using IkeMtz.NRSRx.Certifications.Abstraction.Models;
using IkeMtz.NRSRx.Certifications.OData;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IkeMtz.NRSRx.Certifications.Tests.Integration.OData
{
    [TestClass]
    public partial class CertificationsTests : BaseUnigrationTests
    {
        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetEnabledCertificationsTest()
        {
            using (var srv = new TestServer(TestHostBuilder<Startup, IntegrationODataTestStartup>()))
            {
                var client = srv.CreateClient();
                GenerateAuthHeader(client, await GenerateTokenAsync());

                var resp = await client.GetStringAsync("odata/v1/Certifications?$count=true");
                TestContext.WriteLine($"Server Reponse: {resp}");
                var envelope = JsonConvert.DeserializeObject<ODataEnvelope<Certification>>(resp);
                Assert.AreEqual(envelope.Count, envelope.Value.Count());
                envelope.Value.ToList().ForEach(t =>
                {
                    Assert.IsNotNull(t.CreatedBy);
                    Assert.IsNotNull(t.CreatedOnUtc);

                    Assert.IsTrue(t.IsEnabled);
                });
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task CertificationSelectTest()
        {
            using (var srv = new TestServer(TestHostBuilder<Startup, IntegrationODataTestStartup>()))
            {
                var client = srv.CreateClient();
                GenerateAuthHeader(client, await GenerateTokenAsync());

                var resp = await client.GetAsync("odata/v1/certifications?$select=id&top=1&$count=true");

                var objB = await DeserializeResponseAsync<ODataEnvelope<Certification>>(resp);

                Assert.IsNull(objB.Value.FirstOrDefault()?.Name);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task CertificationDateFilterTest()
        {
            var objA = new Certification()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                IsEnabled = true
            };
            using (var srv = new TestServer(TestHostBuilder<Startup, IntegrationODataTestStartup>()))
            {
                var client = srv.CreateClient();
                GenerateAuthHeader(client, await GenerateTokenAsync());

                var resp = await client.GetAsync("odata/v1/certifications?filter=createdOnUtc%20gt%202019-01-02T00%3A00%3A00Z&$top=1&$count=true");

                var objB = await DeserializeResponseAsync<ODataEnvelope<Certification>>(resp);


                Assert.IsTrue(Math.Max(1, objB.Value.Count()) <= 1);

                Assert.AreEqual(DateTime.UtcNow.Year, objB.Value.FirstOrDefault()?.CreatedOnUtc.Year);
            }
        }
    }
}
