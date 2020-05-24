using System;
using System.Linq;
using System.Threading.Tasks;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NurseCron.Services.Schedules.Abstraction;
using NurseCron.Services.Schedules.OData.Data;

namespace NurseCron.Services.Schedules.OData.Tests.Unigration
{
  [TestClass]
  public partial class ItemsTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Unigration")]
    public async Task GetItemsTest()
    {
      var objA = new Schedule()
      {
        Id = Guid.NewGuid(),
        EmployeeName = $"Test- {Guid.NewGuid().ToString().Substring(29)}",
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationODataTestStartup>()
          .ConfigureTestServices(x =>
          {
            ExecuteOnContext<DatabaseContext>(x, db =>
            {
              _ = db.Schedules.Add(objA);
            });
          })
       );
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.GetStringAsync($"odata/v1/{nameof(Schedule)}s?$count=true");

      //Validate OData Result
      TestContext.WriteLine($"Server Reponse: {resp}");
      var envelope = JsonConvert.DeserializeObject<ODataEnvelope<Schedule>>(resp);
      Assert.AreEqual(objA.EmployeeName, envelope.Value.First().EmployeeName);
    }
  }
}
