using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.HealthItems.Models;
using IkeMtz.NRSRx.HealthItems.OData;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace IkeMtz.NRSRx.HealthItems.Tests.Integration.OData
{
    [TestClass]
    public partial class HealthItemsTests : BaseUnigrationTests
    {
        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetEnabledHealthItemsTest()
        {
            using (var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>()))
            {
                var client = srv.CreateClient();
                GenerateAuthHeader(client, await GenerateTokenAsync());

                var resp = await client.GetStringAsync("odata/v1/healthitems?$count=true");
                TestContext.WriteLine($"Server Reponse: {resp}");
                var envelope = JsonConvert.DeserializeObject<ODataEnvelope<HealthItem>>(resp);
                Assert.AreEqual(envelope.Count, envelope.Value.Count());
                envelope.Value.ToList().ForEach(t =>
                {
                    Assert.IsNotNull(t.CreatedBy);
                    Assert.IsNotNull(t.CreatedOnUtc);

                    Assert.IsTrue(t.IsEnabled);
                });
            }
        }
    }
}
