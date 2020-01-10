using System;
using System.Net;
using System.Threading.Tasks;
using IkeMtz.NRSRx.Competencies.Abstraction.Models;
using IkeMtz.NRSRx.Competencies.WebApi;
using IkeMtz.NRSRx.Competencies.WebApi.Data;
using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace IkeMtz.NRSRx.Competencies.Tests.Unigration.Api
{
  [TestClass]
  public class CompetenciesTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Unigration")]
    public async Task GetCompetencyTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());
      //Get 
      var resp = await client.GetAsync($"api/v1/competencies.json");

      Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
      var result = await resp.Content.ReadAsStringAsync();

      var obj = JsonConvert.DeserializeObject<dynamic>(result);
      Assert.AreEqual("NRSRx Competency API Microservice Controller", obj.name.ToString());
    }

    [TestMethod]
    [TestCategory("Unigration")]
    public async Task SaveCompetencyTest()
    {
      var objA = new CompetencyInsertRequest
      {
        Name = Guid.NewGuid().ToString()
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.PutAsJsonAsync("api/v1/competencies.json", objA);

      var result = await DeserializeResponseAsync<Competency>(resp);
      Assert.AreEqual(objA.Name, result.Name);
      Assert.IsTrue(result.IsEnabled);
      Assert.AreEqual("Integration Tester", result.CreatedBy);

      var ctx = srv.GetDbContext<CompetenciesContext>();
      var comp = await ctx.Competencies.FirstOrDefaultAsync(t => t.Name == result.Name);
      Assert.IsNotNull(comp);
      Assert.AreEqual(result.CreatedOnUtc, comp.CreatedOnUtc);
    }

    [TestMethod]
    [TestCategory("Unigration")]
    public async Task UpdateCompetencyTest()
    {
      var objA = new Competency
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString(),
        IsEnabled = true,
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>()
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
      //Update
      objA.Name = Guid.NewGuid().ToString();
      var resp = await client.PostAsJsonAsync($"api/v1/competencies.json?id={objA.Id}", new CompetencyUpdateRequest(objA));
      var result = await DeserializeResponseAsync<Competency>(resp);

      Assert.AreEqual("Integration Tester", result.UpdatedBy);
      Assert.AreEqual(objA.Name, result.Name);
      var ctx = srv.GetDbContext<CompetenciesContext>();
      var comp = await ctx.Competencies.FirstOrDefaultAsync(t => t.Name == result.Name);

      Assert.IsNotNull(comp);
      Assert.AreEqual(result.UpdatedOnUtc, comp.UpdatedOnUtc);
      Assert.AreEqual("Integration Tester", comp.UpdatedBy);
    }

    [TestMethod]
    [TestCategory("Unigration")]
    public async Task UpdateCompetencyNotFoundTest()
    {
      var objA = new CompetencyUpdateRequest
      {
        Name = Guid.NewGuid().ToString(),
        Id = Guid.NewGuid(),
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.PostAsJsonAsync($"api/v1/competencies.json?id={objA.Id}", objA);
      Assert.AreEqual(HttpStatusCode.NotFound, resp.StatusCode);
      Assert.AreEqual("\"Competency Not Found\"", await resp.Content.ReadAsStringAsync());
    }

    [TestMethod]
    [TestCategory("Unigration")]
    public async Task DeleteCompetencyTest()
    {
      var objA = new Competency
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString(),
        IsEnabled = true,
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>()
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
      //Update 
      var resp = await client.DeleteAsync($"api/v1/Competencies.json?id={objA.Id}");

      Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
      var result = await DeserializeResponseAsync<Competency>(resp);

      Assert.AreEqual("Integration Tester", result.UpdatedBy);
      Assert.IsFalse(result.IsEnabled);
    }

    [TestMethod]
    [TestCategory("Unigration")]
    public async Task DeleteCompetencyNotFoundTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());
      //Delete
      var resp = await client.DeleteAsync($"api/v1/competencies.json?id={Guid.NewGuid()}");
      Assert.AreEqual(HttpStatusCode.NotFound, resp.StatusCode);
      Assert.AreEqual("\"Competency Not Found\"", await resp.Content.ReadAsStringAsync());
    }

  }
}
