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
  public partial class BuildingsTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Unigration")]
    public async Task GetBuildingsTest()
    {
      var objA = new Building()
      {
        Id = Guid.NewGuid(),
        Name = $"Test- {Guid.NewGuid().ToString()[29..]}",
        AddressLine1 = $"100 {TestDataFactory.StringGenerator(10)}",
        CityOrMunicipality = TestDataFactory.StringGenerator(5),
        StateOrProvidence = TestDataFactory.StringGenerator(2, characterSet: CharacterSets.UpperCase),
        Country = "USA",
        PostalCode = "12345",
        SiteName = TestDataFactory.StringGenerator(5)
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationODataTestStartup>()
          .ConfigureTestServices(x =>
          {
            ExecuteOnContext<DatabaseContext>(x, db =>
            {
              _ = db.Buildings.Add(objA);
            });
          })
       );
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.GetStringAsync($"odata/v1/{nameof(Building)}s?$count=true");

      //Validate OData Result
      TestContext.WriteLine($"Server Reponse: {resp}");
      var envelope = JsonConvert.DeserializeObject<ODataEnvelope<Building>>(resp);
      Assert.AreEqual(objA.Name, envelope?.Value.First().Name);
    }
  }
}
