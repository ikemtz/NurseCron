using System;
using System.Linq;
using System.Threading.Tasks;
using IkeMtz.NRSRx.Competencies.Abstraction.Models;
using IkeMtz.NRSRx.Competencies.OData;
using IkeMtz.NRSRx.Competencies.OData.Data;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace IkeMtz.NRSRx.Competencies.Tests.Unigration.OData
{
  [TestClass]
  public partial class CompetenciesTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Unigration")]
    public async Task GetEnabledCompetenciesTest()
    {
      var objA = new Competency()
      {
        Id = Guid.NewGuid(),
        Name = "Test",
        IsEnabled = true,
        CreatedOnUtc = DateTime.UtcNow,
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationODataTestStartup>()
          .ConfigureTestServices(x =>
            {
              ExecuteOnContext<CompetenciesContext>(x, db =>
                    {
                      db.Competencies.Add(objA);
                    });
            })
       );
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.GetStringAsync("odata/v1/competencies?$count=true");

      TestContext.WriteLine($"Server Reponse: {resp}");
      var envelope = JsonConvert.DeserializeObject<ODataEnvelope<Competency>>(resp);
      Assert.AreEqual(objA.CreatedOnUtc, envelope.Value.First().CreatedOnUtc.ToUniversalTime());
    }
    [TestMethod]
    [TestCategory("Unigration")]
    public async Task HideDisabledCompetenciesTest()
    {
      var objA = new Competency()
      {
        Id = Guid.NewGuid(),
        Name = "Test",
        IsEnabled = false
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationODataTestStartup>()
          .ConfigureTestServices(x =>
          {
            ExecuteOnContext<CompetenciesContext>(x, db =>
                  {
                    db.Competencies.Add(objA);
                  });
          })
       );
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.GetAsync("odata/v1/competencies?$count=true");

      var objB = await DeserializeResponseAsync<ODataEnvelope<Competency>>(resp);

      Assert.AreEqual(0, objB.Value.Count());
    }

  }
}
