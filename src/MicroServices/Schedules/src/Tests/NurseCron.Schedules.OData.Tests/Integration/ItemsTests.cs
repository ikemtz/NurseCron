using System;
using System.Linq;
using System.Threading.Tasks;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json; 
using NurseCron.Services.Schedules.Abstraction;

namespace NurseCron.Services.Schedules.OData.Tests.Integration
{
  [TestClass]
  public partial class ItemsTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Integration")]
    public async Task GetItemsTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationODataTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.GetStringAsync($"odata/v1/{nameof(Schedule)}s?$count=true");
      TestContext.WriteLine($"Server Reponse: {resp}");
      var envelope = JsonConvert.DeserializeObject<ODataEnvelope<Schedule>>(resp);
      Assert.AreEqual(envelope.Count, envelope.Value.Count());
      envelope.Value.ToList().ForEach(t =>
      {
        Assert.IsNotNull(t.EmployeeName);
        Assert.AreNotEqual(Guid.Empty, t.Id);
      });
    }
  }
}
