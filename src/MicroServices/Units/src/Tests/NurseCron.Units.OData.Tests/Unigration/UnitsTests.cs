using System;
using System.Linq;
using System.Threading.Tasks;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Unigration;
using NurseCron.Units.Abstraction;
using NurseCron.Units.OData;
using NurseCron.Units.OData.Data;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace NurseCron.Units.Tests.Unigration.OData
{
  [TestClass]
  public partial class UnitsTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Unigration")]
    public async Task GetUnitsTest()
    {
      var objA = new Unit()
      {
        Id = Guid.NewGuid(),
        Name = $"Test- {Guid.NewGuid().ToString().Substring(29)}",
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationODataTestStartup>()
          .ConfigureTestServices(x =>
          {
            ExecuteOnContext<DatabaseContext>(x, db =>
            {
              _ = db.Units.Add(objA);
            });
          })
       );
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.GetStringAsync($"odata/v1/{nameof(Unit)}s?$count=true");

      //Validate OData Result
      TestContext.WriteLine($"Server Reponse: {resp}");
      var envelope = JsonConvert.DeserializeObject<ODataEnvelope<Unit>>(resp);
      Assert.AreEqual(objA.Name, envelope.Value.First().Name);
    }
  }
}
