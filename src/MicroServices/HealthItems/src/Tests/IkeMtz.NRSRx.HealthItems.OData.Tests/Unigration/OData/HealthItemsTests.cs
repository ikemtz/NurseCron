using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.HealthItems.Models;
using IkeMtz.NRSRx.HealthItems.OData;
using IkeMtz.NRSRx.HealthItems.OData.Data;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IkeMtz.NRSRx.HealthItems.Tests.Unigration.OData
{
    [TestClass]
    public partial class HealthItemsTests : BaseUnigrationTests
    {
        [TestMethod]
        [TestCategory("Unigration")]
        public async Task GetEnabledHealthItemsTest()
        {
            var objA = new HealthItem()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                IsEnabled = true,
                CreatedOnUtc = DateTime.UtcNow,
            };
            using (var srv = new TestServer(TestHostBuilder<Startup, UnigrationODataTestStartup>()
                .ConfigureTestServices(x =>
                  {
                      ExecuteOnContext<HealthItemsContext>(x, db =>
                      {
                          db.HealthItems.Add(objA);
                      });
                  })
             ))
            {
                var client = srv.CreateClient();
                GenerateAuthHeader(client, GenerateTestToken());

                var resp = await client.GetStringAsync("odata/v1/healthitems?$count=true");

                TestContext.WriteLine($"Server Reponse: {resp}");
                var envelope = JsonConvert.DeserializeObject<ODataEnvelope<HealthItem>>(resp);
                Assert.AreEqual(objA.CreatedOnUtc, envelope.Value.First().CreatedOnUtc.ToUniversalTime());
            }
        }
        [TestMethod]
        [TestCategory("Unigration")]
        public async Task HideDisabledHealthItemsTest()
        {
            var objA = new HealthItem()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                IsEnabled = false
            };
            using (var srv = new TestServer(TestHostBuilder<Startup, UnigrationODataTestStartup>()
                .ConfigureTestServices(x =>
                {
                    ExecuteOnContext<HealthItemsContext>(x, db =>
                    {
                        db.HealthItems.Add(objA);
                    });
                })
             ))
            {
                var client = srv.CreateClient();
                GenerateAuthHeader(client, GenerateTestToken());

                var resp = await client.GetAsync("odata/v1/healthitems?$count=true");

                var objB = await DeserializeResponseAsync<ODataEnvelope<HealthItem>>(resp);

                Assert.AreEqual(0, objB.Value.Count());
            }
        }

    }
}
